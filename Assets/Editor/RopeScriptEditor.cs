using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Rope))]
public class RopeScriptEditor : Editor
{

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        DrawDefaultInspector();

        Rope myRope = (Rope)target;
        if (GUILayout.Button("Bake Rope"))
        {
            myRope.BakeRope();
            EditorUtility.SetDirty(myRope);
        }
        if (GUILayout.Button("Clear"))
        {
            myRope.ClearRope();
            EditorUtility.SetDirty(myRope);
        }
    }

    [MenuItem("GameObject/3D Object/Custom/Rope")]
    public static void NewRope(){
        var newRope = 
            PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets/RopeBase.prefab", typeof(GameObject)));

        PrefabUtility.DisconnectPrefabInstance(newRope);

        newRope.name = "Rope";
        Selection.activeGameObject = newRope as GameObject;
    }
}
