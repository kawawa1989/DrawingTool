using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Drawing : MonoBehaviour {
	[SerializeField]
	UnityEngine.UI.RawImage image;
	[SerializeField]
	UnityEngine.UI.Image timerImage;
	[SerializeField]
	UnityEngine.UI.Text timerText;

	DirectoryInfo dInfo;
	Texture2D currentTexture = null;
	float time = 0;
	GlobalData g;
	DateTime startDT;
	float timeScale = 1;
	FileSelector selector;

	// Use this for initialization
	void Start () {
		g = GlobalData.instance;
		dInfo = new DirectoryInfo (g.directory);
		if (g.randomActive) {
			selector = new RandomSelector (dInfo.GetFiles ());
		} else {
			selector = new SequencialSelector (dInfo.GetFiles ());
		}
	}

	// Update is called once per frame
	void Update () {
		if (currentTexture == null) {
			var f = selector.currentFile;
			LoadTexture (f.FullName);
			Debug.Log ("size:" + currentTexture.width + "," + currentTexture.height);
			startDT = DateTime.Now;
			if (!selector.HasNext ()) {
				selector.Reset ();
			}
		}

		if (time >= g.drawingTimeSec) {
			Next ();
		}

		var current = startDT.AddSeconds (time);
		var progressDT = current - startDT;
		time += Time.deltaTime * timeScale;
		timerImage.fillAmount = time / g.drawingTimeSec;
		timerText.text = string.Format ("{0:00}:{1:00}", progressDT.Minutes, progressDT.Seconds);
	}

	[SerializeField]
	GameObject stopButton;
	[SerializeField]
	GameObject resumeButton;

	public void Stop () {
		timeScale = 0;
		stopButton.SetActive (false);
		resumeButton.SetActive (true);
	}

	public void Resume () {
		timeScale = 1;
		stopButton.SetActive (true);
		resumeButton.SetActive (false);
	}

	public void Next () {
		if (currentTexture != null) {
			Destroy (currentTexture);
		}
		currentTexture = null;
		time = 0;
		selector.Next ();
	}

	public void Prev () {
		if (currentTexture != null) {
			Destroy (currentTexture);
		}
		currentTexture = null;
		time = 0;
		selector.Prev ();
	}

	public void BackToMenu () {
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Main");
	}

	void LoadTexture (string filePath) {
		var bytes = System.IO.File.ReadAllBytes (filePath);
		float screenWidth = 1920;
		float screenHeight = 1200;

		currentTexture = new Texture2D (0, 0);
		currentTexture.LoadImage (bytes);

		float fw = currentTexture.width;
		float fh = currentTexture.height;
		float ratio = 0;
		if (fw > fh) {
			ratio = screenWidth / fw;
			fw = fw * ratio;
			fh = fh * ratio;
			if (fh >= screenHeight) {
				ratio = screenHeight / fh;
				fw = fw * ratio;
				fh = fh * ratio;
			}
		} else {
			ratio = screenHeight / fh;
			fw = fw * ratio;
			fh = fh * ratio;
			if (fw >= screenWidth) {
				ratio = screenWidth / fw;
				fw = fw * ratio;
				fh = fh * ratio;
			}
		}

		var rt = image.GetComponent<RectTransform> ();
		image.texture = currentTexture;
		rt.sizeDelta = new Vector2 (fw, fh);
	}
}