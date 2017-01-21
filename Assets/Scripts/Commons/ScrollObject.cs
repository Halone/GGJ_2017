using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollObject : MonoBehaviour {

	//private ScrollRect myScrollRect;
	private RectTransform myRectTransform;
	private float		m_scrollCount;
	public  float		speedOfScroll;

	// Use this for initialization
	void Start () {
		m_scrollCount = 0;
		myRectTransform = GetComponent<RectTransform>();
	} 

	// Update is called once per frame
	void Update () {
		m_scrollCount = speedOfScroll * Time.deltaTime;

		myRectTransform.localPosition += -myRectTransform.up;

		/* Version Rectangle
		myRectTransform.offsetMin = new Vector2(myRectTransform.offsetMin.x, m_startTop - m_scrollCount);
		myRectTransform.offsetMax = new Vector2(myRectTransform.offsetMax.x, m_startBottom + m_scrollCount);*/
	}
}
