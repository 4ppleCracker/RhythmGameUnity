using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct Triangle
{
    public Vector2 A, B, C;
    public Triangle(Vector2 a, Vector2 b, Vector2 c)
    {
        A = a; B = b; C = c;
    }

    public Vector2 Middle => (A + B + C) / 3;
    public float Width {
        get {
            var list = new List<float>() { A.x, B.x, C.x };
            list.Sort((a, b) => a.CompareTo(b));
            return list.Last();
        }
    }
    public float Height {
        get {
            var list = new List<float>() { A.y, B.y, C.y };
            list.Sort((a, b) => a.CompareTo(b));
            return list.First();
        }
    }
    public float WidthAt(int x)
    {
        var length = (Height - x) / Height;
        return Width * length;
    }

    public static Triangle operator -(Triangle a, Triangle b)
    {
        return new Triangle(a.A - b.A, a.B - b.B, a.C - b.C);
    }
    public static Triangle operator -(Triangle a, Vector2 b)
    {
        return a - new Triangle(b, b, b);
    }
    public static Triangle operator +(Triangle a, Triangle b)
    {
        return new Triangle(a.A + b.A, a.B + b.B, a.C + b.C);
    }
    public static Triangle operator +(Triangle a, Vector2 b)
    {
        return a + new Triangle(b, b, b);
    }
    public static Triangle operator *(Triangle a, Triangle b)
    {
        return new Triangle(a.A * b.A, a.B * b.B, a.C * b.C);
    }
    public static Triangle operator *(Triangle a, Vector2 b)
    {
        return a * new Triangle(b, b, b);
    }
    public static Triangle operator *(Triangle a, float b)
    {
        return a * new Vector2(b, b);
    }
}
