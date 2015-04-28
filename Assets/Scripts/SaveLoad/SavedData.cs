using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class SavedData {

	public static SavedData current;

	// Struct for the data of the saved games
	[System.Serializable]
	public struct gameData {
		public int id;
		public string levelName;
		public DateTime time;
	};

	private int currentId;	// The ID for the current saved game
	private Dictionary<int, gameData> savedGames;	// The list of saved games

	public SavedData() {
		currentId = -1;
		savedGames = new Dictionary<int, gameData> ();
	}

	/// <summary>
	/// Create an id for the new game to be saved.
	/// </summary>
	/// <returns>The id.</returns>
	public int NewData () {
		return ++currentId;
	}

	/// <summary>
	/// Update the time of the saved game and it's level name.
	/// </summary>
	public void UpdateData () {
		gameData currentData;
		int id;
		id = Game.current.GetId ();

		currentData.id = id;
		currentData.levelName = Game.current.GetLevelName ();
		currentData.time = DateTime.Now;

		if (savedGames.ContainsKey (id))
			savedGames.Remove (id);
		savedGames.Add (id, currentData);
	}

	/// <summary>
	/// Gets a list of saved games.
	/// </summary>
	/// <returns>The games list.</returns>
	public List<gameData> GetData () {
		if (savedGames.Count <= 0)
			return new List<gameData> ();
		else
			return savedGames.Values.ToList ();
	}

	public void DeleteData (int id) {
		if (savedGames.ContainsKey (id)) {
			savedGames.Remove (id);
		}
	}
}