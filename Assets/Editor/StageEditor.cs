using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StageManager))]
public class StageEditor : Editor
{

    public override void OnInspectorGUI()
    {
        StageManager myStage = (StageManager)target;

        myStage.STAGE_X = EditorGUILayout.IntField("STAGE_X", myStage.STAGE_X);
        myStage.STAGE_Y = EditorGUILayout.IntField("STAGE_Y", myStage.STAGE_Y);
        myStage.backBlock = (GameObject)EditorGUILayout.ObjectField("Back Block:", myStage.backBlock,typeof(GameObject),true);
        myStage.foreBlock = (GameObject)EditorGUILayout.ObjectField("Fore Block:", myStage.foreBlock, typeof(GameObject), true);

        if (GUILayout.Button("Create Stage"))
        {
            myStage.createStageFunction();
        }
    }
}
