
using UnityEngine;

public interface IDragable 
{
    void OnBeginDrag(Vector3 position);
    void OnDrag(Vector3 position);
    void OnEndDrag(Vector3 position);
}
