using UnityEngine;
using System.Collections;

public class RotateToOrigin : MonoBehaviour {


	private Quaternion parent_rotation;

	bool parent_changed_rotation = false;

	void Start(){

		/*parent_rotation = transform.parent.rotation;

		Debug.Log("Rotacao original: " + parent_rotation);*/
	}
	// Update is called once per frame
	void FixedUpdate () {
	
		/*if(parent_rotation != transform.parent.rotation){
			parent_rotation = transform.parent.rotation;

			parent_changed_rotation = true;
		}else{
			Debug.Log("Nova rotacao: " + parent_rotation);
		}*/
		//Debug.Log(parent.rotation); //0,0,0,1
	}
}
