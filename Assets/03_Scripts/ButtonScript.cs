using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour {
	public int buttonNumber;
	public Grapher grapher;
	public GameObject axes;

	private bool axesOn=true;
	void OnTriggerEnter(Collider c){
		if(c.transform.root.tag == "Player"){
			if(buttonNumber == 0){
				grapher.ChangeA(-1f);
			}else if(buttonNumber == 1){
				grapher.ChangeA(0f);
			}else if(buttonNumber == 2){
				grapher.ChangeA(1f);
			}else if(buttonNumber == 3){
				axesOn = !axesOn;
				axes.SetActive(axesOn);
			}
		}
	}
}
