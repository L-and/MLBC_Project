using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어와 카메라간의 거리를 유지시켜주는 스크립트
public class PlayerFllowCam : MonoBehaviour
{
    private GameObject mainCam; // 메인카메라 오브젝트
    private GameObject player; // 플레이어 오브젝트

    private Vector3 offset; // 플레이어와 카메라간의 벡터차


    private void Start()
    {
        mainCam = GameObject.Find("Main Camera");
        player = GameObject.Find("Player");

        offset = mainCam.transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        MaintenanceCamOffset();
    }

    private void MaintenanceCamOffset()
    {
        mainCam.transform.position = new Vector3(
            mainCam.transform.position.x,
            player.transform.position.y + offset.y,
            player.transform.position.z + offset.z
            );
    }

}
