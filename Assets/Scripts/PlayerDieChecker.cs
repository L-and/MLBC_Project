using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieChecker : MonoBehaviour
{
    PlayerController playerController;

    private void Start()
    {
        playerController = transform.root.gameObject.GetComponent<PlayerController>();
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Unit")
        {
            playerController.playerDie();

            Debug.Log("플레이어 사망");
        }
    }
}
