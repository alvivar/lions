using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Snapshot : MonoBehaviour
{
    public string id;
    public Transform source;
    public int index = 0;

    private List<Dictionary<Transform, Snapframe>> frames = new List<Dictionary<Transform, Snapframe>>();

    public void NewSnapshot()
    {
        var dict = new Dictionary<Transform, Snapframe>();

        for (int i = 0; i < source.childCount; i++)
        {
            var t = source.GetChild(i);
            var snap = new Snapframe(t);

            dict[t] = snap;
        }

        frames.Add(dict);
        index = frames.Count - 1;

        Save();
    }

    public void UpdateSnapshot()
    {
        var frame = new Dictionary<Transform, Snapframe>();

        for (int i = 0; i < source.childCount; i++)
        {
            var t = source.GetChild(i);
            var snap = new Snapframe(t);

            frame[t] = snap;
        }

        frames[index] = frame;

        Save();
    }

    public void DeleteSnapshot()
    {
        if (frames.Count < 0)
            return;

        frames.RemoveAt(index);
        Show(-1);
        Save();
    }

    public void Show(int idx)
    {
        if (frames.Count < 0)
            return;

        index = (index + idx) % frames.Count;
        index = index >= frames.Count ? 0 : index;
        index = index < 0 ? frames.Count - 1 : index;

        var snap = frames[index];
        foreach (var kv in snap)
        {
            kv.Key.localPosition = kv.Value.position;
            kv.Key.localRotation = kv.Value.rotation;
            kv.Key.localScale = kv.Value.scale;
        }
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

            var clean = string.Join(" ", trans.Split(null).Where(x => x.Trim().Length > 0));
            json += $"[{clean.Trim().TrimEnd(',')}],";
        }

        return $"[{json.Trim().TrimEnd(',')}]";
    }

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