using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Grapher : MonoBehaviour{
	public GameObject ULR;
	public GameObject VLR;
	[Space(20)]
	[Range (10, 100)]
	public int Sres = 10;
	[Range (10, 100)]
	public int Tres = 10;
	[Space (20)]
	public int maxS;
	public int maxT;
	public int minS;
	public int minT;
	[Space (20)]
	public GameObject pointPrefab;

	private List<LineRenderer> lrs = new List<LineRenderer>();
	private GameObject[,] points;
	private float scale=1;

	float fx (float s, float t){
		return (1.25f+0.5f*Mathf.Cos(s))*Mathf.Cos(t);
		// return Mathf.Sin(s)*Mathf.Cos(t);
	}
	float fy (float s, float t){
		return (1.25f+0.5f*Mathf.Cos(s))*Mathf.Sin(t);
		// return Mathf.Sin(s)*Mathf.Sin(t);
	}
	float fu (float s, float t){
		return 0.5f*Mathf.Sin(s);
		// return Mathf.Cos(s);
	}
	float fv (float s, float t){
		return Mathf.Sin (Mathf.Sqrt(s*s+t*t));
	}
	public void Invert(){
		Vector3 o = new Vector3(0,3,0);
		float r = 1f;
		for (int i = 0; i < Sres; i++){
			for (int j = 0; j < Tres; j++){
				Vector3 p = points [i, j].transform.localPosition;
				Vector3 op = p-o;
				float opm = op.magnitude;
				float oppm = r*r/opm;
				Vector3 pp = o+oppm*(op/opm);
				points[i, j].transform.localPosition = pp;
			}
		}
		ReDrawLines();
	}
	void ReDrawLines(){
		// GameObject[] lrs = GameObject.FindGameObjectsWithTag("LR");
		for(int i=0; i<Sres; i++){
			// GameObject e = lrs[i];
			// e.transform.parent = transform;
			LineRenderer lr = lrs[i];
			lr.positionCount = Tres;
			Vector3[] poss = new Vector3[lr.positionCount];
			for(int j=0; j<Tres; j++){
				poss[j] = points[i,j].transform.localPosition;
			}
			lr.SetPositions(poss);
		}
		for(int j=0; j<Tres; j++){
			// GameObject e = lrs[j+Sres];
			// e.transform.parent = transform;
			LineRenderer lr = lrs[j+Sres];
			lr.positionCount = Sres;
			Vector3[] poss = new Vector3[lr.positionCount];
			for(int i=0; i<Sres; i++){
				poss[i] = points[i,j].transform.localPosition;
			}
			lr.SetPositions(poss);
		}
		// for(int i=0; i<Sres; i++){
		// }
	}
	void Update(){
		for(int i=0; i<lrs.Count; i++){
			lrs[i].startWidth = scale*0.05f;
		}
	}
	void Start (){
		points = new GameObject[Sres, Tres];
		for (int s = 0; s < Sres; s++){
			for (int t = 0; t < Tres; t++){
				GameObject p = Instantiate (pointPrefab) as GameObject;
				float cS = ((float)((maxS-minS) * s) / Sres) + minS;
				float cT = ((float)((maxT-minT) * t) / Tres) + minT;
				float x = fx(cS, cT);
				float y = fy(cS, cT);
				float z = fu(cS, cT);

				p.transform.position = new Vector3 (x, y, z);
				p.transform.parent = transform;

				// Renderer rend = p.GetComponent<Renderer> ();
				// Material m = new Material (rend.material);
				// m.color = new Color ((float)(s) / Sres, (float)(t) / Tres, 0.5f);
				// m.SetColor ("_EmissionColor", m.color);
				// p.GetComponent<Renderer> ().material = m;

				points [s, t] = p;
			}
		}
		Debug.Log(points[0,0].transform.position);
		DrawLines();
		transform.RotateAround (Vector3.zero, new Vector3 (1, 0, 0), 90);
	}


	public void SetScale(float f){
		scale = f;
	}
	void DrawLines(){
		for(int i=0; i<Sres; i++){
			GameObject e = Instantiate(ULR) as GameObject;
			e.transform.parent = transform;
			LineRenderer lr = e.GetComponent<LineRenderer>();
			lr.positionCount = Tres;
			Vector3[] poss = new Vector3[lr.positionCount];
			for(int j=0; j<Tres; j++){
				poss[j] = points[i,j].transform.position;
			}
			lr.SetPositions(poss);
			lrs.Add(lr);
		}
		for(int j=0; j<Tres; j++){
			GameObject e = Instantiate(VLR) as GameObject;
			e.transform.parent = transform;
			LineRenderer lr = e.GetComponent<LineRenderer>();
			lr.positionCount = Sres;
			Vector3[] poss = new Vector3[lr.positionCount];
			for(int i=0; i<Sres; i++){
				poss[i] = points[i,j].transform.position;
			}
			lr.SetPositions(poss);
			lrs.Add(lr);
		}
		// for(int i=0; i<Sres; i++){
		// }
	}
}
