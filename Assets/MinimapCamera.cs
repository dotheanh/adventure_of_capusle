using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    public Transform player;
    private bool isLockedPosition = false;
    private Camera cam;
    
    void Start()
    {
        cam = this.GetComponent<Camera>();
    }

    // LatUpdate called after Update()
    void LateUpdate()
    {
        if (!isLockedPosition)
        {
            Vector3 newPosition = player.position;
            newPosition.y = transform.position.y;
            newPosition.x += 1; // hard code fix
            transform.position = newPosition;
        }
    }

    public void ToggleLockMiniMapPosition()
    {
        isLockedPosition = !isLockedPosition;

        if (isLockedPosition)
        {
            transform.position = new Vector3(1, 30, 0);
        }
    }

    public void ZoomIn()
    {
        cam.orthographicSize -= 5;
        if (cam.orthographicSize < 0) cam.orthographicSize = 0;
    }
    public void ZoomOut()
    {
        cam.orthographicSize += 5;
    }
}
