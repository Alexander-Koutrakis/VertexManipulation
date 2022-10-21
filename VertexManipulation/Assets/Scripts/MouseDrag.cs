
using UnityEngine;

//<summary> give input to all dragambles of this gameobject
public class MouseDrag : MonoBehaviour
{
    private IDragable[] dragables;
    private bool dragging;
    private void Awake()
    {
        dragables = GetComponents<IDragable>();
    }

    private void OnMouseDown()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        for(int i = 0; i < dragables.Length; i++)
        {
            dragables[i].OnBeginDrag(mouseScreenPosition);
        }
        dragging = true;     
    }

    private void OnMouseDrag()
    {
        if (dragging)
        {
            Vector3 mouseScreenPosition = Input.mousePosition;
            for (int i = 0; i < dragables.Length; i++)
            {
                dragables[i].OnDrag(mouseScreenPosition);
            }
        }
    }

    private void OnMouseUp()
    {
        if (dragging)
        {
            Vector3 mouseScreenPosition = Input.mousePosition;
            for (int i = 0; i < dragables.Length; i++)
            {
                dragables[i].OnEndDrag(mouseScreenPosition);
            }
        }
    }

}
