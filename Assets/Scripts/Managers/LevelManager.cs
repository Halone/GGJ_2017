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
        SetModeVoid();
        isReady = true;

        yield return true;
    }
    #endregion

    #region Level Managment
	protected override void PlayGame(Dictionary<int, int> p_PlayerInstrumentDictionnary){
		SetModeNormal();
	}

	void Update() {
		doAction();
	}

    public void SetModeVoid() {
        doAction = DoActionVoid;
    }

    private void DoActionVoid() {

	}

    public void SetModeNormal() {
        doAction = DoActionNormal;
    }

    private void DoActionNormal() {
		//Ici on check l'exit + le temps de scroll
		if (CurrentTimeGame < GameDuration) {
			CurrentTimeGame += Time.deltaTime;
		}
		else {
			CurrentTimeGame = 0;
			CameraManager.instance.SwitchCamera(CameraManager.MENU_CAMERA_NAME);
			MenuManager.instance.OnClicScores();
			SetModeVoid();
			//On réinitialise tout
		}
	}
    #endregion
}