using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuManager: BaseManager<MenuManager> {
    #region Variables
    public GameObject TitleCard;
    public GameObject Lobby;
    public GameObject Scores;
    public GameObject Credits;
    public List<GameObject> LobbyPlayerList;

    private GameObject m_CurrentScreen;
    #endregion

    #region Initialisation & Destroy
    override protected IEnumerator CoroutineStart() {
        isReady = true;

        yield return true;
    }
    #endregion

    #region Interface Managment
    protected override void MainScreen() {
        OpenScreen(TitleCard);
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