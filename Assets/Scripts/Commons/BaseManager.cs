using System.Collections.Generic;
using UnityEngine;

public abstract class BaseManager<T>: Singleton<T> where T: Component {
    #region Initialisation & Destroy
    protected override void Start() {
        if (GameManager.instance) {
            GameManager.instance.onMainScreen   += MainScreen;
            GameManager.instance.onPlayGame     += PlayGame;
        }
        else DebugError("GameManager does not exist");

        base.Start();
    }

    protected override void Destroy() {
        if (GameManager.instance) {
            GameManager.instance.onMainScreen   -= MainScreen;
            GameManager.instance.onPlayGame     -= PlayGame;
        }

        base.Destroy();
    }
    #endregion

    #region Game Events
    protected virtual void MainScreen() {

    }

    protected virtual void PlayGame(Dictionary<int, int> p_PlayerInstrumentDictionnary) {

    }
    #endregion
}