using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Drawing;
using System.IO;

[CustomEditor(typeof(ImageEffects))]
public class ImageEffectsWindow : Editor
{
    Texture2D tex;
    bool isTextureLoaded;

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Load Image"))
            OpenFolderPanel();
        if (isTextureLoaded)
        {
           GUILayout.Label(tex);

        }
        //isTextureLoaded = false;                                                                                 
    }

    void OpenFolderPanel()
    {
        string path = EditorUtility.OpenFilePanel("Open Image file", "", "png");
        byte[] fileDate;
        fileDate = File.ReadAllBytes(path);
        tex = new Texture2D(2, 2);
        tex.LoadImage(fileDate);
        isTextureLoaded = true;
    }
}
