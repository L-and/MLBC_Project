using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null; // 싱글톤패턴

    private GameObject[] unitGameObjects;
    private UnitMove[] unitMoves; // 모든 유닛들의 unitMove
    private UnitMove playerUnitMove;

    private PlayerController playerController;

    private bool _isGameRunning = false; // 게임실행시 true

    public bool isGameRunning
    {
        get { return _isGameRunning; }
        set
        {
            _isGameRunning = !_isGameRunning;

            if (_isGameRunning == true)
                activateComponents();
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
///////////유니티 에디터/////////////
# if UNITY_EDITOR
        _isGameRunning = true;
# endif
//////////////////////////////////
        unitGameObjects = GameObject.FindGameObjectsWithTag("Unit");

        unitMoves = new UnitMove[unitGameObjects.Length];

        for (int i = 0; i < unitGameObjects.Length; i++)
        {
            unitMoves[i] = unitGameObjects[i].GetComponent<UnitMove>();
        }
        playerUnitMove = gameObject.GetComponent<UnitMove>();

        playerController = gameObject.GetComponent<PlayerController>();
    }

    private void activateComponents()
    {
        for (int i = 0; i < unitMoves.Length; i++)
        {
            unitMoves[i].enabled = true;
        }
        playerUnitMove.enabled = true;

        playerController.enabled = true;
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
