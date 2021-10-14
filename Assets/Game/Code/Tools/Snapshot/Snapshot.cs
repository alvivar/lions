using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

// #jam
public class Snapshot : MonoBehaviour
{
    public Transform root;
    public int index = 0;
    public Snapframe first = null;

    private List<Dictionary<Transform, Snapframe>> frames = new List<Dictionary<Transform, Snapframe>>();

    [ContextMenu("Take snapshot")]
    public void TakeSnapshot()
    {
        var dict = new Dictionary<Transform, Snapframe>();

        for (int i = 0; i < root.childCount; i++)
        {
            var t = root.GetChild(i);
            var snap = new Snapframe(t);

            if (first == null)
                first = snap;

            dict[t] = snap;
        }

        frames.Add(dict);

        Save();
    }

    [ContextMenu("Next")]
    public void Next()
    {
        if (frames.Count < 0)
            return;

        if (index >= frames.Count)
            index = 0;

        var snap = frames[index];
        foreach (var kv in snap)
        {
            kv.Key.localPosition = kv.Value.position;
            kv.Key.localRotation = kv.Value.rotation;
            kv.Key.localScale = kv.Value.scale;
        }

        index = ++index % frames.Count;
    }

    [ContextMenu("Reset")]
    public void Reset()
    {
        frames.Clear();
    }

    public string ToJson()
    {
        string json = "";
        foreach (var frame in frames)
        {
            string trans = "";
            foreach (var kv in frame)
            {
                var x = kv.Value.position.x;
                var y = kv.Value.position.y;
                var z = kv.Value.position.z;
                var position = $"{x}, {y}, {z}";

                x = kv.Value.rotation.x;
                y = kv.Value.rotation.y;
                z = kv.Value.rotation.z;
                var w = kv.Value.rotation.w;
                var rotation = $"{x}, {y}, {z}, {w}";

                x = kv.Value.scale.x;
                y = kv.Value.scale.y;
                z = kv.Value.scale.z;
                var scale = $"{x}, {y}, {z}";

                trans += $@"
                    {{""{kv.Key.name}"" : {{
                        ""position"" : ""{position}"",
                        ""rotation"" : ""{rotation}"",
                        ""scale"" : ""{scale}""
                    }}}},
                ";
            }

            json += $"[{trans.Trim().TrimEnd(',')}],";
        }

        return $@"[{json.Trim().TrimEnd(',')}]";
    }

    [ContextMenu("Save")]
    public void Save()
    {
        File.WriteAllText("Snapshot.json", ToJson());
    }
}

[Serializable]
public class Snapframe
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;

    public Snapframe(Transform target)
    {
        position = target.transform.localPosition;
        rotation = target.transform.localRotation;
        scale = target.transform.localScale;
    }
}