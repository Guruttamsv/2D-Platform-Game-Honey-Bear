using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenBehaviour : MonoBehaviour
{
    public float speed;
    public float range;
    public string playerTag = "Player"; // Tag for the player

    bool isFollowing = false; // Flag to determine if the Queen should follow the player

    void Update()
    {
        // Find the player GameObject by tag
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);

        if (player != null)
        {
            // Check if the player is within the specified range to start following
            if (Vector2.Distance(transform.position, player.transform.position) < range)
            {
                // The player is within range, start following
                isFollowing = true;
            }
        }
        else
        {
            Debug.LogWarning("Player not found. Make sure it has the correct tag.");
        }

        // Only follow the player if the trigger condition is met
        if (isFollowing)
        {
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        // Find the player GameObject by tag
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);

        if (player != null)
        {
            // Move towards the player's position
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

            // Check if the Queen is within the specified range of the player
            if (Vector2.Distance(transform.position, player.transform.position) < range)
            {
                // The Queen is within range, stop following (you can adjust this condition as needed)
                isFollowing = false;
            }
        }
        else
        {
            Debug.LogWarning("Player not found. Make sure it has the correct tag.");
        }
    }



}
