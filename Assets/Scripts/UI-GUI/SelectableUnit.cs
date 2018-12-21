using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class SelectableUnit : MonoBehaviour
{
    public GameObject selectionCircle;

    public Transform target;
    float speed = 0.5f;
    Vector3[] path;
    int targetIndex;
    BoardManager boardManager;
    private void Start()
    {
        GameObject gameManagerGO = GameObject.FindGameObjectWithTag("GameManager");
        boardManager = gameManagerGO.GetComponent<BoardManager>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        { if (transform.position.x != boardManager.mousePos.x || transform.position.z != boardManager.mousePos.z)
                PathRequestManager.RequestPath(transform.position, boardManager.mousePos, OnPathFound);
        }
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            targetIndex = 0;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];
        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }
            currentWaypoint.y = 1;
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed);
            yield return null;

        }
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}