using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraSlide : MonoBehaviour 
{
    private MenuManager menu;

    public float startY;
    public float goalY;
    public float duration;

    private float startTime;

    private bool finished;

	// Use this for initialization
	void Start () 
    {
        menu = FindObjectOfType<MenuManager>();
        menu.gameObject.SetActive(false);

        transform.position = new Vector3(transform.position.x, startY, transform.position.z);

        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () 
    {
        float t = (Time.time - startTime) / duration;
		float y = Mathf.SmoothStep(transform.position.y, goalY, t);

        Vector3 position = new Vector3(transform.position.x, y, transform.position.z);
        transform.position = position;

        // if the thingy made it to its goal, enable the menu 
        if (transform.position.y <= goalY + .1f)
        {
            menu.gameObject.SetActive(true);
        }

        // if the player presses space or enter, skip the animation
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            transform.position = new Vector3(transform.position.x, goalY, transform.position.z);
	}
}
