using UnityEngine;

public abstract class BaseManager<T>: Singleton<T> where T: Component {
    #region Initialisation & Destroy
    protected override void Start() {
        if (GameManager.instance) {
            GameManager.instance.onMainScreen += MainScreen;
        }
        else DebugError("GameManager does not exist");

        base.Start();
    }

    protected override void Destroy() {
        if (GameManager.instance) {
            GameManager.instance.onMainScreen   -= MainScreen;
        }

        base.Destroy();
    }
    #endregion

    #region Game Events
    protected virtual void MainScreen() {

    }
    #endregion
}