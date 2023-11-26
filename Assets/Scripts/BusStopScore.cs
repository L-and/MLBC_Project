using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusStopScore : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("[OnTriggerEnter]정류장점수 획득!");
        if (other.tag == "Player")
        {
            ScoreManager.AddBusStopScore();
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
