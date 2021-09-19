using System.Globalization;
using UnityEngine;

public static class Bitf
{
    public static int Int(string str, int or)
    {
        int n;
        return int.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out n) ? n : or;
    }

    public static float Float(string str, float or)
    {
        float n;
        return float.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out n) ? n : or;
    }

    public static long Long(string str, long or)
    {
        long n;
        return long.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out n) ? n : or;
    }

    public static string Str(float f, int precision)
    {
        precision = (int) Mathf.Pow(10, precision);

        var intF = (int) f;
        var decimalF = (int) Mathf.Abs((f - intF) * precision);

        return $"{intF}.{decimalF}";
    }

    public static float Round(float f, int precision)
    {
        precision = (int) Mathf.Pow(10, precision);

        return Mathf.Round(f * precision) / precision;
    }
}