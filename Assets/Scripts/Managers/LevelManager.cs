using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager: BaseManager<LevelManager> {
    #region Variables
    public Action onDestroyLevel;

	public bool IsOnPlay;

	//A Remplacer par la durée de la musique !!! /!\
	public float GameDuration = 10;
	public float CurrentTimeGame = 0;

	DoAction doAction;
    
    public int currentLevelID {
        get;
        private set;
     }
    #endregion

    #region Initialisation & Destroy
    override protected IEnumerator CoroutineStart() {
        isReady = true;

        yield return true;
    }
    #endregion

    #region Level Managment
    private void LoadLevel(int p_LevelID) {
        currentLevelID = p_LevelID;
    }

    public void CloseLevel() {
        CameraManager.instance.SwitchCamera(CameraManager.MENU_CAMERA_NAME);
        DestroyLevel();
    }

    public void DestroyLevel() {
        if (onDestroyLevel != null) onDestroyLevel();
    }

	protected override void PlayGame(Dictionary<int, int> p_PlayerInstrumentDictionnary)
	{
		base.PlayGame(p_PlayerInstrumentDictionnary);
		SetModeNormal();
	}

	protected override void Start()
	{
		base.Start();
		SetModeVoid();
	}

	void Update()
	{
		doAction();
	}

	private void DoActionVoid()
	{
	}

	private void DoActionNormal()
	{
		//Ici on check l'exit + le temps de scroll
		if(CurrentTimeGame < GameDuration)
		{
			CurrentTimeGame += Time.deltaTime;
		}
		else
		{
			IsOnPlay = false;
			CurrentTimeGame = 0;
			CameraManager.instance.SwitchCamera(CameraManager.MENU_CAMERA_NAME);
			MenuManager.instance.OnClicScores();
			SetModeVoid();
			//On réinitialise tout
		}
		
	}

	public void SetModeNormal()
	{
		IsOnPlay = true;
		doAction = DoActionNormal;
	}

	public void SetModeVoid()
	{
		doAction = DoActionVoid;
	}
	#endregion
}
//TODO: refacto (CloseLevel) en event