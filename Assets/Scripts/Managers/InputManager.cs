using UnityEngine;
using System.Collections;

public class InputManager: BaseManager<InputManager> {
    #region Variables
    private DoAction m_DoAction;
    private RaycastHit targetTouch;
    private Vector3 m_DownStartPos;
    private float m_MoveDownLimit;
    private int m_TouchID;
    #endregion

    #region Initialisation & Destroy
    override protected IEnumerator CoroutineStart() {
        SetModeVoid();
        m_MoveDownLimit = 2.0f;
        isReady         = true;

        yield return true;
    }

    protected override void Destroy() {
        m_DoAction = null;

        base.Destroy();
    }
    #endregion

    #region Input Managment
    protected override void Play(int p_LevelID) {
        SetModeNormal();
    }

    #region DoAction
    void Update() {
        m_DoAction();
    }

    public void SetModeVoid() {
        m_DoAction = DoActionVoid;
    }

    private void DoActionVoid() {

    }

    public void SetModeNormal() {
        if (GameManager.instance.isTouchDevice) {
            m_DoAction = DoActionNormalTouch;
        }
        else {
            m_DoAction = DoActionNormal;
        }
    }

    private void DoActionNormal() {
        if (Input.GetMouseButtonDown(0)) {
            SetModeDown();
        }
    }

    private void DoActionNormalTouch() {
        if (Input.touches.Length > 0) {
            SetModeDown();
        }
    }

    private void SetModeDown() {
        if (GameManager.instance.isTouchDevice) {
            m_DownStartPos  = Input.touches[0].position;
            m_DoAction      = DoActionDownTouch;
            m_TouchID       = Input.touches[0].fingerId;
        }
        else {
            m_DownStartPos  = Input.mousePosition;
            m_DoAction      = DoActionDown;
        }
    }

    private void DoActionDown() {
        if (Input.GetMouseButtonUp(0)) {
            InputUp();
            SetModeNormal();
        }
        else if ((m_DownStartPos - Input.mousePosition).magnitude >= m_MoveDownLimit) {
            SetModeMove();
        }
    }

    private void DoActionDownTouch() {
        if (Input.touches.Length == 0 || Input.touches[0].fingerId != m_TouchID) {
            InputUp();
            SetModeNormal();
        }
        /*else if ((XXX).magnitude >= m_MoveDownLimit) {
            SetModeMove();
        }*/
    }

    private void SetModeMove() {
        if (GameManager.instance.isTouchDevice) {
            m_DoAction = DoActionMoveTouch;
        }
        else {
            m_DoAction = DoActionMove;
        }
    }

    private void DoActionMove() {
        if (Input.GetMouseButtonUp(0)) {
            SetModeNormal();
        }
        else {
            Scrolling(Input.mousePosition);
        }
    }

    private void DoActionMoveTouch() {
        if (Input.touches.Length == 0 || Input.touches[0].fingerId != m_TouchID) {
            SetModeNormal();
        }
        else {
            Scrolling(Input.touches[0].position);
        }
    }
    #endregion

    private void InputUp() {
        
    }

    private void Scrolling(Vector3 p_DownPos) {
        
    }
    #endregion
}