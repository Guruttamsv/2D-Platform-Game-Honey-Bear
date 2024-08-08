using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehaviour : MonoBehaviour
{
    public float patrolSpeed;
    public float patrolRange;
    public string playerTag = "Player"; // Variable for player tag

    float currentSpeed;
    bool isAttacking = false;

    Vector3 wayPoint;
    Vector3 originalScale;
    float helper = 1.0F;

    Animator myAnim;

    void Start()
    {
        myAnim = GetComponent<Animator>();
        originalScale = transform.localScale;
        SetNewDestination();
        
    }

    void Update()
    {
        if (!isAttacking)
        {
            Patrol();
        }
        else
        {
            Attack();
        }
    }

    void Patrol()
    {
        myAnim.SetInteger("enemyState", 0); // Set to idle
        currentSpeed = patrolSpeed;

        // Calculate the direction based on the current movement
        Vector3 direction = (wayPoint - transform.position).normalized;

        // Normalize the direction to ensure consistent speed
        direction.Normalize();

        // Move the enemy
        transform.Translate(direction * currentSpeed * Time.deltaTime);

        // Check for the player's proximity to trigger the attack
        CheckForAttack();

        // Check if the enemy has reached the target position
        if (Vector3.Distance(transform.position, wayPoint) < 0.1f)
        {
            // Toggle the direction
            Flip();
            SetNewDestination();
            
        }
    }

 void Attack()
{
    // Find the player
    GameObject player = GameObject.FindGameObjectWithTag(playerTag);

    if (player != null)
    {
        // Face the player while attacking
        Vector3 playerDirection = (player.transform.position - transform.position).normalized;

        // Flip the enemy if the player is to the left
        if (playerDirection.x < 0)
        {
            Vector3 theScale = transform.localScale;
            theScale.x = Mathf.Abs(theScale.x);
            transform.localScale = theScale;
        }
        else
        {
            // If the player is to the right, make sure the enemy is facing the right
            Flip();
        }

        myAnim.SetInteger("enemyState", 1); // Set to attack
    }
    else
    {
        Debug.LogWarning("Player not found. Make sure it has the correct tag.");
    }
}

    void SetNewDestination()
    {
        helper *= -1;
        Vector3 currentPosition = transform.position;

        float tobeX = currentPosition.x + patrolRange * helper;
        wayPoint = new Vector3(tobeX, currentPosition.y, currentPosition.z);
        
    }

    void CheckForAttack()
    {
        // Check for the player's proximity to trigger the attack
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);

        if (player != null)
        {
            if (Vector2.Distance(transform.position, player.transform.position) < 1.0f)
            {
                isAttacking = true;
            }
        }
        else
        {
            Debug.LogWarning("Player not found. Make sure it has the correct tag.");
        }
    }

    void Flip()
    {
        // Flip the enemy based on the direction

            // If the direction is to the left, flip the enemy
            Vector3 theScale = transform.localScale;
            theScale.x = Mathf.Abs(originalScale.x) * helper;
            transform.localScale = theScale;
        
        
    }
}