using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphsScript : MonoBehaviour {
	public bool playDemo = true;

	private float da;
	private float a;
	private float b1 = 2*Mathf.PI;
	private float b2 = 4*Mathf.PI;
	private float b3 = 6*Mathf.PI;
	private float b4 = 8*Mathf.PI;
	private bool b1d, b2d, b3d, b4d = false;

	Grapher[] graphers;
	void Start () {
		graphers = new Grapher[transform.childCount];
		int index = 0;
		foreach(Transform t in transform){
			graphers[index] = t.GetComponent<Grapher>();
			index++;
		}
		if(playDemo){
			ChangeA(1f);
		}
	}


	void Update () {
		if(playDemo){
			if(a>b4){
				if(!b4d){
					b4d = true;
					playDemo = false;
					toggleDLines(true);
					ChangeA(0f);
					SetA(0f);
					// toggleLines();
				}
			}else if(a>b3){
				if(!b3d){
					b3d = true;
					toggleUVLines(true);
					toggleDLines(false);
				}
			}else if(a>b2){
				if(!b2d){
					b2d = true;
					toggleUVLines(false);
				}
			}else if(a>b1){
				if(!b1d){
					b1d = true;
					toggleLines(true);
				}
			}
		}
	}
	void FixedUpdate(){
		if(da!=0){
			if(da>0){
				a += 0.01f;
			}else{
				a -= 0.01f;
			}
		}
	}
	public void toggleUVLines(bool b){
		foreach(Grapher g in graphers){
			g.toggleUVLines(b);
		}
	}
	public void toggleDLines(bool b){
		foreach(Grapher g in graphers){
			g.toggleDLines(b);
		}
	}
	public void toggleLines(bool b){
		foreach(Grapher g in graphers){
			g.toggleLines(b);
		}
	}
	public void ShowOnlyOneGraph(bool b){
		int index = 0;
		float fa=0f;
		foreach(Grapher g in graphers){
			if(index>0){
				g.gameObject.SetActive(!b);
				if(b){
					g.a = fa;
				}
			}else{
				fa = g.a;
			}
			index++;
		}
	}
	public void ChangeA(float d){
		da = d;
		foreach(Grapher g in graphers){
			g.ChangeA(d);
		}
	}
	public void SetA(float a){
		this.a = a;
		foreach(Grapher g in graphers){
			g.a = a;
		}
		UpdatePoints();
	}
	public void UpdatePoints(){
		foreach(Grapher g in graphers){
			g.UpdatePoints();
		}
	}
	public void SetScale(float f){
		foreach(Grapher g in graphers){
			g.SetScale(f);
		}
	}
	public void Go(string sfx, string sfy, string sfu, string sfv){
		foreach(Grapher g in graphers){
			g.Go(sfx, sfy, sfu, sfv);
		}
	}
}
