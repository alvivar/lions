using UnityEngine;

public static class Util
{
    public static string Flat(float f)
    {
        var nf = (int) f;
        var df = (int) Mathf.Abs((f - nf) * 1000);

        return $"{nf}.{df}";
    }
}