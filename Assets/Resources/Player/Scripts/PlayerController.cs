using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    
    //Things attached to the Slime
    Rigidbody2D playerRigidBody;
    CapsuleCollider2D playerCollider;
    Animator playerAnim;
    SpriteRenderer playerSR;
    Transform playerTransform;

    //Score related things
    int score;
    int health;
    public Text scoreText;

    //Variables for physics
    public float horMovement;
    float distToGround;

    //For use with animations
    bool isWalking = false;
    //bool isPlaced = false;

    //Movement integers to change in unity interface
    public int moveSpeed;
    public int jumpHeight;

    // Use this for initialization
	void Start () {

        //Score initialization to 0
        score = 0;
        health = 3;
        SetScoreText();

        //Get the rigidbody and others from the object attached to
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        playerAnim = GetComponent<Animator>();
        playerSR = GetComponent<SpriteRenderer>();
        playerTransform = GetComponent<Transform>();

        //Use the collider to see if grounded
        distToGround = playerCollider.bounds.extents.y + 0.2f;
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(isGrounded());
        //Horizontal movement
        horMovement = Input.GetAxis("Horizontal");
        Vector2 horVector = new Vector2(horMovement * moveSpeed, playerRigidBody.velocity.y);
        //Limiting horizontal movement
        if (playerRigidBody.velocity.x < 5)
        {
            playerRigidBody.velocity = horVector;
        }
        else if (playerRigidBody.velocity.x > -5)
        {
            playerRigidBody.velocity = horVector;
        }

        //Vertical movement (jump)
        Vector2 verVector = new Vector2(0, jumpHeight*10);
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            playerRigidBody.AddForce(verVector);
        } 

        //Animation controls
        if (Input.GetAxis("Horizontal") > 0)
        {
            isWalking = true;
            playerAnim.SetBool("isWalking", isWalking);
            playerSR.flipX = true;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            isWalking = true;
            playerAnim.SetBool("isWalking", isWalking);
            playerSR.flipX = false;
        }
        else
        {
            isWalking = false;
            playerAnim.SetBool("isWalking", isWalking);
        }
        //Player dies or falls off map, reload scene
        if (health == 0 || playerTransform.position.y < -5)
        {
            SceneManager.LoadScene("Level1");
        }
        //Hard reset
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManager.LoadScene("Level1");
        }

    }

    public bool IsGrounded()
    {
        Vector3 tempVector = playerRigidBody.transform.position;
        tempVector.y -= distToGround - 0.05f;
        //Debug.DrawRay(tempVector, Vector2.down, Color.blue, 0.05f);
        return Physics2D.Raycast(tempVector, Vector2.down, 0.05f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PickUpSm"))
        {
            collision.gameObject.SetActive(false);
            score++;
            SetScoreText();
        }
        else if (collision.gameObject.CompareTag("PickUpBig"))
        {
            collision.gameObject.SetActive(false);
            score += 3;
            SetScoreText();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 knockBack = new Vector2(0, 10);
        //Debug.Log(diff.x + " " + diff.y);
        if (collision.gameObject.CompareTag("Damage"))
        {
            health--;
            SetScoreText();
            playerAnim.SetBool("isHurt", true);
            playerTransform.localScale *= 0.75f;
            playerRigidBody.velocity = knockBack;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Damage"))
        {
            playerAnim.SetBool("isHurt", false);
        }
    }

    /*IEnumerator PlaceObject()
    {
        Vector2 temp = new Vector2(0, -1);

        blockToPlace.transform.position = playerRigidBody.position + temp;
        blockToPlace.SetActive(true);
    }
    */
    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString() + "\tHealth: " + health.ToString();
    }
}
