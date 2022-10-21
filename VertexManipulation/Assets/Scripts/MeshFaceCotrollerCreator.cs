
using System.Collections.Generic;
using UnityEngine;

namespace MeshControl
{
    //<summary/>this class is responsible for creating 
    // faceControllers for each face of the mesh
    public static class MeshFaceCotrollerCreator
    {
        //<summary>Create all faces by getting all mesh triangles first
        // and group them by the normals direction
        public static void CreateFaces(Mesh mesh,MeshController meshController)
        {
            Transform parentTransform = meshController.transform;
            Triangle[] allTriangles = CreateTriangles(mesh);
            Dictionary<Vector3, List<Triangle>> groupedByNormals = GroupByNormals(allTriangles);
            foreach (Vector3 normal in groupedByNormals.Keys)
            {
                CreateFacesFromSameNormalsTriangles(groupedByNormals[normal], normal,parentTransform);
            }
        }

        //<summary>Create cache all mesh triangles into a <class> Triangle
        private static Triangle[] CreateTriangles(Mesh mesh)
        {
            int[] trianglesINT = mesh.triangles;
            Vector3[] normals = mesh.normals;
            Vector3[] vertices = mesh.vertices;

            int alltrianglesLength = (trianglesINT.Length / 3);
            Triangle[] allTriangles = new Triangle[alltrianglesLength];
            int index = 0;

            for (int i = 0; i < trianglesINT.Length; i += 3)
            {
                Vector3[] triangleVertices = new Vector3[3];
                Vector3 normal = normals[trianglesINT[i]];
                triangleVertices[0] = vertices[trianglesINT[i]];
                triangleVertices[1] = vertices[trianglesINT[i + 1]];
                triangleVertices[2] = vertices[trianglesINT[i + 2]];

                Triangle triangle = new Triangle(triangleVertices, normal);

                allTriangles[index] = triangle;
                index++;
            }

            return allTriangles;
        }

        //<summary> Group all triangles by the direction of their normals
        private static Dictionary<Vector3, List<Triangle>> GroupByNormals(Triangle[] allTriangles)
        {
            Dictionary<Vector3, List<Triangle>> groupTrianglesByNormals = new Dictionary<Vector3, List<Triangle>>();
            for (int i = 0; i < allTriangles.Length; i++)
            {
                Vector3 normal = allTriangles[i].Normal;
                if (groupTrianglesByNormals.ContainsKey(normal))
                {
                    groupTrianglesByNormals[normal].Add(allTriangles[i]);
                }
                else
                {
                    List<Triangle> triangles = new List<Triangle>();
                    triangles.Add(allTriangles[i]);
                    groupTrianglesByNormals.Add(normal, triangles);
                }
            }
            return groupTrianglesByNormals;
        }

        //<summary>
        //Check whitch triangles are connected with at least 2 points and group them
        // create a faceController for each group
        //
        //<param name="triangles" is a list of triangles with the same normals>
        //<param name="normal" is the normal for all of the triangles>
        private static void CreateFacesFromSameNormalsTriangles(List<Triangle> triangles, Vector3 normal,Transform parentTransform)
        {
            GameObject faceGOprefab = Resources.Load<GameObject>("GameObjects/FaceControllerGO");
            PointController[] pointControllers = parentTransform.GetComponentsInChildren<PointController>();
            //while the list is not empty try to group triangles
            while (triangles.Count > 0)
            {

                List<Triangle> connectedTriangles = new List<Triangle>();
                connectedTriangles.Add(triangles[0]);

                for (int i = 0; i < triangles.Count; i++)
                {
                    if (!connectedTriangles.Contains(triangles[i]))
                    {
                        bool connect = false;
                        for (int j = 0; j < connectedTriangles.Count; j++)
                        {
                            if (connectedTriangles[j].Connected(triangles[i]))
                            {
                                connect = true;
                                break;
                            }
                        }

                        if (connect)
                        {
                            connectedTriangles.Add(triangles[i]);
                        }
                    }
                }

                //create faceController for the tiangle group
                CreateFaceController(connectedTriangles, normal, pointControllers,parentTransform,faceGOprefab);

                //remove the grouped triangles from the total
                for (int j = 0; j < connectedTriangles.Count; j++)
                {
                    triangles.Remove(connectedTriangles[j]);
                }
            }
        }

        //Create faceController for the tiangle group
        //<param name="triangles" thelist of triangles of the face>
        //<param name="normalsDirection" the normal for all of the triangles>
        //<param name="pointControllers" all PointControllers of the mesh>
        //<param name="parentTransform" mesh transform>
        //<param name="parentTransform" the prefab of FaceController>
        private static void CreateFaceController(List<Triangle> triangles, Vector3 normalsDirection, PointController[] pointControllers,Transform parentTransform,GameObject faceGOprefab)
        {
            List<PointController> facePointControllersList = new List<PointController>();
            for (int i = 0; i < triangles.Count; i++)
            {
                for (int j = 0; j < pointControllers.Length; j++)
                {
                    if (!facePointControllersList.Contains(pointControllers[j]))
                    {
                        if (triangles[i].HasVerticeAtPosition(pointControllers[j].transform.localPosition))
                        {
                            facePointControllersList.Add(pointControllers[j]);
                        }
                    }
                }
            }
            PointController[] facePointControllers = facePointControllersList.ToArray();
            GameObject newFaceGO = GameObject.Instantiate(faceGOprefab, parentTransform);

            FaceController face = newFaceGO.GetComponent<FaceController>();
            face.Initialize(facePointControllers, normalsDirection);
        }
    }
}
