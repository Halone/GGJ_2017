using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateLD : MonoBehaviour, AudioProcessor.AudioCallbacks
{

	private float		m_scrollCount;
	public  float		speedOfScroll;
	public GameObject sphere;

	public GameObject RawLD;
	public GameObject RawLDPrefab;

	private Vector3 Yincrement = Vector3.zero;

	public AudioProcessor processor;

	void Start()
	{//ou CoroutineStart
		processor.addAudioCallback(this);
		m_scrollCount = 0;
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.H))
		{
			Debug.Log("Enregister!");
			UnityEditor.PrefabUtility.ReplacePrefab(RawLD, RawLDPrefab);
		}
		m_scrollCount = speedOfScroll * Time.deltaTime;
		transform.localPosition -= Yincrement;
		Yincrement = Vector3.left * Random.Range(-20, 20);
		transform.localPosition += Vector3.up * m_scrollCount;
		transform.localPosition += Yincrement;
		Camera.main.transform.position += Vector3.up * m_scrollCount;
	}

	public void onOnbeatDetected()
	{
		GameObject go = Instantiate(sphere, transform.position, Quaternion.identity, RawLD.transform);
	}

	public void onSpectrum(float[] spectrum)
	{
		for(int i = 0; i < spectrum.Length; ++i)
		{
			Vector3 start = new Vector3(i, 0, 0);
			Vector3 end = new Vector3(i, spectrum[i], 0);
			Debug.DrawLine(start, end);
		}
	}

	/* Version Rectangle
	myRectTransform.offsetMin = new Vector2(myRectTransform.offsetMin.x, m_startTop - m_scrollCount);
	myRectTransform.offsetMax = new Vector2(myRectTransform.offsetMax.x, m_startBottom + m_scrollCount);*/
}
