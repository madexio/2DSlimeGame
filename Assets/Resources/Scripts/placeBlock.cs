using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class placeBlock : MonoBehaviour
{

    public GameObject player;
    public float changeRate;
    SpriteRenderer platformSR;
    BoxCollider2D platformBC;
    Transform platformTransform;
    Rigidbody2D playerRigidBody;

    public float platformXShift;
    public float platformYShift;
    bool isPlaced;

    // Start is called before the first frame update
    void Start()
    {
        platformSR = GetComponent<SpriteRenderer>();
        platformBC = GetComponent<BoxCollider2D>();
        platformTransform = GetComponent<Transform>();
        playerRigidBody = player.GetComponent<Rigidbody2D>();
        platformSR.enabled = false;
        platformBC.enabled = false;

        isPlaced = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            bePlaced();
            //Debug.Log("Shift key hit");
        }
    }

    void bePlaced()
    {
        
        if (!isPlaced)
        {
            platformXShift = playerRigidBody.velocity.x/5.0f;
            if (playerRigidBody.velocity.y < 0)
            {
                platformYShift = -2.0f;
            } else
            {
                platformYShift = -1f;
            }
            platformTransform.transform.position = new Vector2(player.transform.position.x + platformXShift, player.transform.position.y + platformYShift);
            
            platformSR.enabled = true;
            platformBC.enabled = true;
            player.transform.localScale -= new Vector3(changeRate, changeRate, 0);
            isPlaced = true;
        }
        //yield return new WaitForSeconds(3);
        else if (isPlaced)
        {
            //platformTransform.transform.position = player.transform.position;
            platformBC.enabled = false;
            platformSR.enabled = false;
            player.transform.localScale += new Vector3(changeRate, changeRate, 0);
            isPlaced = false;
        }
    }
}