using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRScript : MonoBehaviour {
	private GameObject[] myPoints;
	// Use this for initialization
	void Start () {

	}
	public void SetPoints(GameObject[] ps){
		myPoints = new GameObject[ps.Length];
		for(int i=0; i<myPoints.Length; i++){
			myPoints[i] = ps[i];
			print(myPoints[i]);
		}
	}
	public void Recalculate(){
		Vector3[] poss = new Vector3[myPoints.Length];
		for(int i=0; i<poss.Length; i++){
			poss[i] = myPoints[i].transform.position;
		}
		GetComponent<LineRenderer>().SetPositions(poss);
	}
	// Update is called once per frame
	void Update () {

	}
}
