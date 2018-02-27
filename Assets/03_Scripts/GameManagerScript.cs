using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {
	public GameObject graph;
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
			// Debug.Log(scale);
		}

	}
}
