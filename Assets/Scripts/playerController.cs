using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerController : MonoBehaviour
{
    public lifeManager cm;
    // Move variables
    public float maxspeed;

    // Jump variables
    bool grounded = false;
    float groundCheckRadius = 0.02f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpHeight;

    Rigidbody2D myRB;
    Animator myAnim;
    bool facingRight;

    bool isControllable = true; // Added flag for controlling player

    private bool isKicking = false;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();

        facingRight = true;
    }

    void Update()
    {
        if (grounded && Input.GetAxis("Jump") > 0 && isControllable)
        {
            grounded = false;
            myAnim.SetBool("isGrounded", grounded);
            myRB.AddForce(new Vector2(0, jumpHeight));
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        myAnim.SetBool("isGrounded", grounded);
        myAnim.SetFloat("verticalSpeed", myRB.velocity.y);

        if (isControllable)
        {
            // Check if the Shift key is pressed
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            // Trigger the "kick" animation
            myAnim.SetTrigger("Kick");
            isKicking=true;
        }
        
            float move = Input.GetAxis("Horizontal");
            myAnim.SetFloat("speed", Mathf.Abs(move));
            myRB.velocity = new Vector2(move * maxspeed, myRB.velocity.y);

            if (move > 0 && !facingRight)
            {
                flip();
            }
            else if (move < 0 && facingRight)
            {
                flip();
            }
        }
    }

    void flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    // Method to enable or disable player control
    public void SetControllable(bool controllable)
    {
        isControllable = controllable;
    }

   void OnTriggerEnter2D(Collider2D other)
   {
    if (other.gameObject.CompareTag("StickCoins"))
    {
        Destroy(other.gameObject);
        cm.playerLife++;
    }
    else if (other.gameObject.CompareTag("Enemy"))
    {
        if (isKicking)
        {
            cm.PlayerKicksEnemy(other.gameObject);
            isKicking = false;
        }
        else
        {
            // Assuming the player loses life when touched by an enemy
            cm.EnemyTouchesPlayer();
        }
    }
    else if (other.gameObject.CompareTag("QueenBee"))
    {
        if (isKicking)
        {
            cm.PlayerKicksQueen(other.gameObject);
            isKicking = false;
        }
        else
        {
            // Assuming the player loses life when touched by an enemy
            cm.EnemyTouchesPlayer();
        }
    }
}

}
