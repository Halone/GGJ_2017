using System;
using System.Collections;
using System.Collections.Generic;

public class LevelManager: BaseManager<LevelManager> {
    #region Variables
    public Action onDestroyLevel;

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

	private void DoActionVoid()
	{

	}

	private void DoActionNormal()
	{
		//Euh ScrollObj ?
	}

	public void SetModeNormal()
	{
		doAction = DoActionNormal;
	}

	public void SetModeVoid()
	{
		doAction = DoActionVoid;
	}
	#endregion
}
//TODO: refacto (CloseLevel) en event