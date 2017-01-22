using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager: BaseManager<InputManager> {
    #region Variables
    private DoAction m_DoAction;
    private RaycastHit targetTouch;
    private Vector3 m_DownStartPos;
	private List<Vector3> m_DownStartPosList;
	private List<int>	  m_TouchIDList;
	private float m_MoveDownLimit;
    private int m_TouchID;

	public LayerMask BuildingsMask;
	private string WAVE_TAG = "Wave";
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
    protected override void MainScreen() {
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
			for(int i = 0; i < Input.touches.Length; i++)
			{
				m_DownStartPosList[i] = Input.touches[i].position;
				m_TouchIDList[i] = Input.touches[i].fingerId;
            }

			//m_DownStartPos  = Input.touches[0].position;
			//m_TouchID       = Input.touches[0].fingerId;
			m_DoAction = DoActionDownTouch;
        }
        else {
            m_DownStartPos  = Input.mousePosition;
            m_DoAction      = DoActionDown;
        }

    }

    private void DoActionDown() {

		RaycastFromPos(Input.mousePosition);

		if (Input.GetMouseButtonUp(0)) {
            InputUp();
            SetModeNormal();
		}
        /*else if ((m_DownStartPos - Input.mousePosition).magnitude >= m_MoveDownLimit) {
            SetModeMove();
        }*/
    }

    private void DoActionDownTouch() {
		//Ici on renvoie en SetModeNormal si plus personne touche

		for(int i = 0; i < Input.touches.Length; i++)
		{
			RaycastFromPos(Input.GetTouch(i).position);
        }

		if(Input.touches.Length == 0)
		{
			SetModeNormal();
		}

		/*if (Input.touches.Length == 0 || Input.touches[0].fingerId != m_TouchID) {
            InputUp();
            SetModeNormal();
        }
        else if ((XXX).magnitude >= m_MoveDownLimit) {
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

	private void RaycastFromPos(Vector3 lPos){
		RaycastHit hit;
		Ray rayOut = CameraManager.instance.getActiveCamera.ScreenPointToRay(lPos);
        if(Physics.Raycast(rayOut, out hit)){
			if(hit.transform.gameObject.tag == WAVE_TAG){
				
			}
		}
	}

	private void InputUp() {
        
    }

    private void Scrolling(Vector3 p_DownPos) {
		DebugLogWarning("scrollscroll");
    }
    #endregion
}