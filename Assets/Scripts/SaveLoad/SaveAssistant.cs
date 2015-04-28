using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SaveAssistant {

	private string savename;

	public SaveAssistant (GameObject objectToSave, string label) {
		if (Game.current == null)
			Game.NewGame (Application.loadedLevelName, 3);
		savename = GetSavename (objectToSave, label);
	}

	/// <summary>
	/// Return an unique savename based on label and position.
	/// </summary>
	/// <returns>The savename.</returns>
	/// <param name="objectToSave">Object use as reference.</param>
	/// <param name="label">Label.</param>
	static string GetSavename (GameObject objectToSave, string label) {
		return label + "." + Application.loadedLevelName
					 + "." + objectToSave.transform.position.x
					 + "." + objectToSave.transform.position.y
					 + "." + objectToSave.transform.position.z;
	}
	
	/// <summary>
	/// Loads the value if it's saved, otherwise returns the default value.
	/// </summary>
	/// <returns>The saved value or default value.</returns>
	/// <param name="label">The label used to identify the value.</param>
	/// <param name="defaultValue">Default value if it's not saved yet.</param>
	/// <typeparam name="T">The type of the value.</typeparam>
	public T LoadValue<T> (string label, T defaultValue) {
		// If the object hasn't been saved, then return the default value
		if (Game.current.ObjectSaved(savename) == false)
			return defaultValue;

		Hashtable values = Game.current.GetSavedValues (savename);
		if (values.Contains (label) == false)
			return defaultValue;

		return (T)values[label];
	}

	/// <summary>
	/// Sets the value to be saved the next time the game is saved.
	/// </summary>
	/// <param name="label">A label to identify the value.</param>
	/// <param name="value">The value to be saved.</param>
	/// <typeparam name="T">The type of the value.</typeparam>
	public void SetToSave<T> (string label, T value) {
		Hashtable values = Game.current.GetSavedValues (savename);
		values[label] = value;
		Game.current.SetSavedValues (savename, values);
	}

	/// <summary>
	/// Loads the position of the given object, if it's not saved yet keeps in the same position.
	/// </summary>
	/// <param name="target">The object to be moved.</param>
	public void LoadPosition (GameObject target) {
		if (Game.current.ObjectSaved(savename) == false)
			return;

		float x, y, z;
		Hashtable values = Game.current.GetSavedValues (savename);
		if (values.Contains ("transform.position.x")) {
			x = (float)values["transform.position.x"];
		} else {
			x = target.transform.position.x;
		}
		if (values.Contains ("transform.position.y")) {
			y = (float)values["transform.position.y"];
		} else {
			y = target.transform.position.y;
		}
		if (values.Contains ("transform.position.z")) {
			z = (float)values["transform.position.z"];
		} else {
			z = target.transform.position.z;
		}
		target.transform.position = new Vector3(x,y,z);
	}

	/// <summary>
	/// Sets the position to be saved next time the game is saved.
	/// </summary>
	/// <param name="target">The object to have its position saved.</param>
	public void SetToSavePosition (GameObject target) {
		Hashtable values = Game.current.GetSavedValues (savename);
		values["transform.position.x"] = target.transform.position.x;
		values["transform.position.y"] = target.transform.position.y;
		values["transform.position.z"] = target.transform.position.z;
		Game.current.SetSavedValues (savename, values);
	}
}
