using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;
using UnityEditor;
using Dummiesman;

public class SelectCADFile : MonoBehaviour
{
    [Button("Load CAD File")] private void Method1() { LoadCADFile(); }

    string path;

    public ScaleObj tmp;

    void LoadCADFile()
    {
        if (!Application.isPlaying) {
            Debug.LogWarning("Application must be running to load CAD files");
            return;
        }
        //System.Diagnostics.Process.Start("explorer.exe");
        path = EditorUtility.OpenFilePanel("Select .obj File", "", "obj");
        GameObject importedObj = new OBJLoader().Load(path);
        //Mesh importedMesh = GetComponent<ObjFromFile>().ImportFile(path);

        tmp.obj = importedObj;
    }
}
