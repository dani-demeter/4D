using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour {
	public int buttonNumber;
	public string fcn;
	public GraphsScript graphs ;
	public GameObject axes;
	public GameManagerScript gm;
	public GameObject linesParent;
	public GameObject meshParent;

	private bool axesOn=false;
	private bool linesOn = false;
	private bool showSecGraph = false;
	void OnTriggerEnter(Collider c){
		if(c.transform.root.tag == "Player"){
			if(buttonNumber == 0){
				graphs.ChangeA(-1f);
			}else if(buttonNumber == 1){
				graphs.ChangeA(0f);
			}else if(buttonNumber == 2){
				graphs.ChangeA(1f);
			}else if(buttonNumber == 3){
				if(fcn=="axes"){
					axesOn = !axesOn;
					axes.SetActive(axesOn);
				}else if(fcn=="secGraph"){
					showSecGraph = !showSecGraph;
					graphs.ShowOnlyOneGraph(showSecGraph);
				}else if(fcn=="linTri"){
					linesOn = !linesOn;
					graphs.toggleLines(linesOn);
				}
			}else if(buttonNumber == 4){
				gm.HandleButton(4, fcn);
			}else if(buttonNumber==5){
				gm.HandleButton(5, fcn);
			}else if(buttonNumber==6){
				gm.Go();
			}else if(buttonNumber==7){
				if(fcn=="custom"){
					gm.fxT.text = "fx =s";
					gm.fyT.text = "fy =sqrt((s-t)*(s-t))";
					gm.fuT.text = "fu =t";
					gm.fvT.text = "fv =1";
					gm.Go();
				}else if(fcn == "torus"){
					gm.fxT.text = "fx =cos(s)";
					gm.fyT.text = "fy =cos(a)*sin(s)+sin(a)*sin(t)";
					gm.fuT.text = "fu =cos(t)";
					gm.fvT.text = "fv =-sin(a)*sin(s)+cos(a)*sin(t)";
					gm.Go();
				}
			}else if(buttonNumber==8){
				if(fcn=="0"){
					graphs.SetA(0);
				}else if(fcn == "pi/4"){
					graphs.SetA(0.785f);
				}else if(fcn == "pi/2"){
					graphs.SetA(1.57f);
				}
				graphs.UpdatePoints();
			}
		}
	}
}
