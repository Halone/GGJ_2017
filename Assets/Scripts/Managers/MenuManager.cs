﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MenuManager: BaseManager<MenuManager> {
    #region Variables
    private const int PLAYER_MAX = 4;

    private class PlayerInstrument {
        public bool isJoin;
        public bool isLock;
        public Transform player;

        public PlayerInstrument(bool p_IsJoin, bool p_IsLock, Transform p_Player) {
            isJoin = p_IsJoin;
            isLock = p_IsLock;
            player = p_Player;
        }
    }

    private GameObject m_CurrentScreen;
    private List<bool> m_IsInstrumentTaken;
    private List<PlayerInstrument> m_PlayerList;
    private Dictionary<int, int> m_PlayerInstrumentDictionnary;

    public GameObject TitleCard;
    public GameObject Lobby;
    public GameObject Scores;
    public GameObject Credits;
    public GameObject BTN_Launch;

    public Action<Dictionary<int, int>> onLaunchGame;

    public int playerNB {
        get;
        private set;
    }
    #endregion

    #region Initialisation & Destroy
    override protected IEnumerator CoroutineStart() {
        playerNB                        = 0;
        m_PlayerInstrumentDictionnary   = new Dictionary<int, int>();
        m_PlayerList                    = new List<PlayerInstrument>();
        m_IsInstrumentTaken             = new List<bool>();

        for (int cptPlayer = 1; cptPlayer <= PLAYER_MAX; cptPlayer++) {
            Transform l_Player = Lobby.transform.Find("Player" + cptPlayer);
            m_PlayerList.Add(new PlayerInstrument(false, false, l_Player));
            m_IsInstrumentTaken.Add(false);
        }

        isReady = true;

        yield return true;
    }
    #endregion

    #region Interface Managment
    protected override void MainScreen() {
        OpenScreen(TitleCard);
    }

    protected override void PlayGame(Dictionary<int, int> p_PlayerInstrumentDictionnary) {
        
    }

    public void OnClicCredits() {
        OpenScreen(Credits);
    }

	public void OnClicScores()
	{
		OpenScreen(Scores);
	}

	public void OnClicPlay() {
        OpenScreen(Lobby);
    }

    public void OnClicTitleCard() {
        OpenScreen(TitleCard);
    }

    public void OnClicJoin(int p_PlayerID) {//1 -> 4
        SwitchLobbyPlayer(p_PlayerID - 1, true);
    }

    public void OnClicLeave(int p_PlayerID) {//1 -> 4
        SwitchLobbyPlayer(p_PlayerID - 1, false);
    }

	public void OnClicCloseApp()
	{
		Application.Quit();
	}

    private void SwitchLobbyPlayer(int p_PlayerID, bool p_IsJoin) {//0 -> 3
        Transform l_Instruments         = m_PlayerList[p_PlayerID].player.Find("Instruments");
        m_PlayerList[p_PlayerID].isJoin = p_IsJoin;

        l_Instruments.gameObject.SetActive(p_IsJoin);
        m_PlayerList[p_PlayerID].player.Find("BTN_Join").gameObject.SetActive(!p_IsJoin);
        if (p_IsJoin) ActivatePlayerAvailableInstruments(l_Instruments);
    }

    private void ActivatePlayerAvailableInstruments(Transform p_Instruments) {
        for (int cptInstrument = 0; cptInstrument < m_IsInstrumentTaken.Count; cptInstrument++) {
            p_Instruments.GetChild(cptInstrument).gameObject.SetActive(!m_IsInstrumentTaken[cptInstrument]);
        }
    }

    public void OnClicPlayerInstrument(int p_PlayerIDInstrumentID) {//11 -> 44
        int l_PlayerID      = int.Parse(p_PlayerIDInstrumentID.ToString()[0].ToString()) - 1;//0 -> 3
        int l_InstrumentID  = int.Parse(p_PlayerIDInstrumentID.ToString()[1].ToString()) - 1;//0 -> 3

        if (m_PlayerList[l_PlayerID].isLock) UnLockInstrument(l_PlayerID, l_InstrumentID);//unlock
        else LockInstrument(l_PlayerID, l_InstrumentID);//lock

        BTN_Launch.SetActive(playerNB > 0);
    }

    private void UnLockInstrument(int p_PlayerID, int p_InstrumentID) {//0 -> 3
        m_PlayerList[p_PlayerID].isLock     = false;
        m_IsInstrumentTaken[p_InstrumentID] = false;
        playerNB--;
        m_PlayerList[p_PlayerID].player.Find("Instruments").GetChild(m_IsInstrumentTaken.Count).gameObject.SetActive(true);

        UpdateAvailableInstruments();
    }

    private void LockInstrument(int p_PlayerID, int p_InstrumentID) {//0 -> 3
        Transform l_Instruments             = m_PlayerList[p_PlayerID].player.Find("Instruments");
        m_PlayerList[p_PlayerID].isLock     = true;
        m_IsInstrumentTaken[p_InstrumentID] = true;
        playerNB++;

        for (int cptIntrument = 0; cptIntrument < m_IsInstrumentTaken.Count; cptIntrument++) {
            l_Instruments.GetChild(cptIntrument).gameObject.SetActive(cptIntrument == p_InstrumentID);
        }

        l_Instruments.GetChild(m_IsInstrumentTaken.Count).gameObject.SetActive(false);
        UpdateAvailableInstruments();
    }

    private void UpdateAvailableInstruments() {
        for (int cptPlayer = 0; cptPlayer < m_PlayerList.Count; cptPlayer++) {
            if (m_PlayerList[cptPlayer].isJoin && !m_PlayerList[cptPlayer].isLock) {
                ActivatePlayerAvailableInstruments(m_PlayerList[cptPlayer].player.Find("Instruments"));
            }
        }
    }

    public void OnClicLaunch() {
        if (onLaunchGame != null) onLaunchGame(m_PlayerInstrumentDictionnary);
		HUDManager.instance.SetPlayers(m_PlayerInstrumentDictionnary);
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