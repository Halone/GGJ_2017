using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DrawCurveManager : MonoBehaviour {

	public List<PointMesh> PointList = new List<PointMesh>();

	public GameObject LinesParent;
	public GameObject PrefabLine;
	public GameObject PrefabCircle;

	public GameObject lLine;
	public UIMeshLine lMeshline;

	private bool StateInLine;

	// Use this for initialization
	void Start () {
		if(PointList.Count > 0)
			return;
		Init();

		for(var j = 0; j < PointList.Count; j++)
		{
			if(PointList[j].IsMain && !StateInLine)
			{
				//Start de line
				lLine = Instantiate(PrefabLine, LinesParent.transform);
				lMeshline = lLine.GetComponent<UIMeshLine>();
				AddPointInMeshLine(j, false);
				StateInLine = true;
			}
			else if(!PointList[j].IsMain && !StateInLine)
			{
				// Solo click
				GameObject lObject = Instantiate(PrefabCircle, LinesParent.transform);
				lObject.transform.localPosition = PointList[j].Position;

			}
			else if(!PointList[j].IsMain && StateInLine)
			{
				// Point dans ligne
				AddPointInMeshLine(j, true);
			}
			else if(PointList[j].IsMain && StateInLine)
			{
				//Fin de line
				AddPointInMeshLine(j, true);
				StateInLine = false;
			}
		}
	}

	public void AddPointInMeshLine(int pIndex, bool lCurvePoint)
	{
		LinePoint lLinePoint = new LinePoint(PointList[pIndex].Position);
		if(pIndex != 0 && lCurvePoint)
		{
			lLinePoint.isPrvCurve = true;
			lLinePoint.prvCurvePoint = PointList[pIndex].Position - (PointList[pIndex].Position - PointList[pIndex - 1].Position) / 2;
		}
		lMeshline.AddPoint(lLinePoint);
	}

	public void Init() {
		for(var i = 0; i < transform.childCount; i++)
		{
			Debug.Log("yolo");
			PointMesh lPointMesh = transform.GetChild(i).GetComponent<PointMesh>();
			lPointMesh.Order = transform.GetChild(i).position.y;
			lPointMesh.Position = new Vector2(transform.GetChild(i).position.x, transform.GetChild(i).position.y);

			PointList.Add(transform.GetChild(i).GetComponent<PointMesh>());
		}
		PointList.Sort(SortByY);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	static int SortByY(PointMesh p1, PointMesh p2)
	{
		return p1.Order.CompareTo(p2.Order);
	}
}
