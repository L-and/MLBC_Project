using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if(playerTransform.position.z - 20 > transform.position.z)
        {
            ObjectPool.ReturnObject(gameObject);
            UnitGenerateManager.UnitPosDequeue();
        }
    }
}
