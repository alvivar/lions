using System;
using System.Collections.Generic;
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

    public static long Long(byte[] bigEndian)
    {
        bigEndian = SubArray(bigEndian, 0, 8);

        if (BitConverter.IsLittleEndian)
            Array.Reverse(bigEndian);

        return BitConverter.ToInt64(bigEndian, 0);
    }

    public static string Str(float f, int precision)
    {
        precision = (int)Mathf.Pow(10, precision);

        var intf = (int)f;
        var decf = (int)Mathf.Abs((f - intf) * precision);

        if (decf == 0)
            return $"{intf}";

        return $"{intf}.{decf}";
    }

    public static string Str(byte[] f)
    {
        return System.Text.Encoding.UTF8.GetString(f, 0, f.Length);
    }

    public static float Round(float f, int precision)
    {
        precision = (int)Mathf.Pow(10, precision);

        return Mathf.Round(f * precision) / precision;
    }

    public static float[] Floats(string str)
    {
        var numbers = new List<float>();

        foreach (var s in str.Split(' ', ','))
        {
            float n;
            if (float.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out n))
                numbers.Add(n);
        }

        return numbers.ToArray();
    }

    private static T[] SubArray<T>(this T[] data, int index, int length)
    {
        T[] result = new T[length];
        Array.Copy(data, index, result, 0, length);

        return result;
    }
}