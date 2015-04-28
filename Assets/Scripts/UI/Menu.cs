using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Menu : MonoBehaviour {

	private List<SavedData.gameData> data = null;

	void OnGUI () {
		int i = 10;
		foreach (SavedData.gameData game in data) {
			if (GUI.Button(new Rect(10, i, 200, 50), game.levelName + "\n" + game.time.ToString ())){
				SaveLoad.Load (game.id);
				Game.current.LoadScene ();
			}
			if (GUI.Button(new Rect(205, i, 70, 50), "Delete")) {
				SaveLoad.DeleteSavedData (game.id);
				data = SavedData.current.GetData ();
			}
			i += 55;
		}
	}

	void Start () {
		SaveLoad.LoadSavedData ();
		data = SavedData.current.GetData ();
	}

	public void NewGame () {
		Game.NewGame ("Save Example", 3);
		Game.current.LoadScene ();
	}

	public void ContinueGame () {
		SaveLoad.LoadSavedData ();
		List<SavedData.gameData> data = SavedData.current.GetData ();
		int id = -1;
		string levelName = "";
		foreach (SavedData.gameData game in data) {

			id = game.id;
			levelName = game.levelName;
		}
		if (id != -1) {
			Debug.Log ("Loading level " + levelName);
			SaveLoad.Load (id);
		}
		Game.current.LoadScene ();
	}

	public void CleanGames () {
		SaveLoad.CleanSavedData ();
		data = new List<SavedData.gameData> ();
	}
}
