using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData {
	static GlobalData _instance;
	public static GlobalData instance {
		get {
			if (_instance == null) {
				_instance = new GlobalData ();
			}
			return _instance;
		}
	}

	const string KEY_DIR = "KEY_DIR";

	public float drawingTimeSec;
	string _directory;
	public string directory {
		get {
			return _directory;
		}
		set {
			_directory = value;
			PlayerPrefs.SetString (KEY_DIR, _directory);
		}
	}
	public bool randomActive = true;

	// Use this for initialization
	GlobalData () {
		directory = PlayerPrefs.GetString (KEY_DIR, "");
	}
}