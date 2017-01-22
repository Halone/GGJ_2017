using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personnage : MonoBehaviour {

	private Animator anim;
	public int IDAnimator;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetStateBad()
	{
		anim.SetInteger("ID", IDAnimator);
		anim.SetBool("Bad", false);
		anim.SetBool("Great", false);
		anim.SetBool("Bad", true);
	}

	public void SetStateGood()
	{
		anim.SetInteger("ID", IDAnimator);
		anim.SetBool("Bad", false);
		anim.SetBool("Great", false);
		anim.SetBool("Good", true);
	}

	public void SetStateGreat()
	{
		anim.SetInteger("ID", IDAnimator);
		anim.SetBool("Good", false);
		anim.SetBool("Bad", false);
		anim.SetBool("Great", true);
		
	}
}
