using UnityEngine;
using System.Collections;

public class SortingLayerChange : MonoBehaviour {
	
	public string targetTag = "";
	// Use this for initialization
	/*void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}*/

	void OnTriggerExit2D(Collider2D collider){

		if(targetTag.ToLower() == "box"){
			collider.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
		}
	}
}
