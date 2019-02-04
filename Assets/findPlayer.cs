using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class findPlayer : MonoBehaviour
{
    public GameObject player;
    public GameObject platform;

    ParticleSystem platformParticle;
    Transform particleTransform;

    float minDist;
    public float speed;
    bool isPlaced;

    // Start is called before the first frame update
    void Start()
    {
        platformParticle = GetComponent<ParticleSystem>();
        particleTransform = GetComponent<Transform>();

        minDist = 0.5f;
        isPlaced = false;

        platformParticle.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (!isPlaced)
            {
                isPlaced = false;
                particleTransform.transform.position = platform.transform.position;
                platformParticle.Play();
                
            } else if (isPlaced)
            {
                isPlaced = true;
                platformParticle.Clear();
                platformParticle.Stop();
            }
            Debug.Log(isPlaced);
        }
        if (!isPlaced)
        {
            if (particleTransform.position.x < player.transform.position.x)
            {
                particleTransform.position = new Vector2(particleTransform.position.x + speed, particleTransform.position.y);
            }
            else if (particleTransform.position.x > player.transform.position.x)
            {
                particleTransform.position = new Vector2(particleTransform.position.x - speed, particleTransform.position.y);
            }

            if (particleTransform.position.y < player.transform.position.y)
            {
                particleTransform.position = new Vector2(particleTransform.position.x, particleTransform.position.y + speed);
            }
            else if (particleTransform.position.y > player.transform.position.y)
            {
                particleTransform.position = new Vector2(particleTransform.position.x, particleTransform.position.y - speed);
            }
        }

        Vector2 difference = new Vector2(Mathf.Abs(particleTransform.position.x - player.transform.position.x), Mathf.Abs(particleTransform.transform.position.y - player.transform.position.y));
        //Debug.Log(difference.x + difference.y);
        if (difference.x < minDist && difference.y < minDist)
        {
            platformParticle.Clear();
            platformParticle.Stop();
            isPlaced = false;
            Debug.Log(isPlaced);
        }
    }
}
