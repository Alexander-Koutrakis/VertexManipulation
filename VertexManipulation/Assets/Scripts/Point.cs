
using UnityEngine;
namespace MeshControl
{
    //<summary> This script represents a group of vertices
    // that exist in the same posiition in the world
    public class Point
    {
        private int[] verticesIndexes;
        private Vector3 position;
        private MeshController meshController;

        public Point(int[] verticesIndexes, Vector3 position, MeshController meshController)
        {
            this.position = position;
            this.verticesIndexes = verticesIndexes;
            this.meshController = meshController;
        }

        public void MovePoint(Vector3 newPosition)
        {
            position = newPosition;
            meshController.MoveVertices(verticesIndexes, position);
        }

    }
}
