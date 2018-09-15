using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class NoteSliderObject : MonoBehaviour
{
    public int Slice { get; set; }

    private MeshRenderer m_renderer;
    private MeshRenderer Renderer {
        get {
            if (m_renderer != null)
                return m_renderer;
            return m_renderer = GetAddComponent<MeshRenderer>();
        }
    }
    private MeshFilter m_meshFilter;
    private MeshFilter Filter {
        get {
            if (m_meshFilter != null)
                return m_meshFilter;
            return m_meshFilter = GetAddComponent<MeshFilter>();
        }
    }
    private MeshCollider m_meshCollider;
    private MeshCollider Collider {
        get {
            if (m_meshCollider != null)
                return m_meshCollider;
            return m_meshCollider = GetAddComponent<MeshCollider>();
        }
    }
    private Mesh Mesh {
        get {
            return Filter.mesh;
        }
    }
    public Triangle Triangle {
        get {
            return new Triangle(Mesh.vertices[0], Mesh.vertices[1], Mesh.vertices[2]);
        }
        set {
            SetTriangle(value);
        }
    }
    public Material Material { get { return Renderer.material; } set { Renderer.material = value; } }
    public void SetTriangle(Triangle triangle)
    {
        Mesh.Clear();
        Mesh.vertices = new Vector3[] { triangle.A, triangle.B, triangle.C };
        Mesh.triangles = new int[] { 0, 2, 1 };
        Collider.sharedMesh = Mesh;
    }
    private T GetAddComponent<T>() where T : Component
    {
        T t;
        if ((t = GetComponent<T>()) == null)
            t = gameObject.AddComponent<T>();
        return t;
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawRay(Vector2.zero, new Vector2(Triangle.Width, 0));
        Gizmos.DrawRay(Vector2.zero, new Vector2(0, Triangle.Height));
    }
}
