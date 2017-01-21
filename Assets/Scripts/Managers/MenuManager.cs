using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MenuManager: BaseManager<MenuManager> {
    #region Variables
    public GameObject TitleCard;
    public GameObject Lobby;
    public GameObject Scores;
    public GameObject Credits;
    public GameObject BTN_Launch;
    public List<GameObject> LobbyPlayerList;
    public Action<Dictionary<int, int>> onLaunchGame;

    private GameObject m_CurrentScreen;
    private Dictionary<int, int> m_PlayerInstrumentDictionnary;

    public int playerNB {
        get;
        private set;
    }
    #endregion

    #region Initialisation & Destroy
    override protected IEnumerator CoroutineStart() {
        playerNB                        = 0;
        m_PlayerInstrumentDictionnary   = new Dictionary<int, int>();
        isReady                         = true;

        yield return true;
    }
    #endregion

    #region Interface Managment
    protected override void MainScreen() {
        OpenScreen(TitleCard);
    }

    protected override void PlayGame(Dictionary<int, int> p_PlayerInstrumentDictionnary) {
        DebugLogWarning(" nombre de joueur : " + p_PlayerInstrumentDictionnary.Keys.Count);
    }

    public void OnClicCredits() {
        OpenScreen(Credits);
    }

    public void OnClicPlay() {
        OpenScreen(Lobby);
    }

    public void OnClicTitleCard() {
        OpenScreen(TitleCard);
    }

    public void OnClicJoin(int p_PlayerID) {
        SwitchLobbyPlayer(p_PlayerID, true);
    }

    public void OnClicLeave(int p_PlayerID) {
        SwitchLobbyPlayer(p_PlayerID, false);
    }

    private void SwitchLobbyPlayer(int p_PlayerID, bool p_IsInstrumentActive) {
        LobbyPlayerList[p_PlayerID - 1].transform.FindChild("Instruments").gameObject.SetActive(p_IsInstrumentActive);
        LobbyPlayerList[p_PlayerID - 1].transform.FindChild("BTN_Join").gameObject.SetActive(!p_IsInstrumentActive);
    }

    public void OnClicPlayerInstrument(int p_PlayerIDInstrumentID) {
        int l_PlayerID      = int.Parse(p_PlayerIDInstrumentID.ToString()[0].ToString());
        int l_InstrumentID  = int.Parse(p_PlayerIDInstrumentID.ToString()[1].ToString());

        if (m_PlayerInstrumentDictionnary.ContainsKey(l_PlayerID)) {//deselection
            playerNB--;
            //deverrouiller l'instrument deselectionne pour tout le monde (si joueur non verrouille)
            //deverouiller les instruments non selectionne pour lui
        }
        else {//selection
            playerNB++;
            m_PlayerInstrumentDictionnary.Add(l_PlayerID, l_InstrumentID);
            //verouille l'instrument pour les autres joueurs (si joueur non verrouille)
            //verouille les autres instruments
        }

        BTN_Launch.SetActive(playerNB > 0);
    }

    public void OnClicLaunch() {
        if (onLaunchGame != null) onLaunchGame(m_PlayerInstrumentDictionnary);
    }

    private void OpenScreen(GameObject p_ScreenToOpen) {
        CloseCurrentScreen();
        p_ScreenToOpen.SetActive(true);
        m_CurrentScreen = p_ScreenToOpen;
    }

    private void CloseCurrentScreen() {
        if (m_CurrentScreen != null) m_CurrentScreen.SetActive(false);
    }
    #endregion
}