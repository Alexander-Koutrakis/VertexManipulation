
using UnityEngine;
using System;

namespace MeshControl
{
    //<summary>Control a point of the mesh by draging the gameobject with the mouse
    public class PointController : MonoBehaviour, IDragable
    {
        private Point point;
        private MeshRenderer meshRenderer;
        private Material yellowMAT;
        private Material blueMAT;
        private float cameraDistance;
        private Action onPointMove;

        public void Initialize(Point newPoint)
        {
            point = newPoint;
            meshRenderer = GetComponent<MeshRenderer>();
            yellowMAT = Resources.Load<Material>("Materials/YellowMAT");
            blueMAT = Resources.Load<Material>("Materials/BlueMAT");
        }

        //<summary> change the material when the cotroller is selected
        public void OnBeginDrag(Vector3 mousePosition)
        {

            Select();
            cameraDistance = Vector3.Distance(Camera.main.transform.position, transform.position);
        }

        //<summary>get mouse world position by drawing a ray with the length of the distance from
        // the camera to the gameobject and position the mouse postion
        public void OnDrag(Vector3 mousePosition)
        {
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            Vector3 endPoint = ray.GetPoint(cameraDistance);
            transform.position = endPoint;
            Move();
        }

        //<summary> change the material when the cotroller is deselected
        public void OnEndDrag(Vector3 mousePosition)
        {
            Deselect();
        }

        public void Select()
        {
            meshRenderer.material = yellowMAT;
        }


        public void Deselect()
        {
            meshRenderer.material = blueMAT;
        }

        public void Move()
        {
            point.MovePoint(transform.localPosition);
            onPointMove?.Invoke();
        }

        public void AddOnPointMoveAction(Action action)
        {
            onPointMove += action;
        }

        public void RemoveOnPointMoveAction(Action action)
        {
            onPointMove -= action;
        }
    }
}
