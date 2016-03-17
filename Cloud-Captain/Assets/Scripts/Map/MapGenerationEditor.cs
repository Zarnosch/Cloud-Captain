using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MapGeneration))]
public class MapGenerationEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("Generate World"))
        {
            MapGeneration generation = (MapGeneration)target;
            generation.GenerateWorld();
        }
        if (GUILayout.Button("Destroy World"))
        {
            MapGeneration generation = (MapGeneration)target;
            generation.DestroyWorld();
        }
    }
}
