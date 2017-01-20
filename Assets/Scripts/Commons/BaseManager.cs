using UnityEngine;

public abstract class BaseManager<T>: Singleton<T> where T: Component {
    #region Initialisation & Destroy
    protected override void Start() {
        if (GameManager.instance) {
            GameManager.instance.onMenu += Menu;
            GameManager.instance.onPlay += Play;
        }
        else DebugError("GameManager does not exist");

        base.Start();
    }

    protected override void Destroy() {
        if (GameManager.instance) {
            GameManager.instance.onMenu -= Menu;
            GameManager.instance.onPlay -= Play;
        }

        base.Destroy();
    }
    #endregion

    #region Game Events
    protected virtual void Play(int p_LevelID) {
        
    }

    protected virtual void Menu() {

    }
    #endregion
}