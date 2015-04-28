using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Game {
	
	public static Game current = null;

	private int id;
	private int lifes;
	private int points;
	private string levelName;

	private Dictionary<string, Hashtable> saveables;

	public Game (int gameId, string firstLevelName, int lifes) {
		this.id = gameId;
		this.lifes = lifes;
		this.points = 0;
		this.levelName = firstLevelName;

		this.saveables = new Dictionary<string, Hashtable>();
	}

	/// <summary>
	/// Creates a new game object and it's file.
	/// </summary>
	/// <param name="firstLevelName">First level name.</param>
	/// <param name="lifeAmount">Life amount.</param>
	public static void NewGame (string firstLevelName, int lifeAmount) {
		SaveLoad.LoadSavedData ();
		Game.current = new Game (SavedData.current.NewData (), firstLevelName, lifeAmount);
		SaveLoad.Save ();
	}

	public int GetId () {
		return id;
	}

	public int GetLifes () {
		return lifes;
	}

	public void RemoveOneLife () {
		this.lifes--;
	}
	
	public void IncrementOneLife () {
		this.lifes++;
	}

	public int GetPoint () {
		return points;
	}

	public void AddPoints (int quantity) {
		this.points += Mathf.Abs (quantity);
	}

	public string GetLevelName () {
		return levelName;
	}

	public void LoadScene () {
		Application.LoadLevel (this.levelName);
	}

	/// <summary>
	/// Returns the list of saved components of the given object.
	/// </summary>
	/// <returns>The saved components.</returns>
	/// <param name="savename">The object label.</param>
	public Hashtable GetSavedValues (string savename) {
		if (saveables.ContainsKey (savename)) {
			return this.saveables[savename];
		} else {
			return new Hashtable(6, 0.8F);
		}
	}

	public void SetSavedValues (string savename, Hashtable values) {
		saveables[savename] = values;
	}

	/// <summary>
	/// Return true if the object was saved, otherwise false.
	/// </summary>
	/// <returns><c>true</c>, if objected was saved, <c>false</c> otherwise.</returns>
	/// <param name="savename">The object label.</param>
	public bool ObjectSaved (string savename) {
		if (this.saveables.ContainsKey (savename))
			return true;
		else
			return false;
	}
}
