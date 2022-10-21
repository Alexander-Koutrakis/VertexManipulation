//<sumary>change the cursor icon when hovering over or clicking
using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField] private CursorTexturePalette cursorTexturePalette;
    private static bool startClicking;
    private void OnMouseEnter()
    {
        if (!startClicking)
        {
            Cursor.SetCursor(cursorTexturePalette.CanClickTexture, Vector2.zero, CursorMode.Auto);
        }
    }

    private void OnMouseExit()
    {
        if (!startClicking)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }

    private void OnMouseDown()
    {
        Cursor.SetCursor(cursorTexturePalette.ClickingTexture, Vector2.zero, CursorMode.Auto);
        startClicking = true;
    }

    private void OnMouseUp()
    {
        if (startClicking)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }

        startClicking = false;
    }
}
