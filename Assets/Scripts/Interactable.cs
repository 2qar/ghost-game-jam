using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour 
{
	private const string PLAYER = "Player";

	GhostTalk player;

	public BoxCollider2D triggerAreaBox;
	public CircleCollider2D triggerAreaCircle;

	public bool triggered { get; protected set; }
	public bool useCircle;

	// Use this for initialization
	void Start () 
	{
		player = GhostTalk.instance;

		// try to get the trigger collider of choice and make it a trigger
		if(!useCircle)
		{
			try { triggerAreaBox = GetComponent<BoxCollider2D>(); triggerAreaBox.isTrigger = true; }
			catch { print("error getting circle collider"); }
		}
		else
		{
			try { triggerAreaCircle = GetComponent<CircleCollider2D>(); triggerAreaCircle.isTrigger = true;}
			catch { print("error getting box collider"); }
		}

		StartExtra();
	}

	public abstract void StartExtra();

	// Update is called once per frame
	void Update () 
	{
		inputCheck();
		UpdateExtra();

		if(player.talkingToGhost)
			triggered = false;
	}

	public abstract void UpdateExtra();

	public abstract void OnInteraction();

	/// <summary>
	/// Enables or disables the trigger area.
	/// </summary>
	public void changeTriggerState()
	{
		if(useCircle)
			triggerAreaCircle.enabled = !triggerAreaCircle.enabled;
		else
			triggerAreaBox.enabled = !triggerAreaBox.enabled;
	}

	public void inputCheck()
	{
		if(Input.GetKeyDown(KeyCode.E) && triggered)
			OnInteraction();
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.gameObject.tag.Equals(PLAYER))
			triggered = true;
	}

	private void OnTriggerExit2D(Collider2D collider)
	{
		if(collider.gameObject.tag.Equals(PLAYER))
			triggered = false;
	}

}
