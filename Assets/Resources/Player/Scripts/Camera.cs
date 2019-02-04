using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y > 3)
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -1);
        } else //if (transform.position.y <=3) 
        {
            transform.position = new Vector3(player.transform.position.x, transform.position.y, -1);
        }
    }
}
