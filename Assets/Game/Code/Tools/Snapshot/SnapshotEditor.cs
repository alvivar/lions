using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Snapshot))]
public class SnapshotEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var snap = (Snapshot) target;

        snap.source = EditorGUILayout.ObjectField("Source", snap.source, typeof(Transform), true) as Transform;

        GUILayout.Label("");
        snap.filter = EditorGUILayout.TextField("Search", snap.filter);
        if (snap.lastFilter != snap.filter)
            snap.Search();

        if (snap.files.Length > 0)
        {
            GUILayout.BeginVertical();
            foreach (var file in snap.files)
            {
                if (GUILayout.Button(file))
                {
                    snap.chosenFile = file;
                    snap.Load();
                }
            }
            GUILayout.EndVertical();
        }

        GUILayout.Label("");
        snap.chosenFile = EditorGUILayout.TextField("Name", snap.chosenFile);
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
            snap.Show(-1);
        if (GUILayout.Button(">>"))
            snap.Show(1);
        GUILayout.EndHorizontal();

        if (snap.frames.Count < 1)
            snap.index = 0;
        GUILayout.Label($"Frame {snap.index + 1} of {snap.frames.Count}");

        GUILayout.Label("");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Load"))
            snap.Load();
        if (GUILayout.Button("Save"))
            snap.Save();
        GUILayout.EndHorizontal();
    }
}