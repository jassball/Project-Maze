using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GodModeGuideLine : MonoBehaviour
{
    public Transform target;  // Set this to the point in your maze
    public GameObject player;  // Reference to your player

    private NavMeshPath path;
    private bool guideMode = false;
    private LineRenderer lineRenderer;

    void Start()
    {
        path = new NavMeshPath();
        lineRenderer = GetComponent<LineRenderer>();
        // Set up the LineRenderer
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));  // Set material to a default one
        lineRenderer.startColor = Color.blue;
        lineRenderer.endColor = Color.blue;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            guideMode = !guideMode;  // Toggle guide mode
        }

        if (guideMode)
        {
            NavMesh.CalculatePath(player.transform.position, target.position, NavMesh.AllAreas, path);
            DrawPath();
        }
        else
        {
            lineRenderer.positionCount = 0;  // Clear the line when guide mode is off
        }
    }

    void DrawPath()
    {
        if (path.corners.Length < 2) // if the path has 1 or no corners, there is no need to draw.
            return;

        lineRenderer.positionCount = path.corners.Length;
        lineRenderer.SetPositions(path.corners);  // Set the positions of the LineRenderer to the corners of the path
    }
}
