﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class placeBlock : MonoBehaviour
{

    public GameObject player;
    SpriteRenderer platformSR;
    BoxCollider2D platformBC;
    Transform platformTransform;

    bool isPlaced;

    // Start is called before the first frame update
    void Start()
    {
        platformSR = GetComponent<SpriteRenderer>();
        platformBC = GetComponent<BoxCollider2D>();
        platformTransform = GetComponent<Transform>();
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
            platformTransform.transform.position = new Vector2(player.transform.position.x, player.transform.position.y - 1);
            platformSR.enabled = true;
            platformBC.enabled = true;
            player.transform.localScale -= new Vector3(0.5f, 0.5f, 0);
            isPlaced = true;
        }
        //yield return new WaitForSeconds(3);
        else if (isPlaced)
        {
            //platformTransform.transform.position = player.transform.position;
            platformBC.enabled = false;
            platformSR.enabled = false;
            player.transform.localScale += new Vector3(0.5f, 0.5f, 0);
            isPlaced = false;
        }
    }
}