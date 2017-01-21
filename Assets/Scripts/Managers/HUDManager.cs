using UnityEngine;
using System.Collections;

public class HUDManager: BaseManager<HUDManager> {
    #region Variables
    public GameObject Hud;
    #endregion

    #region Initialisation & Destroy
    override protected IEnumerator CoroutineStart() {
        isReady = true;

        yield return true;
    }
    #endregion

    #region HUD Managment
    public void SwitchHUD(bool p_SetActive) {
        Hud.SetActive(p_SetActive);
    }
    #endregion
}