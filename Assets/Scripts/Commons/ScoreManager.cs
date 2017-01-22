using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {



	public float scoreP1 = 0;
	public float scoreP2 = 0;
	public float scoreP3 = 0;
	public float scoreP4 = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(LevelManager.instance.IsOnPlay)
		{
			//add score si on touche ou pas ligne
			//voir si on peut le faire dans le input manager
		}
	}

	public void InitScoreManager()
	{
		//Ici associer un score à un player 
		//Si jamais le player est false on n'update pas le score dans le futur

		for(int i = 0; i < MenuManager.instance.m_PlayerList.Count; i++)
		{
			
		}
	}
}
