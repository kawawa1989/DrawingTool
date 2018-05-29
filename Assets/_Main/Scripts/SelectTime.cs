using System.Collections;
using System.Collections.Generic;
using SimpleFileBrowser;
using UnityEngine;
using UnityEngine.UI;

public class SelectTime : MonoBehaviour {
	public UnityEngine.UI.Button[] buttons;
	[SerializeField]
	UnityEngine.UI.Text dirText;

	void Start () {
		float[] times = new float[] {
			30,
			60,
			90,
			60 * 2,
			60 * 5,
			60 * 10,
			60 * 20,
			60 * 40,
			60 * 60,
		};
		int index = 0;
		foreach (var button in buttons) {
			int i = index;
			button.onClick.AddListener (() => {
				var data = FindObjectOfType<GlobalData> ();
				data.time = times[i];
				UnityEngine.SceneManagement.SceneManager.LoadScene ("Drawing");
			});
			index++;
		}

		{
			var data = FindObjectOfType<GlobalData> ();
			dirText.text = data.directory;
		}
	}

	public void OpenFolder () {
		SimpleFileBrowser.FileBrowser.ShowLoadDialog ((path) => {
		    Debug.Log(path);
		    var data = FindObjectOfType<GlobalData>();
			data.SetDir(path);
			dirText.text = path;			
		}, () => {
		}, true);
	}
}