using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class DrawCurveManager : MonoBehaviour {

	public List<PointMesh> PointList = new List<PointMesh>();

	public GameObject Panel;

	// Use this for initialization
	void Start () {
		for(var i = 0; i < transform.childCount; i++)
		{
			PointMesh lPointMesh = transform.GetChild(i).GetComponent<PointMesh>();
			lPointMesh.Order = transform.GetChild(i).position.y;
			lPointMesh.Position = new Vector2(transform.GetChild(i).position.x, transform.GetChild(i).position.y);

			PointList.Add(transform.GetChild(i).GetComponent<PointMesh>());
		}
		PointList.Sort(SortByY);

		var line = Panel.GetComponent<UIMeshLine>();
		for(var j = 0; j < PointList.Count; j++)
		{
			LinePoint lLinePoint = new LinePoint(PointList[j].Position);
			line.AddPoint(new LinePoint(PointList[j].Position));
			if (j + 1 < PointList.Count)
			{
				lLinePoint = new LinePoint(PointList[j].Position + (PointList[j+1].Position - PointList[j].Position) / 2);
				lLinePoint.isPrvCurve = true;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	static int SortByY(PointMesh p1, PointMesh p2)
	{
		return p1.Order.CompareTo(p2.Order);
	}
}
