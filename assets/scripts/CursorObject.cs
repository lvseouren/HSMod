using UnityEngine;

public class CursorObject : MonoBehaviour
{
    public Texture2D cursorTexture1;
    public Texture2D cursorTexture2;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    void Start()
    {
        Cursor.SetCursor(cursorTexture1, hotSpot, cursorMode); // initialise default state of cursor
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) // When clicking, depending on current state, change the state
            Cursor.SetCursor(cursorTexture2, hotSpot, cursorMode);
        else
            Cursor.SetCursor(cursorTexture1, hotSpot, cursorMode);
    }
}