using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject[] poolingObjectPrefabs;

    private Dictionary<string, Queue<GameObject>> poolingObjectQueue = new Dictionary<string, Queue<GameObject>>();

    private void Awake()
    {
        Initialize(10);
    }

    private void Initialize(int initCount) // 오브젝트 초기화
    {
        for (int i = 0; i < poolingObjectPrefabs.Length; i++)
        {
            for (int j = 0; j < initCount; j++)
            {
                if (!poolingObjectQueue.ContainsKey(poolingObjectPrefabs[i].name))
                {
                    Queue<GameObject> newQueue = new Queue<GameObject>();
                    poolingObjectQueue.Add(poolingObjectPrefabs[i].name, newQueue);
                }

                poolingObjectQueue[poolingObjectPrefabs[i].name].Enqueue(CreateNewObject(i));
            }
        }
    }

    private GameObject CreateNewObject(int prefabIndex) // 풀링할 오브젝트를 생성
    {
        var newObj = Instantiate(poolingObjectPrefabs[prefabIndex]);
        newObj.name = poolingObjectPrefabs[prefabIndex].name;
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);

        return newObj;
    }

    public GameObject GetObject(Vector3 position) // 오브젝트를 사용
    {
        int prefabIndex = (int)Random.Range(0, poolingObjectPrefabs.Length);
        if (poolingObjectQueue[poolingObjectPrefabs[prefabIndex].name].Count > 0)
        {
            var obj = poolingObjectQueue[poolingObjectPrefabs[prefabIndex].name].Dequeue();
            obj.transform.position = position;
            obj.gameObject.SetActive(true);
            obj.transform.SetParent(this.transform);

            return obj;
        }
        else
        {
            var newObj = CreateNewObject(prefabIndex);
            newObj.transform.position = position;
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(this.transform);

            return newObj;
        }
        
    }

    public void ReturnObject(GameObject obj) // 오브젝트를 반환
    {
        obj.gameObject.SetActive(false); // 오브젝트를 비활성화
        obj.transform.SetParent(transform); // 매니저 오브젝트를 부모로 설정
        poolingObjectQueue[obj.name].Enqueue(obj); // 오브젝트를 큐에 다시넣어줌
        if(gameObject.GetComponent<ObjectGenerateManager>() != null)
            gameObject.GetComponent<ObjectGenerateManager>().ObjectPosDequeue(); // ObjectGenerateManager의 ObjectPos큐를 비워줌
    }

    //public void ReturnAllObject() // 오브젝트풀링중인 오브젝트들을 모두 풀에 리턴해줌
    //{
    //    int childIndex = 0;
    //    for (int i = 0; i < (poolingObjectQueue.Count); i++)
    //    {
    //        for(int j=0; j < transform.childCount / poolingObjectQueue.Count; j++)
    //        {
    //            GameObject childObject = transform.GetChild(childIndex).gameObject;
    //            if (childObject.activeSelf == true)
    //            {
    //                //transform.GetChild(childIndex).gameObject.name = childIndex.ToString();
    //                ReturnObject(childObject);
    //            }
    //            childIndex++;
    //        }
    //    }
    //}

}
