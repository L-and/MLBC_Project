using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameManger : MonoBehaviour
{
    private UnitMove unitMove;
    private PlayerController playerController;

    private bool _isGameRunning = false;

    public bool isGameRunning
    {
        get { return _isGameRunning; }
        set
        {
            _isGameRunning = !_isGameRunning;

            if(_isGameRunning == true)
                activateComponents();
        }
    }

    private void Start()
    {
        unitMove = gameObject.GetComponent<UnitMove>();
        playerController = gameObject.GetComponent<PlayerController>();
    }

    private void activateComponents()
    {
        unitMove.enabled = true;
        playerController.enabled = true;
    }
    
    
}


#if UNITY_EDITOR
[CustomEditor(typeof(GameManger))]
public class GameMangerEditor : Editor
{
    public GameManger script;

    public void OnEnable()
    {
        script = (GameManger)target;
    }

    public override void OnInspectorGUI()
    {
        bool is_isGameRunning = !script.isGameRunning;
        GUI.backgroundColor = (is_isGameRunning ? Color.red : Color.green);

        if(GUILayout.Button("isGameRunning is"+script.isGameRunning+"(Click to make "+is_isGameRunning+")"))
        {
            script.isGameRunning = is_isGameRunning;
        }

        GUI.backgroundColor = Color.white;
        base.OnInspectorGUI();
    }
}

#endif
