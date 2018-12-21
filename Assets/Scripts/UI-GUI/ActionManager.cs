using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour {

    public Texture2D crosshair;
    int x = 0;
    private void Update()
    {
        if (Input.GetKeyDown("m") && x == 1)
        {
            x--;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            Debug.Log("Normal");
        }
        else if (Input.GetKeyDown("m") && x == 0)
        {
            x++;
            Cursor.SetCursor(crosshair, Vector2.zero, CursorMode.Auto);
            Debug.Log("Crosshair");
        }
        
    }
}