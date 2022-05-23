using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.tag);
        if(gameObject.tag == "EndPoint" && other.gameObject.tag == "Player")
        {
            print("스테이지 종료");
        }
    }
}
