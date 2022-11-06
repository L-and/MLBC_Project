using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null; // 싱글톤패턴

    private UnitMove playerUnitMove;

    [SerializeField]
    private GameObject[] objectPoolManagers;

    private PlayerController playerController;

    private bool _isGameRunning = false; // 게임실행시 true

    public bool isGameRunning
    {
        get { return _isGameRunning; }
        set
        {
            _isGameRunning = !_isGameRunning;

            if (_isGameRunning == true)
                ActivateComponents();
        }
    }

    private void Awake()
    {
       if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
       else
        {
            Destroy(this.gameObject);
        }
            
    }

    private void Start()
    {
        GameObject player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        playerUnitMove = player.GetComponent<UnitMove>();

        DisableComponents();
    }

    private void ActivateComponents()
    {
        playerUnitMove.enabled = true;

        playerController.enabled = true;

        for(int i=0; i< objectPoolManagers.Length; i++)
        {
            objectPoolManagers[i].SetActive(true);
        }
    }

    private void DisableComponents()
    {
        playerUnitMove.enabled = false;

        playerController.enabled = false;

        for (int i = 0; i < objectPoolManagers.Length; i++)
        {
            objectPoolManagers[i].SetActive(false);
        }
    }


}



// 에디터에서 bool변수 수정을 위한 스크립트
#if UNITY_EDITOR
[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public GameManager script;

    public void OnEnable()
    {
        script = (GameManager)target;
    }

    public override void OnInspectorGUI()
    {
        bool is_isGameRunning = !script.isGameRunning;
        GUI.backgroundColor = (is_isGameRunning ? Color.red : Color.green);

        if (GUILayout.Button("isGameRunning is" + script.isGameRunning + "(Click to make " + is_isGameRunning + ")"))
        {
            script.isGameRunning = is_isGameRunning;
        }

        GUI.backgroundColor = Color.white;
        base.OnInspectorGUI();
    }
}

#endif
