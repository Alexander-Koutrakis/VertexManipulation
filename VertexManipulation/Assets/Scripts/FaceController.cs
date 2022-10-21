
using UnityEngine;

namespace MeshControl
{
    //<summary>
    // This class is responsible for moving the <param>pointControllers
    // towards the <param>direction
    public class FaceController : MonoBehaviour, IDragable
    {
        //An array of PointControllers that are controlled by this class
        private PointController[] pointControllers;

        // The direction the normals are facing
        private Vector3 direction;

        // Meshrenderer that changes material when selected
        private MeshRenderer meshRenderer;
        private Material yellowMAT;//selected material
        private Material blueMAT;//deselected material

        private float movementSpeed = 10f;

        //the last position of the mouse
        //used to calculate the direction of mouse dragging
        private Vector3 lastPosition;

        //the distance between gameobject and the camera
        //used to calculate the direction of mouse dragging
        private float cameraDistance;

        //<summary/>Initialize parameters
        //rotate transform towards the <param>direction
        //each time a pointController is moved update the tranform position </summary>
        public void Initialize(PointController[] pointControllers, Vector3 direction)
        {
            this.pointControllers = pointControllers;
            this.direction = direction;
            meshRenderer = GetComponent<MeshRenderer>();
            yellowMAT = Resources.Load<Material>("Materials/YellowMAT");
            blueMAT = Resources.Load<Material>("Materials/BlueMAT");

            for (int i = 0; i < pointControllers.Length; i++)
            {
                pointControllers[i].AddOnPointMoveAction(UpdatePosition);
            }

            Vector3 center = pointControllers.Center();
            transform.localPosition = center;
            transform.localRotation = Quaternion.LookRotation(direction);
        }

        //<summary/>When the player starts dragging the mouse
        // select this face controller and cache the first position of the mouse </summary>
        // 
        //<param name = "mousePosition" is the position of the mouse in pixels of the screen>*/
        public void OnBeginDrag(Vector3 mousePosition)
        {
            Select();
            cameraDistance = Vector3.Distance(Camera.main.transform.position, transform.position);
            Ray mouseRay = Camera.main.ScreenPointToRay(mousePosition);
            Vector3 endPoint = mouseRay.GetPoint(cameraDistance);
            lastPosition = endPoint;
        }

        //<summary/>While dragging get the input direction between -1 to 1
        // negative to move the <param> pointControllers oposite of the <param> direction
        // postive to move towards</summary>
        // 
        //<param name = "mousePosition" is the position of the mouse in pixels of the screen>
        public void OnDrag(Vector3 mousePosition)
        {
            float input = InputDirection(mousePosition);
            Move(input);
        }

        //<summary>Deselect the face Controller
        // 
        //<param name = "mousePosition" is the position of the mouse in pixels of the screen>
        public void OnEndDrag(Vector3 mousePosition)
        {
            Deselect();
        }

        //<summary/>Change the material of the faceController meshRenderer
        //and each meshRenderer of the point controllers to appear as selected "yellow"</summary>
        private void Select()
        {
            meshRenderer.material = yellowMAT;
            for (int i = 0; i < pointControllers.Length; i++)
            {
                pointControllers[i].Select();
            }
        }

        //<summary/>Change the material of the faceController meshRenderer
        // and each meshRenderer of the point controllers to appear as deselected "blue"</summary>
        private void Deselect()
        {
            meshRenderer.material = blueMAT;
            for (int i = 0; i < pointControllers.Length; i++)
            {
                pointControllers[i].Deselect();
            }
        }

        //<summary>Move each pointController towards or opposite of the <param>direction
        private void Move(float input)
        {
            for (int i = 0; i < pointControllers.Length; i++)
            {
                pointControllers[i].transform.localPosition += direction * input * movementSpeed * Time.deltaTime;
                pointControllers[i].Move();
            }
        }


        //<summary/>move the transform to the center of its <param> pointControllers </summary>
        public void UpdatePosition()
        {
            transform.localPosition = pointControllers.Center();
        }

        //<summary>
        // Get the direction of the mouse drag ,get the direction of the normals
        // if the angle between the two directions is less than 90 degrees then we get positive
        // input else its negative </summary>
        private float InputDirection(Vector3 mousePosition)
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(mousePosition);
            Vector3 endPoint = mouseRay.GetPoint(cameraDistance);
            Vector3 mouseDirection = endPoint - lastPosition;
            Vector3 lookDirection = transform.forward;
            float angle = Vector3.Angle(lookDirection, mouseDirection);
            float inputMagnitude = GetInputMagnitude();
            lastPosition = endPoint;

            if (angle <= 90)
            {
                return inputMagnitude;
            }
            else
            {
                return -inputMagnitude;
            }
        }

        //<summary> Get the magnitude of the mouse movement
        private float GetInputMagnitude()
        {
            float Xaxis = Input.GetAxis("Mouse X");
            float Yaxis = Input.GetAxis("Mouse Y");
            Xaxis = Mathf.Abs(Xaxis);
            Yaxis = Mathf.Abs(Yaxis);
            if (Xaxis >= Yaxis)
            {
                return Xaxis;
            }
            else
            {
                return Yaxis;
            }
        }
    }
}
