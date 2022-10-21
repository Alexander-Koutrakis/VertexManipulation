
//<summary>Traingles represents a mesh triangles and its facing
using UnityEngine;
public class Triangle
{
    public Vector3[] Vertices { private set; get; }
    public Vector3 Normal { private set; get; }
    public Triangle( Vector3[] vertices, Vector3 normal)
    {
        this.Normal = normal;
        this.Vertices = vertices;
    }

    //Check if triangle is connected with another triangle by checking the vertices positions
    public bool Connected(Triangle triangle)
    {
        int connections = 0;
        for (int i = 0; i < Vertices.Length; i++)
        {
            for (int j = 0; j < triangle.Vertices.Length; j++)
            {
                if (Vertices[i] == triangle.Vertices[j])
                {
                    connections++;
                    break;
                }
            }
        }

        if (connections >= 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Check if the triangle has a vertex on the position
    public bool HasVerticeAtPosition(Vector3 position)
    {
        for (int i = 0; i < Vertices.Length; i++)
        {

            if (Vertices[i] == position)
            {
                return true;
            }
        }

        return false;
    }
}