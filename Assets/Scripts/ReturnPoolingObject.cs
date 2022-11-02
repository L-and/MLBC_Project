using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnPoolingObject : MonoBehaviour
{
    Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform.position.z - 5 > gameObject.transform.position.z)
        {
            transform.parent.gameObject.GetComponent<ObjectPool>().ReturnObject(gameObject);
            transform.parent.gameObject.GetComponent<ObjectGenerateManager>().ObjectPosDequeue();
        }
    }
}
