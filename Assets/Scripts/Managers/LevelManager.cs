using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager: BaseManager<LevelManager> {
    #region Variables
    public Action onDestroyLevel;

	public bool IsOnPlay;

	//A Remplacer par la durée de la musique !!! /!\
	public float GameDuration = 10;
	public float CurrentTimeGame = 0;

	public float scoreP1 = 0;
	public float scoreP2 = 0;
	public float scoreP3 = 0;
	public float scoreP4 = 0;

	private int lastMaxPlayer = -1;

	public GameObject joueurP1;
	public GameObject joueurP2;
	public GameObject joueurP3;
	public GameObject joueurP4;
	public GameObject crown;

	public GameObject winner;

	DoAction doAction;
    
    public int currentLevelID {
        get;
        private set;
     }
    #endregion

    #region Initialisation & Destroy
    override protected IEnumerator CoroutineStart() {
        isReady = true;

        yield return true;
    }
    #endregion

    #region Level Managment
    private void LoadLevel(int p_LevelID) {
        currentLevelID = p_LevelID;
    }

    public void CloseLevel() {
        CameraManager.instance.SwitchCamera(CameraManager.MENU_CAMERA_NAME);
        DestroyLevel();
    }

    public void DestroyLevel() {
        if (onDestroyLevel != null) onDestroyLevel();
    }

	protected override void PlayGame(Dictionary<int, int> p_PlayerInstrumentDictionnary)
	{
		base.PlayGame(p_PlayerInstrumentDictionnary);
		SetModeNormal();
	}

	protected override void Start()
	{
		base.Start();
		SetModeVoid();
	}

	void Update()
	{
		doAction();
	}

	private void DoActionVoid()
	{
	}

	private void DoActionNormal()
	{
		//Ici on check l'exit + le temps de scroll
		if(CurrentTimeGame < GameDuration)
		{
			SetCrownOnBestPlayer();
			CurrentTimeGame++;
		}
		else
		{
			IsOnPlay = false;
			CurrentTimeGame = 0;
			CameraManager.instance.SwitchCamera(CameraManager.MENU_CAMERA_NAME);
			MenuManager.instance.OnClicScores();
			string HeroToReturn = ReturnPlayer(lastMaxPlayer).transform.FindChild("Personnage").GetComponent<SpriteRenderer>().sprite.name;
			HeroToReturn.Substring(0, HeroToReturn.Length - 4);
			Debug.LogWarning("HeroToReturn : " + HeroToReturn);
            Sprite lSprite = Resources.Load<Sprite>("Graphics/Assets/Win_" + ReturnAssetWinByLastName(HeroToReturn));
			winner.GetComponent<SpriteRenderer>().sprite = lSprite;
            SetModeVoid();
			//On réinitialise tout
		}
		


	}

	private string ReturnAssetWinByLastName(string name)
	{
		switch(name)
		{
			case "chara_Rebel_WIP":
                return "Alex";
			case "chara_Stoned_WIP":
                return "Pierre";
			case "chara_Nerd_WIP":
                return "Ashley";
			case "chara_BG_WIP":
                return "Michael";
			default:
				return "";
		}
	}

	public void AddScoreTo(int pX)
	{
		switch(pX)
		{
			case 1:
				scoreP1++;
				break;
			case 2:
				scoreP2++;
				break;
			case 3:
				scoreP3++;
				break;
			case 4:
				scoreP4++;
				break;
		}
	}

	public GameObject ReturnPlayer(int i)
	{
		switch(i)
		{
			case 0:
				return joueurP1;
			case 1:
				return joueurP2;
			case 2:
				return joueurP3;
			case 3:
				return joueurP4;
			default:
				return null;
		}
	}

	private void SetCrownOnBestPlayer()
	{
		List<float> array = new List<float>();
		array.Add(scoreP1);
		array.Add(scoreP2);
		array.Add(scoreP3);
		array.Add(scoreP4);
		int max = 0;

		for(int i = 0; i < 4; i++)
			if(array[i] > max)
			{
				max = i;
			}

		if(lastMaxPlayer == max)
			return;

		lastMaxPlayer = max;

		//crown.transform.rotation = LookAt(-ReturnPlayer(max).GetComponentInChildren<ScrollObject>().transform.up);
		//crown.transform.right = ReturnPlayer(max).transform.FindChild("Personnage").transform.position - crown.transform.position;



		Vector3 dir = ReturnPlayer(max).transform.FindChild("Personnage").transform.position - crown.transform.position;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 270;
		crown.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	public void SetModeNormal()
	{
		IsOnPlay = true;
		doAction = DoActionNormal;
	}

	public void SetModeVoid()
	{
		doAction = DoActionVoid;
	}
	#endregion
}
//TODO: refacto (CloseLevel) en event