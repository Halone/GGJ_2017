using System.Collections;
using System;

public class GameManager: Singleton<GameManager> {
    #region Variables
    public Action<int> onPlay;
    public Action onMenu;

    public bool isTouchDevice {
        get;
        private set;
    }
    #endregion

    #region Initialisation & Destroy
    protected override IEnumerator CoroutineStart() {
        while (HUDManager.instance == null ||
                MenuManager.instance == null ||
                LevelManager.instance == null ||
                DataManager.instance == null ||
                InputManager.instance == null ||
                SoundsManager.instance == null ||
                CameraManager.instance == null
        ) {
            yield return false;
        }

        while (!HUDManager.instance.isReady ||
                !MenuManager.instance.isReady ||
                !LevelManager.instance.isReady ||
                !DataManager.instance.isReady ||
                !InputManager.instance.isReady ||
                !SoundsManager.instance.isReady ||
                !CameraManager.instance.isReady
        ) {
            yield return false;
        }

        MenuManager.instance.onClicLevel += Play;
        isTouchDevice   = true;
        isReady         = true;

		// Menu();
		Play(0);
    }
    #endregion

    #region GameStates Managment
    void Menu() {
        if (onMenu != null) onMenu();
    }
    
    void Play(int p_LevelID) {
        if (onPlay != null) onPlay(p_LevelID);
    }
    #endregion
}
//TODO: récupérer si le device est en touch ou non