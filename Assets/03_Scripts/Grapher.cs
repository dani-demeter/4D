using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Grapher : MonoBehaviour{
	public GameObject ULR;
	public GameObject VLR;
	public GameObject DLR;
	public GameObject meshPrefab;
	public GameObject pointPrefab;
	[Space(20)]
	[Range (2, 100)]
	public int Sres = 10;
	[Range (2, 100)]
	public int Tres = 10;
	[Space (20)]
	public float maxS;
	public float maxT;
	public float minS;
	public float minT;
	[Space (20)]

	private List<LineRenderer> lrs = new List<LineRenderer>();
	private List<MeshFilter> meshes = new List<MeshFilter>();
	private GameObject[,] points;
	private float scale=1f;
	public float a = 0f;
	private float da = 0f;
	private float e = 2.7182818f;
	// private float a = 0f;
	float fx (float s, float t){
		// return (1.25f+0.5f*Mathf.Cos(s))*Mathf.Cos(t);
		return Mathf.Cos(s);
		// return s;
	}
	float fy (float s, float t){
		// return (1.25f+0.5f*Mathf.Cos(s))*Mathf.Sin(t);
		return Mathf.Cos(a)*Mathf.Sin(s)+Mathf.Sin(a)*Mathf.Sin(t);
		// return Mathf.Cos(a)*s+Mathf.Sin(a)*Mathf.Exp(s)*Mathf.Cos(t);
	}
	float fu (float s, float t){
		// return 0.5f*Mathf.Sin(s);
		return Mathf.Cos(t);
		// return Mathf.Exp(s)*Mathf.Sin(t);
	}
	float fv (float s, float t){
		return -Mathf.Sin(a)*Mathf.Sin(s)+Mathf.Cos(a)*Mathf.Sin(t);
		// return -Mathf.Sin(a)*s+Mathf.Cos(a)*Mathf.Exp(s)*Mathf.Cos(t);
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
	public void ChangeA(float d){
		da = d;
	}
	void UpdatePoints(){
		for (int s = 0; s < Sres; s++){
			for (int t = 0; t < Tres; t++){
				GameObject p = points[s, t];
				float cS = ((float)((maxS-minS) * s) / Sres) + minS;
				float cT = ((float)((maxT-minT) * t) / Tres) + minT;
				float x = (1/(Mathf.Sqrt(2)-fv(cS, cT)))*fx(cS, cT);
				float y = (1/(Mathf.Sqrt(2)-fv(cS, cT)))*fy(cS, cT);
				float z = (1/(Mathf.Sqrt(2)-fv(cS, cT)))*fu(cS, cT);
				// float x = fx(cS, cT);
				// float y = fy(cS, cT);
				// float z = fu(cS, cT);

				p.transform.localPosition = new Vector3 (x, y, z);
			}
		}
		ReDrawLines();
	}
	void ReDrawLines(){
		for(int i=0; i<Sres; i++){
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
		for(int i=0; i<Sres; i++){
			LineRenderer lr = lrs[i+Tres+Sres];
			lr.positionCount = Tres;
			Vector3[] poss = new Vector3[lr.positionCount];
			for(int j=0; j<Tres; j++){
				poss[j] = points[(i+j)%Sres,j].transform.localPosition;
			}
			lr.SetPositions(poss);
		}
		// for(int j=0; j<Tres; j++){
		// 	LineRenderer lr = lrs[j+Sres+Tres+Sres];
		// 	lr.positionCount = Sres;
		// 	Vector3[] poss = new Vector3[lr.positionCount];
		// 	for(int i=0; i<Sres; i++){
		// 		poss[i] = points[i,(j-i+Tres)%Tres].transform.localPosition;
		// 	}
		// 	lr.SetPositions(poss);
		// }
		// for(int i=0; i<Sres; i++){
		// }
	}
	void Update(){
		for(int i=0; i<lrs.Count; i++){
			lrs[i].startWidth = scale*0.03f;
		}
		// for(int i=lrs.Count/2; i<lrs.Count; i++){
		// 	lrs[i].startWidth = scale*0.01f;
		// }
	}
	void FixedUpdate(){
		if(da!=0){
			if(da>0){
				a += 0.01f;
			}else{
				a -= 0.01f;
			}
			UpdatePoints();
		}
	}
	void Start (){
		points = new GameObject[Sres, Tres];
		for (int s = 0; s < Sres; s++){
			for (int t = 0; t < Tres; t++){
				GameObject p = Instantiate (pointPrefab) as GameObject;
				float cS = ((float)((maxS-minS) * s) / Sres) + minS;
				float cT = ((float)((maxT-minT) * t) / Tres) + minT;
				float x = (1/(Mathf.Sqrt(2)-fv(cS, cT)))*fx(cS, cT);
				float y = (1/(Mathf.Sqrt(2)-fv(cS, cT)))*fy(cS, cT);
				float z = (1/(Mathf.Sqrt(2)-fv(cS, cT)))*fu(cS, cT);
				// float x = fx(cS, cT);
				// float y = fy(cS, cT);
				// float z = fu(cS, cT);

				p.transform.position = new Vector3 (x, y, z);
				p.transform.parent = transform;
				points [s, t] = p;
			}
		}
		DrawLines();
		CreateMeshes();
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
		for(int i=0; i<Sres; i++){
			GameObject e = Instantiate(DLR) as GameObject;
			e.transform.parent = transform;
			LineRenderer lr = e.GetComponent<LineRenderer>();
			lr.positionCount = Tres;
			Vector3[] poss = new Vector3[lr.positionCount];
			for(int j=0; j<Tres; j++){
				poss[j] = points[(i+j)%Sres,j].transform.position;
			}
			lr.SetPositions(poss);
			lrs.Add(lr);
		}
		// for(int j=0; j<Tres; j++){
		// 	GameObject e = Instantiate(DLR) as GameObject;
		// 	e.transform.parent = transform;
		// 	LineRenderer lr = e.GetComponent<LineRenderer>();
		// 	lr.positionCount = Sres;
		// 	Vector3[] poss = new Vector3[lr.positionCount];
		// 	for(int i=0; i<Sres; i++){
		// 		poss[i] = points[i,(j-i+Tres)%Tres].transform.position;
		// 	}
		// 	lr.SetPositions(poss);
		// 	lrs.Add(lr);
		// }

		// for(int i=0; i<Sres; i++){
		// }
	}
	void CreateMeshes(){
		// // GameObject m = Instantiate(meshPrefab) as GameObject;
		// // Mesh mesh = new Mesh();
		// // m.GetComponent<MeshFilter>().mesh = mesh;
		// // Vector3[] vs = {new Vector3(0,0,0), new Vector3(0,1,0), new Vector3(1,0,0)};
		// // mesh.vertices = vs;
		// // int[] tri = new int[3];
		// // m.transform.parent = transform;
		// // tri[0] = 0;
		// // tri[1] = 1;
		// // tri[2] = 2;
		// // mesh.triangles = tri;
		// GameObject m = Instantiate(meshPrefab) as GameObject;
		// Mesh mesh = new Mesh();
		// m.GetComponent<MeshFilter>().mesh = mesh;
		// Vector3[] vs = new Vector3[Sres*Tres];
		// for(int i=0; i<Sres; i++){
		// 	for(int j=0; j<Tres; j++){
		// 		vs[i*Tres+j] = points[i, j].transform.position;
		// 	}
		// }
		// mesh.vertices = vs;
		// int[] tris = new int[Sres*Tres*2*3];
		// // for(int i=0; i<Sres; i++){
		// 	// for(int j=0; j<Tres; j++){
		// int i=0;
		// int j=0;
		// 		int ind = i*Tres*6+j*6;
		// 		tris[ind] = i*Tres+j;
		// 		tris[ind+1] = (i+1)*Tres+j;
		// 		tris[ind+2] = (i+1)*Tres+j+1;
		// 		ind += 3;
		// 		tris[ind] = i*Tres+j;
		// 		tris[ind+1] = i*Tres+j+1;
		// 		tris[ind+2] = (i+1)*Tres+j+1;
		// 		// Debug.Log((i*Tres+j) + " " + ((i+1)*Tres+j)+ " " + ((i+1)*Tres+j+1));
		// 	// }
		// // }
		// // Vector2[] uvs = new Vector2[vs.Length];
    // // for (int i = 0; i < uvs.Length; i++)
    // // {
    // // 	uvs[i] = new Vector2(vs[i].x, vs[i].z);
    // // }
    // // mesh.uv = uvs;
		// mesh.triangles = tris;
		// m.transform.parent = transform;
	}
}
