using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null; // 싱글톤패턴

    private GameObject player;

    private UnitMove playerUnitMove;

    [SerializeField]
    private GameObject[] objectPoolManagers;

    [SerializeField]
    private ObjectGenerateManager unitGenerateManager;

    private PlayerController playerController;

    private bool _isGameRunning = false; // 게임실행시 true

    private Vector3 initPlayerPosition; // 다시하기를 위해 필요한 플레이어의 초기 위치

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
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        playerUnitMove = player.GetComponent<UnitMove>();

        initPlayerPosition = player.transform.position;

        DisableComponents();
    }

    public static void ActivateComponents()
    {
        Instance.playerUnitMove.enabled = true;

        Instance.playerController.enabled = true;

        for(int i=0; i< Instance.objectPoolManagers.Length; i++)
        {
            Instance.objectPoolManagers[i].gameObject.SetActive(true);
        }

        Instance.unitGenerateManager.ChangeObjectSpawnRangeNear();
    }

    private static void DisableComponents()
    {
        Instance.playerUnitMove.enabled = false;

        Instance.playerController.enabled = false;

        for (int i = 0; i < Instance.objectPoolManagers.Length; i++)
        {
            //Instance.objectPoolManagers[i].GetComponent<ObjectPool>().ReturnAllObject();
            Instance.objectPoolManagers[i].gameObject.SetActive(false);
        }
    }

    public static void GameEnd()
    {
        UIManager.EnableEndUI();
        DisableComponents();
    }

    public static void ReloadGame() // 다시하기 누를 시 초기화면으로 리셋해줌
    {
        //MapCycleSystem.ResetMapPosition(); // 맵들의 위치 재설정
        //Instance.player.transform.position = Instance.initPlayerPosition; // 플레이어 위치 재설정
        //UIManager.EnableMainUI(); // UI 재설정
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void GameExit() // 게임종료
    {
        Application.Quit();
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
