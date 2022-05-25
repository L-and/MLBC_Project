using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadChecker : MonoBehaviour
{
    public bool isRoadExist;

    private void OnTriggerEnter(Collider other) // 콜라이더에 차선이 감지되면 isRoadExist 가 있다고 함
    {
        if(other.gameObject.tag == "Road")
        {
            isRoadExist = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Road")
        {
            isRoadExist = true;
        }
    }

    private void OnTriggerExit(Collider other)  // 콜라이더에 차선이 감지되면 isRoadExist 가 있다고 함
    {
        if (other.gameObject.tag == "Road")
        {
            isRoadExist = false;
        }
    }
}
