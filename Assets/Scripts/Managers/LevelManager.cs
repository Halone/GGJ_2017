﻿using System;
using System.Collections;

public class LevelManager: BaseManager<LevelManager> {
    #region Variables
    public Action onDestroyLevel;
    
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
    protected override void Play(int p_LevelID) {
        LoadLevel(p_LevelID);
    }

    private void LoadLevel(int p_LevelID) {
        currentLevelID = p_LevelID;
    }

    public void CloseLevel() {
        MenuManager.instance.OpenSelectionScreen();
        CameraManager.instance.SwitchCamera(CameraManager.MENU_CAMERA_NAME);
        DestroyLevel();
    }

    public void DestroyLevel() {
        if (onDestroyLevel != null) onDestroyLevel();
    }
    #endregion
}
//TODO: refacto (CloseLevel) en event