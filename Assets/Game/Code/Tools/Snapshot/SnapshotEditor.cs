using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Snapshot))]
public class SnapshotEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var snap = (Snapshot) target;

        snap.id = EditorGUILayout.TextField("Name", snap.id);
        snap.source = EditorGUILayout.ObjectField("Source", snap.source, typeof(Transform), true) as Transform;

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("New"))
            snap.NewSnapshot();
        if (GUILayout.Button("Update"))
            snap.NewSnapshot();
        if (GUILayout.Button("Delete"))
            snap.DeleteSnapshot();
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("<<"))
            snap.Show(-1);
        if (GUILayout.Button(">>"))
            snap.Show(1);
        GUILayout.EndHorizontal();

        snap.index = EditorGUILayout.IntField("Index", snap.index);
    }
}