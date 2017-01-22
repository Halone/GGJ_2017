using System.Collections;
using System;
using System.Collections.Generic;

public class GameManager: Singleton<GameManager> {
    #region Variables
    public Action onMainScreen;
    public Action<Dictionary<int, int>> onPlayGame;

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
                InputManager.instance == null ||
                SoundsManager.instance == null ||
                CameraManager.instance == null ||
                FMODManager.instance == null
        ) {
            yield return false;
        }

        while (!HUDManager.instance.isReady ||
                !MenuManager.instance.isReady ||
                !LevelManager.instance.isReady ||
                !InputManager.instance.isReady ||
                !SoundsManager.instance.isReady ||
                !CameraManager.instance.isReady ||
                !FMODManager.instance.isReady
        ) {
            yield return false;
        }

        MenuManager.instance.onLaunchGame += PlayGame;
        isTouchDevice   = false;
        isReady         = true;

        MainScreen();
    }
    #endregion

    #region GameStates Managment
    private void MainScreen() {
        if (onMainScreen != null) onMainScreen();
    }

    private void PlayGame(Dictionary<int, int> p_PlayerInstrumentDictionnary) {
        if (onPlayGame != null) onPlayGame(p_PlayerInstrumentDictionnary);
    }
    #endregion
}
//TODO: récupérer si le device est en touch ou non