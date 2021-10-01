using UnityEngine;

public class TeaTest : MonoBehaviour
{
    void Start()
    {
        this.tt("ReverseTest")
            .Add(1, () => { Debug.Log($"0 at {Time.time}"); })
            .Add(1, () => { Debug.Log($"1 at {Time.time}"); })
            .Add(1, () => { Debug.Log($"2 at {Time.time}"); })
            .Add(1, t => t.self.Reverse())
            .Add(1, () => { Debug.Log($"3 at {Time.time}"); })
            .Add(1, () => { Debug.Log($"4 at {Time.time}"); })
            .Reverse();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            this.tt("ReverseTest").Play();
        }
    }
}