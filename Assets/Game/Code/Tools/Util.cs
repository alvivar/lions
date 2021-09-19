using UnityEngine;

public static class Util
{
    public static float Round(float f, int precision)
    {
        precision = (int) Mathf.Pow(10, precision);

        return Mathf.Round(f * precision) / precision;
    }

    public static string Flat(float f, int precision)
    {
        precision = (int) Mathf.Pow(10, precision);

        var intF = (int) f;
        var decF = (int) Mathf.Abs((f - intF) * precision);

        return $"{intF}.{decF}";
    }
}