using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HUDManager: BaseManager<HUDManager> {
    #region Variables
    public GameObject Hud;
	public GameObject Lobby;

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

	public void SetPlayers(Dictionary<int, int> p_PlayerInstrumentDictionnary)
	{
		for(var i = 0; i < p_PlayerInstrumentDictionnary.Count; i++)
		{
			Debug.Log(p_PlayerInstrumentDictionnary[i]);
		}
	}
    #endregion
}