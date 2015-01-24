﻿using UnityEngine;
using System.Collections;

public class SprayBottle : MonoBehaviour {
	public bool holdable = true;
	public bool beingHeld;
	public CharacterMovement holder;
	public float sprayCoolDown = 1f;
	public float droppedCoolDown = 1f;
	public float dropSpeed = 10f;
	private float coolDownTimer;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!beingHeld) {
			if(holdable)
			{
				return;
			}

			if(coolDownTimer>0)
			{
				coolDownTimer-=Time.deltaTime;
			}
			else
			{
				collider2D.enabled = true;
				holdable = true;
			}

			return;
		}

		if(coolDownTimer>0f)
		{
			coolDownTimer-=Time.deltaTime;
			return;
		}

		Vector2 sprayDirection = Vector2.zero;
		if(Input.GetKey ("w"))
		{
			sprayDirection +=Vector2.up;
		}
		if(Input.GetKey ("s"))
		{
			sprayDirection -=Vector2.up;
		}
		if(Input.GetKey ("a"))
		{
			sprayDirection +=Vector2.right;
		}
		if(Input.GetKey ("d"))
		{
			sprayDirection +=Vector2.right;
		}

		if(sprayDirection != Vector2.zero)
		{
			//fire a spray in that direction.

			coolDownTimer = sprayCoolDown;
		}

		if(Input.GetKeyDown ("x"))
		{
			GetDropped ();
		}
	}

	public void GetHeld(CharacterMovement newHolder)
	{
		newHolder.holding = true;
		beingHeld = true;
		holder = newHolder;
		transform.parent = newHolder.transform;
		transform.localPosition = Vector3.zero - Vector3.forward;
		//if (collider != null)
			collider2D.enabled = false;
		rigidbody2D.isKinematic = true;
	}

	public void GetDropped()
	{
		holdable = false;
		coolDownTimer = droppedCoolDown;
		beingHeld = false;
		holder.holding = false;
		holder = null;
		transform.parent = null;
		rigidbody2D.velocity = Vector2.up*dropSpeed;
		rigidbody2D.isKinematic = false;
		//collider2D.enabled = true;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if(beingHeld)
		{
			return;
		}
		CharacterMovement theCharacter = coll.gameObject.GetComponent<CharacterMovement> ();
		if (theCharacter != null)
		{
			if(!theCharacter.holding)
			{
				Debug.Log ("grabbed by "+theCharacter.gameObject.name);
				GetHeld (theCharacter);
			}
		}
	}
}
