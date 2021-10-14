using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Snapshot : MonoBehaviour
{
    public string id;
    public Transform source;
    public int index = 0;

    public List<Dictionary<Transform, Snapframe>> frames = new List<Dictionary<Transform, Snapframe>>();

    public void NewSnapshot()
    {
        var frame = new Dictionary<Transform, Snapframe>();

        for (int i = 0; i < source.childCount; i++)
        {
            var t = source.GetChild(i);
            var snap = new Snapframe(t);

            frame[t] = snap;
        }

        frames.Add(frame);
        index = frames.Count - 1;
    }

    public void UpdateSnapshot()
    {
        if (frames.Count < 1)
        {
            NewSnapshot();
            return;
        }

        var frame = new Dictionary<Transform, Snapframe>();

        for (int i = 0; i < source.childCount; i++)
        {
            var t = source.GetChild(i);
            var snap = new Snapframe(t);

            frame[t] = snap;
        }

        frames[index] = frame;
    }

    public void DeleteSnapshot()
    {
        if (frames.Count < 1)
            return;

        frames.RemoveAt(index);
        Show(-1);
    }

    public void Show(int ahead)
    {
        if (frames.Count < 1)
            return;

        index = (index + ahead) % frames.Count;
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
{kv.Key.name}
    position  {position}
    rotation  {rotation}
    scale     {scale} >";
            }

            json += $"{trans}]\n\n";
        }

        return $"{json.Trim()}";
    }

    public void Save()
    {
        File.WriteAllText("Snapshot.txt", ToJson());
    }

    public void Load()
    {
        var text = File.ReadAllText("Snapshot.txt");

        frames.Clear();
        foreach (var frame in text.Trim().Split(']'))
        {
            var dict = new Dictionary<Transform, Snapframe>();

            foreach (var element in frame.Trim().Split('>'))
            {
                if (element.Trim().Length < 1)
                    continue;

                var parts = element.Trim().Split('\n');

                var name = parts[0].Trim();
                var pos = Bitf.Floats(parts[1]);
                var rot = Bitf.Floats(parts[2]);
                var sca = Bitf.Floats(parts[3]);

                var snap = new Snapframe(
                    new Vector3(pos[0], pos[1], pos[2]),
                    new Quaternion(rot[0], rot[1], rot[2], rot[3]),
                    new Vector3(sca[0], sca[1], sca[2]));

                var t = source.Find(name);
                dict[t] = snap;
            }

            if (dict.Count > 0)
                frames.Add(dict);
        }
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

    public Snapframe(Vector3 position, Quaternion rotation, Vector3 scale)
    {
        this.position = position;
        this.rotation = rotation;
        this.scale = scale;
    }
}