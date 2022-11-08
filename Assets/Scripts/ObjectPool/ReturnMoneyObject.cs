using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 코인이 플레이어 닿을 시 처리를하는 스크립트
public class ReturnMoneyObject : MonoBehaviour
{
    Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            MoneyManager.AddMoney(1);

            // 오브젝트풀에 이 오브젝트를 돌려줌
            transform.parent.gameObject.GetComponent<ObjectPool>().ReturnObject(this.gameObject);
        }
    }
}
