
using UnityEngine;

namespace MeshControl
{
    //<summary> Generate point and face controllers to move around the mesh vertices
    public class MeshController : MonoBehaviour
    {
        private Vector3[] vertices;
        private Mesh mesh;
        private MeshCollider meshCollider;

        private void Awake()
        {
            mesh = GetComponent<MeshFilter>().mesh;
            meshCollider = GetComponent<MeshCollider>();
        }
        private void Start()
        {
            Initialize();
        }
        private void Initialize()
        {
            vertices = mesh.vertices;
            MeshPointControllerCreator.CreatePoints(mesh,this);
            MeshFaceCotrollerCreator.CreateFaces(mesh, this);
        }
        private void UpdateVertices()
        {
            mesh.vertices = vertices;
            mesh.RecalculateNormals();
            meshCollider.sharedMesh = mesh;
        }


        //<summary>move the specific vertices to position
        //<param name = "verticesIndexes" the indexes of the vertices that will be moved>
        //<param name = "position" target position>
        public void MoveVertices(int[] verticesIndexes, Vector3 position)
        {
            for (int i = 0; i < verticesIndexes.Length; i++)
            {
                vertices[verticesIndexes[i]] = position;
            }
            UpdateVertices();
        }

    }
}
