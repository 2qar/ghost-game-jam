using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyManager : Interactable
{
	GameObject player;
	Rigidbody2D playerRb;
	Animator playerAnimator;
	GameObject playerEyes;

	GameObject interactPrompt;

	SpriteRenderer sr;
	SpriteRenderer eyeSr;

	private bool _occupied;
	private bool occupied
	{
		get { return _occupied; }
		set
		{
			// make all of the enemies angry when the player gets into a body
			if(value)
			{
				GameObject[] enemies = GameObject.FindGameObjectsWithTag("Ghost");
				foreach(GameObject enemy in enemies)
					enemy.GetComponent<EnemyManager>().Angery = true;
			}

			switchBodies(value);
			_occupied = value;
		}
	}

	public override void StartExtra()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		playerRb = player.GetComponent<Rigidbody2D>();
		playerAnimator = player.GetComponent<Animator>();
		playerEyes = player.transform.GetChild(0).gameObject;

		sr = GetComponent<SpriteRenderer>();
		try { eyeSr = transform.GetChild(0).GetComponent<SpriteRenderer>(); }
		catch { print("Eyes missing or in the wrong index"); }

		try { interactPrompt = transform.GetChild(1).gameObject; }
		catch { print("Interact prompt missing or in the wrong index"); }
	}

	public override void UpdateExtra() 
	{
		interactPrompt.SetActive(triggered);
	}

	public override void OnInteraction()
	{
		occupied = true;
	}

	private void switchBodies(bool occupied)
	{
		// load the right animation for the player
		if(!occupied)
		{
			playerEyes.SetActive(false);
			transform.position = player.transform.position;
			playerAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/Player");
		}
		else
		{
			playerEyes.SetActive(true);
			triggered = false;
			playerAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/Human");
		}

		player.transform.position = gameObject.transform.position;
		playerRb.velocity = new Vector2();

		sr.enabled = !occupied;
		eyeSr.enabled = !occupied;

		changeTriggerState();
	}

}
