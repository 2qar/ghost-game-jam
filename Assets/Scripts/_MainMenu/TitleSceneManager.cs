using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneManager : MonoBehaviour 
{
    private bool ghost = true;

    public GameObject ground;
    Vector3 startPos;

	// Use this for initialization
	void Start () 
    {
        startPos = ground.transform.position;

        // if the player has no savedata, show a human instead of a ghost
        if (!System.IO.File.Exists(SaveLoadManager.dataPath))
            ghost = false;
	}

    private void FixedUpdate()
    {
        // make the ground seemingly infinitely move to the left
        ground.transform.position -= new Vector3(.2f, 0, 0);
        if (ground.transform.position.x <= -8)
            ground.transform.position = startPos;
    }

}
