using UnityEngine;
using System.Collections;
using System;

public class MenuManager: BaseManager<MenuManager> {
    #region Variables
    public Action<int> onClicLevel;
    public GameObject MainScreen;
    public GameObject LevelScreen;
    public GameObject WinScreen;

    private GameObject m_CurrentScreen;
    #endregion

    #region Initialisation & Destroy
    override protected IEnumerator CoroutineStart() {
        isReady = true;

        yield return true;
    }
    #endregion

    #region Interface Managment
    protected override void Menu() {
        OpenScreen(MainScreen);
    }

    protected override void Play(int p_LevelID) {
        CloseCurrentScreen();
    }

    public void OnClickPlay() {
        OpenScreen(LevelScreen);
    }

    public void OnClickLoadLevel(int p_LevelID) {
        if (onClicLevel != null) onClicLevel(p_LevelID);
    }

    public void ShowWinMenu() {
        HUDManager.instance.SwitchHUD(false);
        OpenScreen(WinScreen);
        CameraManager.instance.SwitchCamera(CameraManager.MENU_CAMERA_NAME);
    }

    public void OpenSelectionScreen() {
        OpenScreen(LevelScreen);
    }

    public void OnClickCloseWinScreen() {
        OpenSelectionScreen();
        LevelManager.instance.DestroyLevel();
    }

    private void CloseCurrentScreen() {
        if (m_CurrentScreen != null) m_CurrentScreen.SetActive(false);
    }

    private void OpenScreen(GameObject p_ScreenToOpen) {
        CloseCurrentScreen();
        p_ScreenToOpen.SetActive(true);
        m_CurrentScreen = p_ScreenToOpen;
    }
    #endregion
}
//TODO: refacto