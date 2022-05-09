using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    public Texture2D handCursor;
    public Texture2D normalCursor;
    private Vector2 normalOffset = new Vector2(24, 16);
    private Vector2 handOffset = new Vector2(18, 20);

    void Start()
    {
        Cursor.SetCursor(normalCursor, normalOffset, CursorMode.Auto);
    }

    void Update()
    {
        // Hand if hover over interactable sprite
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPos;

        RaycastHit2D hit = Physics2D.Raycast(cursorPos, Vector2.zero);
        Cursor.SetCursor(normalCursor, normalOffset, CursorMode.Auto);

        if (hit.collider != null)
        {
            updateObject(hit.collider.gameObject);
        }
    }

    void updateObject(GameObject go)
    {
        if (ObjectClick.ActiveNode == go.GetComponent<NodeInfo>().NodeNumber)
        {
            Cursor.SetCursor(handCursor, handOffset, CursorMode.Auto);
        }
    }
}