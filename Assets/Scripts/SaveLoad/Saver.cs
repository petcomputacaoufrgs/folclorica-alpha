using UnityEngine;
using System.Collections;

public class Saver : MonoBehaviour {

	public GameObject player;

	private SaveAssistant playerSaver;

	void Start () {
		playerSaver = new SaveAssistant (player, "player");
		playerSaver.LoadPosition (player);
	}

	public void Save () {
		Debug.Log ("Saving");
		playerSaver.SetToSavePosition (player);
		SaveLoad.Save ();
	}

	public void ReturnMenu () {
		Application.LoadLevel ("Menu");
	}
}
