using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnUnitObject : MonoBehaviour
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
            // 오브젝트풀에 이 오브젝트를 돌려줌
            transform.parent.gameObject.GetComponent<ObjectPool>().ReturnObject(this.gameObject);
            transform.parent.gameObject.GetComponent<ObjectGenerateManager>().ObjectPosDequeue();
        }
    }
}
