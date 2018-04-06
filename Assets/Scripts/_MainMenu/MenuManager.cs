using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour 
{
    private Transform selector;
    private Text[] options;
    private int _optionsIndex;
    private int optionsIndex
    {
        get { return _optionsIndex; }
        set
        {
            selector.transform.position = new Vector2(selector.transform.position.x, options[value].transform.position.y - 3);
            _optionsIndex = value;
        }
    }

	// Use this for initialization
	void Start () 
    {
		initializeOptions();
	}
	
    /// <summary>
    /// Get each text object in the menu and the selector.
    /// </summary>
    private void initializeOptions()
    {
        // get all of the menu options and their positions
        options = new Text[transform.childCount - 1];
        for (int index = 0; index < options.Length; index++)
            options[index] = transform.GetChild(index).GetComponent<Text>();

        // if the player doesn't have any save data, change continue text to new game
        if (!System.IO.File.Exists(SaveLoadManager.dataPath))
            options[0].text = "new game";

        // get the selector
        selector = transform.GetChild(transform.childCount - 1);
    }

	// Update is called once per frame
	void Update () 
    {
        moveSelector();
        selectOption();
	}

    /// <summary>
    /// Move the selector based on user input.
    /// </summary>
    void moveSelector()
    {
        // move the thingy down
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            if (optionsIndex == options.Length - 1) optionsIndex = 0;
            else optionsIndex++;
        // move the thingy up
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            if (optionsIndex == 0) optionsIndex = options.Length - 1;
            else optionsIndex--;
    }

    /// <summary>
    /// do a lil thingy based on which option the player scronches
    /// </summary>
    void selectOption()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            if (optionsIndex == 0) SceneManager.LoadScene("GAME");
            else if (optionsIndex == 1) Debug.Log("options");
            else if (optionsIndex == 2) Application.Quit();
    }

}
