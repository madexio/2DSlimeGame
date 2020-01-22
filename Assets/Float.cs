using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour
{
    // Start is called before the first frame update

    public float amplitude;
    public float speed;
    Transform crystalTransform;
    Vector3 newPos;
    Vector3 startPos;
    void Start()
    {
        crystalTransform = GetComponent<Transform>();
        startPos = crystalTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        newPos = new Vector3(crystalTransform.position.x, startPos.y + amplitude * Mathf.Sin(speed * Time.time), crystalTransform.position.z);
        crystalTransform.position = newPos;
    }
}
