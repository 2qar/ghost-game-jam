using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Camera))]
public class CameraFollower : MonoBehaviour 
{
    // The gameobject for the camera to follow
    public GameObject objectToFollow;

    [HideInInspector]
    public int cameraSize = 36;

    // How long it should take for the camera to get from it's current position to the player
    public float smoothTime;

    float currentVelocityX;
    float currentVelocityY;

    float cameraVelocity;

    Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        followObject(objectToFollow.transform.position);

        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, cameraSize, ref cameraVelocity, smoothTime);
    }

    /// <summary>
    /// Makes the camera smoothly follow a given target.
    /// </summary>
    /// <param name="target">
    /// Target object to follow.
    /// </param>
    private void followObject(Vector2 target)
    {
        // x and y positions
        float[] positions = smoother(target);
        //if (target != null)
        transform.position = new Vector3(positions[0], positions[1], -10);
    }

    /// <summary>
    /// Gets a smoothened x and y position between the camera and the given target.
    /// </summary>
    /// <returns>
    /// If the target object exists, it returns an x and y position for the camera to use.
    /// If the target object has been destroyed or hasn't been set, it returns an empty float array.
    /// </returns>
    /// <param name="target">
    /// Target object to follow.
    /// </param>
    private float[] smoother(Vector2 target)
    {
        // if the target object exists,
        try
        {
            // return x and y positions for the camera to use
            return new float[]
            {
                // x position
                Mathf.SmoothDamp(transform.position.x, target.x, ref currentVelocityX, smoothTime),
                // y position
                Mathf.SmoothDamp(transform.position.y, target.y, ref currentVelocityY, smoothTime)
            };
        }
        catch(Exception e)
        {
            Debug.Log(e);
            return new float[0];
        }
    }

    public void ghostConversation(GameObject focusPoint)
    {
        cameraSize /= 2;
        objectToFollow = focusPoint;
        smoothTime = 1;
    }

    public void endGhostConversation(GameObject focusPoint)
    {
        cameraSize *= 2;
        Destroy(focusPoint);
        objectToFollow = GameObject.FindGameObjectWithTag("Player");
        smoothTime = .23f;
    }

}
