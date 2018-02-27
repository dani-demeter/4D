using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Grapher : MonoBehaviour{
	[Range (10, 100)]
	public int xres = 10;
	[Range (10, 100)]
	public int yres = 10;
	[Space (20)]
	public int maxX;
	public int maxY;
	public int minX;
	public int minY;
	[Space (20)]
	public GameObject pointPrefab;

	private GameObject[,] points;

	void Start (){
		points = new GameObject[xres, yres];
		for (int i = 0; i < xres; i++){
			for (int j = 0; j < yres; j++){
				GameObject p = Instantiate (pointPrefab) as GameObject;

				float x = ((float)((maxX-minX) * i) / xres) + minX;
				float y = ((float)((maxY-minY) * j) / yres) + minY;
				float z = f (x, y);

				p.transform.position = new Vector3 (x, y, z);
				p.transform.parent = transform;

				Renderer rend = p.GetComponent<Renderer> ();
				Material m = new Material (rend.material);
//				m.color = new Color ((float)(i) / xres, (float)(j) / yres, Mathf.Clamp(z, 0, 255));
				m.color = new Color ((float)(i) / xres, (float)(j) / yres, 0.5f);
				m.SetColor ("_EmissionColor", m.color);
				p.GetComponent<Renderer> ().material = m;

				points [i, j] = p;
			}
		}
		transform.RotateAround (Vector3.zero, new Vector3 (1, 0, 0), 90);
	}

	float f (float x, float y){
		return Mathf.Sin (Mathf.Sqrt(x*x+y*y));
//		return (x - y) * (x + y);
	}

	void Update (){

	}
}
