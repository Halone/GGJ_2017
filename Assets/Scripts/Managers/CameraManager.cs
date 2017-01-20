using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraManager: BaseManager<CameraManager> {
    #region Variables
    public const string MENU_CAMERA_NAME   = "MenuCamera";
    public const string LEVEL_CAMERA_NAME  = "LevelCamera";

    private Dictionary<string, Camera> m_CameraList;

    public Camera getActiveCamera {
        get {
            foreach (Camera l_Camera in m_CameraList.Values) {
                if (l_Camera.isActiveAndEnabled) return l_Camera;
            }

            DebugError("Aucune caméra active");
            return null;
        }
    }
    #endregion

    #region Initialisation & Destroy
    protected override IEnumerator CoroutineStart() {
        m_CameraList = new Dictionary<string, Camera>();
        m_CameraList.Add(MENU_CAMERA_NAME, transform.FindChild(MENU_CAMERA_NAME).GetComponent<Camera>());
        m_CameraList.Add(LEVEL_CAMERA_NAME, transform.FindChild(LEVEL_CAMERA_NAME).GetComponent<Camera>());
        isReady = true;

        yield return true;
    }

    protected override void Destroy() {
        m_CameraList.Clear();
        m_CameraList = null;

        base.Destroy();
    }
    #endregion

    #region Camera Managment
    protected override void Play(int p_LevelID) {
        SwitchCamera(LEVEL_CAMERA_NAME);
    }

    protected override void Menu() {
        SwitchCamera(MENU_CAMERA_NAME);
    }

    public void SwitchCamera(string p_CameraToActive) {
        if (m_CameraList.ContainsKey(p_CameraToActive)) {
            getActiveCamera.gameObject.SetActive(false);
            m_CameraList[p_CameraToActive].gameObject.SetActive(true);
        }
        else DebugError(p_CameraToActive + " does not exist");
    }
    #endregion
}
//TODO: refacto partiel pour passer le SwitchCamera en private et ne passer que par des events (Menu / Play / ...)