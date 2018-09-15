using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

struct Triangle
{
    public Vector2 A, B, C;
    public Triangle(Vector2 a, Vector2 b, Vector2 c)
    {
        A = a; B = b; C = c;
    }
}
class NoteSliderObject : MonoBehaviour
{
    private Triangle triangle;
    private Mesh mesh;
    public void SetTriangle(float x, float y, float width, float height)
    {
        triangle = new Triangle(new Vector2(0, height), new Vector2(width / 2, 0), new Vector2(width, height));
        mesh = new Mesh();
        mesh.SetVertices(new List<Vector3>() { triangle.A / new Vector2(width, height), triangle.B / new Vector2(width, height), triangle.C / new Vector2(width, height) });

        MeshCollider collider = GetAddComponent<MeshCollider>();
        collider.sharedMesh = mesh;
        MeshRenderer renderer = GetAddComponent<MeshRenderer>();
        MeshFilter filter = GetAddComponent<MeshFilter>();
        filter.mesh = mesh;
        filter.sharedMesh = mesh;
    }
    private T GetAddComponent<T>() where T : Component
    {
        T t;
        if ((t = GetComponent<T>()) == null)
            t = gameObject.AddComponent<T>();
        return t;
    }
    public void Update()
    {
            
    }
}
