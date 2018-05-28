using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Windows.Forms;

public class SelectTime : MonoBehaviour {
	public UnityEngine.UI.Button[] buttons;
	[SerializeField]
	UnityEngine.UI.Text dirText;
	
	
	void Start(){
		float[] times = new float[]{
			30   ,60   ,90,
			60* 2,60* 5,60*10,
			60*20,60*40,60*60,
		};
		int index = 0;
		foreach( var button in buttons ){
			int i = index;
			button.onClick.AddListener( ()=>{
				var data = FindObjectOfType<GlobalData>();
				data.time = times[i];
				UnityEngine.SceneManagement.SceneManager.LoadScene("Drawing");
			});
			index++;
		}
		
		{
			var data = FindObjectOfType<GlobalData>();
			dirText.text = data.directory;
		}
	}
	
	
	public void OpenFolder(){
		FolderBrowserDialog fbd = new FolderBrowserDialog();
		//上部に表示する説明テキストを指定する
		fbd.Description = "フォルダを指定してください。";
		//ルートフォルダを指定する
		//デフォルトでDesktop
		fbd.RootFolder = System.Environment.SpecialFolder.Desktop;
		//最初に選択するフォルダを指定する
		//RootFolder以下にあるフォルダである必要がある
		fbd.SelectedPath = @"C:\Windows";
		//ユーザーが新しいフォルダを作成できるようにする
		//デフォルトでTrue
		fbd.ShowNewFolderButton = true;
		//ダイアログを表示する
		if (fbd.ShowDialog() == DialogResult.OK)
		{
		    //選択されたフォルダを表示する
		    Debug.Log(fbd.SelectedPath);
		    var data = FindObjectOfType<GlobalData>();
			data.SetDir(fbd.SelectedPath);
			dirText.text = fbd.SelectedPath;
		}
	}
}
