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
    protected override void Play(int p_LevelID) {
        SwitchHUD(true);
    }

    public void SwitchHUD(bool p_SetActive) {
        Hud.SetActive(p_SetActive);
    }
    #endregion
}