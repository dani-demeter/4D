using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour {
	public int buttonNumber;
	public string fcn;
	public Grapher grapher;
	public GameObject axes;
	public GameManagerScript gm;

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
			}else if(buttonNumber == 4){
				gm.HandleButton(4, fcn);
			}else if(buttonNumber==5){
				gm.HandleButton(5, fcn);
			}
		}
	}
}
