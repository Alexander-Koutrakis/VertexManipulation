//<summary> rotate gameobject towards mouse input
using UnityEngine;

public class DragRotation : MonoBehaviour,IDragable
{

    private float rotationSpeed=10;


    public void OnBeginDrag(Vector3 position)
    {

    }

    public void OnDrag(Vector3 position)
    {
        float XaxisRotation = Input.GetAxis("Mouse X")* rotationSpeed;
        float YaxisRotation = Input.GetAxis("Mouse Y") * rotationSpeed;
        transform.Rotate(Vector3.down, XaxisRotation,Space.World);
        transform.Rotate(Vector3.right, YaxisRotation,Space.World);
    }
    
    public void OnEndDrag(Vector3 position)
    {
       
    }

 
}
