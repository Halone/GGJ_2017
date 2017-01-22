using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneratePatternesLD : MonoBehaviour
{
	private List<GameObject> ListSpheres;

	public GameObject LinesParent;
	public GameObject LinesPrefab;
	public GameObject PrefabLine;
	public GameObject PrefabCircle;

	private GameObject lLine;
	private UIMeshLine lMeshline;

	void Start()
	{//ou CoroutineStart
		ListSpheres = new List<GameObject>();
		for(int i = 0; i < transform.GetChild(0).childCount; i++)
		{
			ListSpheres.Add(transform.GetChild(0).GetChild(i).gameObject);
		}
		GeneratePatterneCurves();
	}

	void Update () {
	}

	private void GeneratePatterneCurves()
	{
		int ListSpheresSize = ListSpheres.Count;
		while(ListSpheresSize > 0)
		{
			int lRandomArray = (int) Mathf.Floor(Random.Range(2, 4));
			for(int j = 0; j < lRandomArray; j++)
			{
				int lRandomArraySize = (int) Mathf.Min(ListSpheresSize, Mathf.Floor(Random.Range(3, 5)));
				List<GameObject> PointsLine = ListSpheres.GetRange(0, lRandomArraySize);
				ListSpheres.RemoveRange(0, lRandomArraySize);
				ListSpheresSize -= lRandomArraySize;

				PointsLine[0].GetComponent<PointMesh>().IsMain = true;
				PointsLine[PointsLine.Count-1].GetComponent<PointMesh>().IsMain = true;
				lLine = Instantiate(PrefabLine, LinesParent.transform);
				lMeshline = lLine.GetComponent<UIMeshLine>();

				if (PointsLine.Count > 1)
				{
					for(int k = 0; k < PointsLine.Count; k++)
					{
						if(k == 0 || k == PointsLine.Count - 1)
						{
							AddPointInMeshLine(lLine, PointsLine[k], false);
						}
						else
						{
							AddPointInMeshLine(lLine, PointsLine[k], true);
						}
					}
				} else if (PointsLine.Count == 1) {
					GameObject lObject = Instantiate(PrefabCircle, LinesParent.transform);
					lObject.transform.position = PointsLine[0].transform.position;
				} else if (PointsLine.Count == 0)
				{
					return;
				}
					//Créer line et ajouter les points si supérieur à 1; 1 -> bouton; 0 -> return;
			}
			int lRandomUniquePoints = (int)Mathf.Min(ListSpheresSize, Mathf.Floor(Random.Range(0, 3)));
			for(int l = 0; l < lRandomUniquePoints; l++)
			{
				GameObject lObject = Instantiate(PrefabCircle, LinesParent.transform);
				lObject.transform.position = ListSpheres[l].transform.position;
			}
			//Créer points uniques
			ListSpheres.RemoveRange(0, lRandomUniquePoints);
			ListSpheresSize -= lRandomUniquePoints;
		}
		UnityEditor.PrefabUtility.ReplacePrefab(LinesParent, LinesPrefab);
	}

	public void AddPointInMeshLine(GameObject pLine, GameObject pPoint, bool lCurvePoint)
	{
		LinePoint lLinePoint = new LinePoint(new Vector2(pPoint.transform.position.x, pPoint.transform.position.y));
		if(lCurvePoint)
		{
			lLinePoint.isPrvCurve = true;
			lLinePoint.prvCurvePoint = lLinePoint.point;
		}
		lMeshline.AddPoint(lLinePoint);
	}
}
