using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverCollider : MonoBehaviour
{
    [SerializeField]
    private ObjectPool unitObjectPool;

    [SerializeField]
    private ScoreUIGenerator scoreUIGenerator;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Unit")
        {
            int randomScore = Random.Range(20, 40);
            ScoreManager.AddFeverScore(randomScore);
            unitObjectPool.ReturnObject(other.gameObject);
            scoreUIGenerator.DrawScoreUI(other.gameObject.transform.position, randomScore);
        }
    }
}
