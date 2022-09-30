using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    private GameObject[] unitGameObjects;
    private UnitMove[] unitMoves; // 모든 유닛들의 unitMove

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

    private void Start()
    {
        unitGameObjects = GameObject.FindGameObjectsWithTag("Unit");

        unitMoves = new UnitMove[unitGameObjects.Length];

        for (int i = 0; i < unitGameObjects.Length; i++)
        {
            unitMoves[i] = unitGameObjects[i].GetComponent<UnitMove>();
        }


        playerController = gameObject.GetComponent<PlayerController>();

        Debug.Log(gameObject.name);
    }

    private void activateComponents()
    {
        for (int i = 0; i < unitMoves.Length; i++)
        {
            unitMoves[i].enabled = true;
        }
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
