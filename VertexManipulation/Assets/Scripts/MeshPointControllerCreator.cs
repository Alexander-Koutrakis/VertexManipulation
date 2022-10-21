
using System.Collections.Generic;
using UnityEngine;

namespace MeshControl
{
    //<summary>Create point controllers for each vertex point of a mesh
    public static class MeshPointControllerCreator
    {

        //<summary>create pointControllers for each point of the mesh
        public static void CreatePoints(Mesh mesh, MeshController meshController)
        {
            Vector3[] vertices = mesh.vertices;
            List<Vector3> usedVertices = new List<Vector3>();
            for (int i = 0; i < vertices.Length; i++)
            {
                if (!usedVertices.Contains(vertices[i]))
                {
                    Vector3 position = vertices[i];
                    //group vertices of the same position
                    int[] similarVerticesIndexes = FindSimilarPoints(vertices, position);
                    //create a point for each group
                    CreatePoint(similarVerticesIndexes, position, meshController);
                    usedVertices.Add(vertices[i]);
                }
            }
        }

        //<summary>Find all vertices with the same position as <param>vertexPosition
        private static int[] FindSimilarPoints(Vector3[] vertices, Vector3 vertexPosition)
        {
            List<int> similarVerticesList = new List<int>();
            for (int i = 0; i < vertices.Length; i++)
            {
                if (vertices[i] == vertexPosition)
                {
                    similarVerticesList.Add(i);
                }
            }
            return similarVerticesList.ToArray();
        }

        private static void CreatePoint(int[] verticesIndexes, Vector3 position, MeshController meshController)
        {
            Transform meshTransform = meshController.transform;
            GameObject PointControllerGO = Resources.Load<GameObject>("GameObjects/PointControllerGO");
            GameObject newPointControllerGO = GameObject.Instantiate(PointControllerGO, meshTransform);
            newPointControllerGO.transform.localPosition = position;

            Point point = new Point(verticesIndexes, position, meshController);
            PointController pointController = newPointControllerGO.GetComponent<PointController>();
            pointController.Initialize(point);
        }

    }
}
