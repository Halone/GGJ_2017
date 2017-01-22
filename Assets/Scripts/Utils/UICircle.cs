using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[ExecuteInEditMode]
public class UICircle : MaskableGraphic // Changed to maskableGraphic so it can be masked with RectMask2D
{
	// Creates a line renderer that follows a Sin() function
	// and animates it.

	public Color c1 = Color.yellow;
	public Color c2 = Color.red;
	public int lengthOfLineRenderer = 20;
	private float r = 10f;

	protected override void Start()
	{
		float theta_scale = 0.1f;             //Set lower to add more points
		int size = (int) Mathf.Round((2.0f * Mathf.PI) / theta_scale); //Total number of points in circle.

		LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		lineRenderer.SetColors(c1, c2);
		lineRenderer.SetWidth(0.2F, 0.2F);
		lineRenderer.SetVertexCount(size);

		int i = 0;
		for(float theta = 0; theta < 2 * Mathf.PI; theta += 0.1f)
		{
			var x = r * Mathf.Cos(theta);
			var y = r * Mathf.Sin(theta);

			Vector3 pos = new Vector3(x, y, 0);
			lineRenderer.SetPosition(i, pos);
			i += 1;
		}
	}

	void Update()
	{

		LineRenderer lineRenderer = GetComponent<LineRenderer>();
		var t = Time.time;
		for(int i = 0; i < lengthOfLineRenderer; i++)
		{
			lineRenderer.SetPosition(i, new Vector3(i * 0.5f, Mathf.Sin(i + t), 0.0f));
		}
	}
}
