using System.Collections;
using System.Collections.Generic;
using SimpleFileBrowser;
using UnityEngine;
using UnityEngine.UI;

public class SelectTime : MonoBehaviour {
	public UnityEngine.UI.Button[] buttons;
	[SerializeField]
	UnityEngine.UI.Text dirText;
	[SerializeField]
	UnityEngine.UI.Toggle randomFlag;
	GlobalData g;

	void Start () {
		g = GlobalData.instance;
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
				g.drawingTimeSec = times[i];
				g.randomActive = randomFlag.isOn;
				UnityEngine.SceneManagement.SceneManager.LoadScene ("Drawing");
			});
			index++;
		}
		dirText.text = g.directory;
	}

	public void OpenFolder () {
		SimpleFileBrowser.FileBrowser.ShowLoadDialog ((path) => {
			Debug.Log (path);
			g.directory = path;
			dirText.text = path;
		}, () => { }, true, g.directory);
	}
}