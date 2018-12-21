using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamController : MonoBehaviour
{
    public Transform target;
    public float speed = 1f;
    
    private bool startMoving;
    private Text debug;
    private float zoomfactor = 5f;

    Vector3 direction;
    Vector3 direction2;
    Vector2 mouseScrollDelta;


    void Update()
    {
        //update camera
        if (startMoving)
        {
            target.position += direction;
        }

        //resetCam
        if (Input.GetKeyDown("space"))
        {
            target.transform.rotation = Quaternion.Euler(0, 45, 0);
            Camera.main.orthographicSize = 5;
        }

            //Zoom
            mouseScrollDelta = Input.mouseScrollDelta;
        if (mouseScrollDelta[1] < 0 && Camera.main.orthographicSize < 20)
        {
            Camera.main.orthographicSize += zoomfactor;
        }
        else if (mouseScrollDelta[1] == 0);
        else
        {
            if ((Camera.main.orthographicSize - zoomfactor) > 0 && mouseScrollDelta[1] > 0)
            {
                Camera.main.orthographicSize -= zoomfactor;
            }
        }

        //ChangePOV
        if (Input.GetKeyDown("home"))
        {
            var rot = target.transform.rotation;
            target.transform.rotation = rot * Quaternion.Euler(0, 90, 0);
        }
        if (Input.GetKeyDown("end"))
        {
            var rot = target.transform.rotation;
            target.transform.rotation = rot * Quaternion.Euler(0, -90, 0);
        }

        //KeyBoardInputControle
        if (Input.GetKeyDown("w") || Input.GetKeyDown("up"))
        {
            startMoving = true;
            direction += target.forward * speed;
        }
        if (Input.GetKeyDown("a") || Input.GetKeyDown("left"))
        {
            startMoving = true;
            direction -= target.right * speed;
        }
        if (Input.GetKeyDown("s") || Input.GetKeyDown("down"))
        {
            startMoving = true;
            direction -= target.forward * speed;
        }
        if (Input.GetKeyDown("d") || Input.GetKeyDown("right"))
        {
            startMoving = true;
            direction += target.right * speed;
        }

        if (Input.GetKeyUp("w") || Input.GetKeyUp("up"))
        {
            direction -= target.forward * speed;
        }
        if (Input.GetKeyUp("a") || Input.GetKeyUp("left"))
        {
            direction += target.right * speed;
        }
        if (Input.GetKeyUp("s") || Input.GetKeyUp("down"))
        {
            direction += target.forward * speed;
        }
        if (Input.GetKeyUp("d") || Input.GetKeyUp("right"))
        {
            direction -= target.right * speed;
        }
    }

    public void StartMovingForward(bool opposite)
    {
        startMoving = true;
        direction = (!opposite) ? target.forward : -target.forward;
        direction *= speed;
    }

    public void StartMovingSides(bool opposite)
    {
        startMoving = true;
        direction = (!opposite) ? target.right : -target.right;
        direction *= speed;
    }

    public void StartMovingForwardSide(bool opposite)
    {
        startMoving = true;
        direction = (!opposite) ? target.forward : -target.forward;
        direction2 = (!opposite) ? target.right : -target.right;
        direction = direction + direction2;
        direction *= speed;
    }

    public void StartMovingBottomSide(bool opposite)
    {
        startMoving = true;
        direction = (!opposite) ? target.forward : -target.forward;
        direction2 = (!opposite) ? target.right : -target.right;
        direction = direction - direction2;
        direction *= speed;
    }

    public void StopMoving()
    {
        startMoving = false;
        direction = new Vector3(0, 0, 0);
    }
}