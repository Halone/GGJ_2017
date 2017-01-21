using UnityEngine;
using System.Collections;

public class MenuManager: BaseManager<MenuManager> {
    #region Variables
    public GameObject TitleCard;
    public GameObject Lobby;
    public GameObject Score;
    public GameObject Credits;

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