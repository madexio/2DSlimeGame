using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class platformerController : MonoBehaviour
{
    //Components needed on the player for a 2d platformer with animations
    Rigidbody2D playerRigidBody;
    CapsuleCollider2D playerCollider;
    Animator playerAnim;
    SpriteRenderer playerSR;
    Transform playerTransform;

    //Where to display text
    public Text displayText;
    //Public ints that will control movement/jumping speeds
    public int jumpHeight;
    public int moveSpeed;
    public int maxSpeed;
    public float drag;
    //Bool to send to animator to tell what to do
    bool isWalking;
    //Keycodes for movement
    public KeyCode joyStickJump;
    public KeyCode joyStickAction0;
    public KeyCode joyStickAction1;
    public KeyCode joyStickAction2;

    public KeyCode keyJump;
    public KeyCode keyAction0;
    public KeyCode keyAction1;
    public KeyCode keyAction2;

    //Player health
    int health;
    //Float used to detect if touching the ground using the "ground" tag
    float distToGround;
    //Bools to check for movement or jumping
    bool isMove;
    bool grounded;
    //Movement vectors
    Vector2 horVector;
    Vector2 verVector;
    // Start is called before the first frame update
    void Start()
    {
        //Get the components from the gameObject attached to
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        playerAnim = GetComponent<Animator>();
        playerSR = GetComponent<SpriteRenderer>();
        playerTransform = GetComponent<Transform>();
        //Immediately calculate the distance to the ground (ie bottom of player collider)
        distToGround = playerCollider.bounds.extents.y + 0.2f;
        //Initialize vectors
        horVector = new Vector2(0, 0);
        verVector = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Update the horizontal movement vector depending on the horizontal input
        //add force equal to the horizontal vector to the player
        playerRigidBody.AddForce(getHorMovement(Input.GetAxis("Horizontal")));
        //Check to see if the jump key was pressed
        if (Input.GetKeyDown(keyJump) || Input.GetKeyDown(joyStickJump))
        {
            playerRigidBody.AddForce(jump());
        }
    }
    /*
     * Vector 2getHorMovement(float hor)
     * float hor = horizontal axis, checks what direction to add force or if 0 reduce
     * This function takes the horizontal axis and figures out what force to add to the player in order
     * to move them appropriately and returns it
     */
    Vector2 getHorMovement(float hor)
    {
        //Move right
        if (hor > 0)
        {
            //First check to see if the player is already at or above maxSpeed
            if (playerRigidBody.velocity.x >= maxSpeed)
            {
                //If above, add no force
                return new Vector2(0, 0);
            }
            else
            {
                //If moving to the left when trying to go right, add more force to player as to start moving
                //In the opposite direction quicker ie turn around
                if (playerRigidBody.velocity.x < 0)
                {
                    playerSR.flipX = true;
                    return new Vector2(drag * moveSpeed, 0);
                }
                //Set the animator bool to true for isWalking
                isWalking = true;
                playerSR.flipX = true;
                playerAnim.SetBool("isWalking", isWalking);
                
                //If not moving, or already moving right, add a force equal to moveSpeed

                return new Vector2(moveSpeed, 0);
            }
        }
        //Move left
        else if (hor < 0)
        {
            //First check to see if the player is already at or above the maxSpeed (in the -x direction)
            if (playerRigidBody.velocity.x <= -maxSpeed)
            {
                //If above, add no more force
                return new Vector2(0, 0);
            }
            else
            {
                //Another 'turn around' function
                if (playerRigidBody.velocity.x > 0)
                {
                    playerSR.flipX = false;
                    return new Vector2(-drag * moveSpeed, 0);
                }
                //Set the animator bool to true for isWalking
                isWalking = true;
                playerSR.flipX = false;
                playerAnim.SetBool("isWalking", isWalking);
                //Move left instead of right
                return new Vector2(-moveSpeed, 0);
            }
        }
        else
        {
            //No movement input, stop animating
            isWalking = false;
            playerAnim.SetBool("isWalking", isWalking);
            //If no keys are held, reduce velocity relative to the drag variable
            return new Vector2(-2*drag * playerRigidBody.velocity.x, 0);
        }
    }
    /*Vector2 jump()
     * Just checks if isGrounded() returns true and then
     * returns a vector with a y value to be added to player
     */
    Vector2 jump()
    {
        //Check to see if the player is grounded
        if (isGrounded())
        {
            //Return a jump vector
            return new Vector2(0, jumpHeight);
        }
        else
        {
            //Otherwise return an empty vector
            return new Vector2(0, 0);
        }
    }
    public bool isGrounded()
    {
        //Get the current position of the player
        Vector3 tempVector = playerRigidBody.transform.position;
        //Use the distance to the ground to shift the vector just below the player
        tempVector.y -= distToGround - 0.05f;
        //If the ray hits something, we are grounded
        return Physics2D.Raycast(tempVector, Vector2.down, 0.05f);
    }
}