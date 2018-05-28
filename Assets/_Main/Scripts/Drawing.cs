using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

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
	List<FileInfo> fileList;
	GlobalData g;
	DateTime startDT;
	float timeScale = 1;
	int fileIndex = 0;
	
	// Use this for initialization
	void Start () {
		g = FindObjectOfType<GlobalData>();
		dInfo = new DirectoryInfo(g.directory);
		fileList = new List<FileInfo>(dInfo.GetFiles());
	}
	
	
	
	// Update is called once per frame
	void Update () {
		if( currentTexture == null ){
			var f = fileList[fileIndex];
			LoadTexture(f.FullName);
			Debug.Log("size:" + currentTexture.width + "," + currentTexture.height);
			startDT = DateTime.Now;
		}
		
		if( time >= g.time ){
			Next();
		}
		
		var current = startDT.AddSeconds(time);
		var progressDT = current - startDT;
		time += Time.deltaTime * timeScale;
		timerImage.fillAmount = time / g.time;
		timerText.text = string.Format("{0:00}:{1:00}", progressDT.Minutes, progressDT.Seconds);
	}
	
	[SerializeField]
	GameObject stopButton;
	[SerializeField]
	GameObject resumeButton;
	
	public void Stop(){
		timeScale = 0;
		stopButton.SetActive(false);
		resumeButton.SetActive(true);
	}
	
	public void Resume(){
		timeScale = 1;
		stopButton.SetActive(true);
		resumeButton.SetActive(false);
	}
	
	public void Next(){
		if( currentTexture != null ){
			Destroy(currentTexture);
		}
		currentTexture = null;
		time = 0;
		fileIndex = Mathf.Clamp(fileIndex + 1, 0, fileList.Count - 1);
	}
	
	public void Prev(){
		if( currentTexture != null ){
			Destroy(currentTexture);
		}
		currentTexture = null;
		time = 0;
		fileIndex = Mathf.Clamp(fileIndex - 1, 0, fileList.Count - 1);
	}
	
	void LoadTexture(string filePath){
		var bytes = System.IO.File.ReadAllBytes(filePath);
		currentTexture = new Texture2D(0,0);
		currentTexture.LoadImage(bytes);
		
		float fw = currentTexture.width;
		float fh = currentTexture.height;
		float ratio = 0;
		if( fw > fh ){
			ratio = ((float)Screen.width) / fw;
			fw = fw * ratio;
			fh = fh * ratio;
			if(fh >= Screen.height){
				ratio = ((float)Screen.height) / fh;
				fw = fw * ratio;
				fh = fh * ratio;
			}
		}
		else{
			ratio = ((float)Screen.height) / fh;
			fw = fw * ratio;
			fh = fh * ratio;
			if(fw >= Screen.width){
				ratio = ((float)Screen.width) / fw;
				fw = fw * ratio;
				fh = fh * ratio;
			}
		}
		
		var rt = image.GetComponent<RectTransform>();
		image.texture = currentTexture;
		rt.sizeDelta = new Vector2(fw, fh);
	}
}
