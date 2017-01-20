using System.Collections;
using UnityEngine;

public abstract class Singleton<T>: MonoBehaviour where T: Component {
    #region Variables
    public static T instance {
        get;
        private set;
    }
    
    public bool isReady {
        get;
        protected set;
    }
    #endregion

    #region Initialisation & Destroy
    protected virtual void Awake() {
        if (instance != null) Destroy(instance.gameObject);
        instance    = this as T;
        isReady     = false;
    }

    protected virtual void Start() {
        StartCoroutine(CoroutineStart());
    }

    protected abstract IEnumerator CoroutineStart();

    protected virtual void Destroy() {
        if (instance != null) Destroy(instance.gameObject);
        instance = null;
    }
    #endregion

    protected void DebugError(string p_String) {
        Debug.LogError(name + " throw an error: " + p_String);
    }

    protected void DebugLog(string p_String) {
        Debug.Log(name + " say: " + p_String);
    }
}