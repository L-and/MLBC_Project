using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverCollider : MonoBehaviour
{
    [SerializeField]
    private ObjectPool unitObjectPool;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Unit")
        {
            ScoreManager.AddFeverScore(50.0f);
            unitObjectPool.ReturnObject(other.gameObject);
        }
    }
}