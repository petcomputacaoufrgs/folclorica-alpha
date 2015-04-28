using UnityEngine;
using System.Collections;

public class BoarMovement : MonoBehaviour {

	/*
	 * This is an example on using SaveAssistant
	*/

	public float range;
	public GameObject player;

	private float startingPosition;
	private bool directionLeft;
	private bool live;

	private SaveAssistant savedData;
	
	void Start () {
		savedData = new SaveAssistant(this.gameObject, this.gameObject.name + "." + this.GetType().Name);
		savedData.LoadPosition (this.gameObject);

		startingPosition = gameObject.transform.position.x;
		directionLeft = true;
		live = savedData.LoadValue<bool> ("live", true);
	}

	void Update () {
		if (live) {
			gameObject.transform.Translate((directionLeft ? Vector3.left : Vector3.right) * Time.deltaTime*3);
			if (gameObject.transform.position.x < startingPosition - range && directionLeft)
				directionLeft = false;
			
			if (gameObject.transform.position.x > startingPosition + range && !directionLeft)
				directionLeft = true;

			if (gameObject.transform.position.x - player.transform.position.x < 1)
			{
				live = false;
				savedData.SetToSave ("live", live);
				savedData.SetToSavePosition (this.gameObject);
			}
		}
	}
}
