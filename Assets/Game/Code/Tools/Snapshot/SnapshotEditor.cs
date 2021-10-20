#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Snapshot))]
public class SnapshotEditor : Editor
{
    SerializedProperty fileCache;

    public void OnEnable()
    {
        fileCache = serializedObject.FindProperty("fileCache");
    }

    public override void OnInspectorGUI()
    {
        var snap = (Snapshot) target;
        if (!snap)
            return;

        snap.source = EditorGUILayout.ObjectField("Source", snap.source, typeof(Transform), true) as Transform;

        GUILayout.Label("");
        snap.filter = EditorGUILayout.TextField("Search", snap.filter);
        if (snap.lastFilter != snap.filter)
        {
            snap.lastFilter = snap.filter;
            snap.SearchFiles();
        }

        if (snap.files.Length > 0)
        {
            GUILayout.BeginVertical();
            foreach (var file in snap.files)
            {
                if (GUILayout.Button(file))
                    snap.Load(file);
            }
            GUILayout.EndVertical();
        }

        GUILayout.Label("");
        snap.currentFile = EditorGUILayout.TextField("Name", snap.currentFile);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("New"))
            snap.NewSnapshot();
        if (GUILayout.Button("Update"))
            snap.UpdateSnapshot();
        if (GUILayout.Button("Delete"))
            snap.DeleteSnapshot();
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("<<"))
            snap.Step(-1);
        if (GUILayout.Button(">>"))
            snap.Step(1);
        GUILayout.EndHorizontal();

        if (snap.frames.Count < 1)
            snap.index = 0;
        GUILayout.Label($"Frame {snap.index + 1} of {snap.frames.Count}");

        GUILayout.Label("");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Load"))
            snap.Load(snap.currentFile);
        if (GUILayout.Button("Save"))
            snap.Save();
        GUILayout.EndHorizontal();

        GUILayout.Label("");
        EditorGUILayout.PropertyField(fileCache, new GUIContent("Files Cache"));
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Cache all"))
            snap.CacheAllFiles();
        if (GUILayout.Button("Clear"))
            snap.ClearFileCache();
        if (GUILayout.Button("Set Dirty"))
            EditorUtility.SetDirty(target);
        GUILayout.EndHorizontal();
    }
}

#endif