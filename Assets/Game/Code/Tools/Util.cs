using UnityEngine;

public static class Util
{
    public static float Round(float f, int precision)
    {
        precision *= 10;
        return Mathf.Round(f * precision) / precision;
    }

    public static string Flat(float f, int precision)
    {
        precision *= 10;

        var nf = (int) f;
        var df = (int) Mathf.Abs((f - nf) * precision);

        return $"{nf}.{df}";
    }
}