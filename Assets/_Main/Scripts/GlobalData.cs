using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData : MonoBehaviour {
	const string KEY_DIR = "KEY_DIR";
	
	public float time;
	public string directory;
	
	
	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad(gameObject);
		directory = PlayerPrefs.GetString(KEY_DIR, "");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void SetDir(string dir){
		directory = dir;
		PlayerPrefs.SetString(KEY_DIR, dir);
	}
}
