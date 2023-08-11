using UnityEngine;

public class CursorManager : MonoBehaviour
{
    private void Update()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPos;

        Cursor.visible = false;
    }
}