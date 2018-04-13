using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphsScript : MonoBehaviour {
	private float da;
	private float a;
	Grapher[] graphers;
	void Start () {
		graphers = new Grapher[transform.childCount];
		int index = 0;
		foreach(Transform t in transform){
			graphers[index] = t.GetComponent<Grapher>();
			index++;
		}
	}


	void Update () {

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
