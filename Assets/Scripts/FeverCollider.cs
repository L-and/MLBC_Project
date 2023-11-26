using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverCollider : MonoBehaviour
{
    [SerializeField]
    private ObjectPool unitObjectPool;

    [SerializeField]
    private ScoreUIGenerator scoreUIGenerator;

    [SerializeField]
    GameObject explosionEffect;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Unit")
        {
            int randomScore = Random.Range(20, 40);
            ScoreManager.AddFeverScore(randomScore);
            unitObjectPool.ReturnObject(other.gameObject);
            Instantiate(explosionEffect, other.transform.position, Quaternion.identity, null);
            scoreUIGenerator.DrawScoreUI(other.gameObject.transform.position, randomScore);
        }
    }
}
