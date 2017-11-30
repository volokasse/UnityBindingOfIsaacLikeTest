using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LivesManager))]
public class LivesManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LivesManager script = (LivesManager)target;
        if (GUILayout.Button("-1 Live"))
        {
            script.Hit();
        }
        if (GUILayout.Button("+1 Live"))
        {
            script.Heal();
        }
    }
}