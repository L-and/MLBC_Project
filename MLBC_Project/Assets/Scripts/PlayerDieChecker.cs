using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Unit")
        {
            GameManager.GameEnd();
        }
    }
}
