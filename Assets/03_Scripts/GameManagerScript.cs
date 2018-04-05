using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {
	public GameObject graph;
	public GraphsScript graphs;
	[Space(10)]
	public float scale = 1f;
	private float prevScale = 1f;
	[Space(10)]
	public Transform rightHand;
	public Transform leftHand;
	[Space(10)]
	public bool rightTP;
	public bool leftTP;

	private float dist = 0f;
	private float Odist = 0f;


	[Space(10)]
	public TextMesh fxT;
	public TextMesh fyT;
	public TextMesh fuT;
	public TextMesh fvT;

	private bool fxA = false;
	private bool fyA = false;
	private bool fuA = false;
	private bool fvA = false;
	void Start () {

	}
	public void setRightTP(bool b){
		rightTP = b;
		if(b){
			if(leftTP){
				graph.transform.parent = null;
				prevScale = scale;
				Odist = (rightHand.position-leftHand.position).magnitude;
			}else{
				graph.transform.parent = rightHand;
			}
		}else{
			if(leftTP){
				graph.transform.parent = leftHand;
			}else{
				graph.transform.parent = null;
			}
		}
	}
	public void setLeftTP(bool b){
		leftTP = b;
		if(b){
			if(rightTP){
				graph.transform.parent = null;
				prevScale = scale;
				Odist = (rightHand.position-leftHand.position).magnitude;
			}else{
				graph.transform.parent = leftHand;
			}
		}else{
			if(rightTP){
				graph.transform.parent = rightHand;
			}else{
				graph.transform.parent = null;
			}
		}
	}
	void Update () {
		if(rightTP && leftTP){
			dist = (rightHand.position-leftHand.position).magnitude;
			scale = prevScale*dist/Odist;
			graph.transform.localScale = new Vector3(scale, scale, scale);
			graphs.SetScale(scale);
		}

	}
	public void Go(){
		HandleButton(5, "enter");
		graphs.Go(fxT.text.Substring(4,fxT.text.Length-4), fyT.text.Substring(4,fyT.text.Length-4), fuT.text.Substring(4,fuT.text.Length-4), fvT.text.Substring(4,fvT.text.Length-4));
	}
	public void HandleButton(int num, string fcn){
		if(num==4){
			deleteCursor();
			if(fcn=="fx"){
				fxT.text += "|";
				fxA = true;
				fyA = false;
				fuA = false;
				fvA = false;
			}else if(fcn=="fy"){
				fyT.text += "|";
				fxA = false;
				fyA = true;
				fuA = false;
				fvA = false;
			}else if(fcn=="fu"){
				fuT.text += "|";
				fxA = false;
				fyA = false;
				fuA = true;
				fvA = false;
			}else if(fcn=="fv"){
				fvT.text += "|";
				fxA = false;
				fyA = false;
				fuA = false;
				fvA = true;
			}
		}else if(num==5){
			TextMesh active = null;
			if(fxA){
				active = fxT;
			}else if(fyA){
				active = fyT;
			}else if(fuA){
				active = fuT;
			}else if(fvA){
				active = fvT;
			}
			if(active!=null){
				deleteCursor();
				if(fcn=="del"){
					if(active.text.Length>4){
						deleteCursor();
					}
					active.text += "|";
				}else if(fcn=="enter"){
					fxA = false;
					fyA = false;
					fuA = false;
					fvA = false;
				}else{
					active.text += (fcn+"|");
				}
			}
		}
	}
	void deleteCursor(){
		if(fxA){
			fxT.text = fxT.text.Substring(0, fxT.text.Length-1);
		}else if(fyA){
			fyT.text = fyT.text.Substring(0, fyT.text.Length-1);
		}else if(fuA){
			fuT.text = fuT.text.Substring(0, fuT.text.Length-1);
		}else if(fvA){
			fvT.text = fvT.text.Substring(0, fvT.text.Length-1);
		}
	}
}
