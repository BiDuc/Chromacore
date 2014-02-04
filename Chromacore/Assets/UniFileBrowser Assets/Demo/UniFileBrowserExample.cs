// Example of open/save usage with UniFileBrowser
// This script is free to use in any manner

using UnityEngine;
using System;
using System.Collections;

public class UniFileBrowserExample : MonoBehaviour {
	
	string message = "";
	float alpha = 1.0f;
	char pathChar = '/';

	/*
	bool pcP = false;
	bool iphoneP = false;
	bool androidP = false;
	
	// The WWW data
	private WWW wwwData;
	// The instance of this class used to download
	private static UniFileBrowserExample downloadManager = null;
	*/
	
	void Start () {
		/*
		// Initialization of download manager
		if(UniFileBrowserExample.downloadManager == null){
			UniFileBrowserExample.downloadManager = FindObjectOfType(typeof(UniFileBrowserExample)) as UniFileBrowserExample;
		}*/
		
		
		if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer) {
			pathChar = '\\';
		}
	}
	
	void OnGUI () {
		GUIStyle buttonStyle = new GUIStyle("button");
		buttonStyle.fontSize = 25;
		
		// Only load the File Browser in the Song Browser scene
		if (Application.loadedLevelName != "SongBrowser"){
			return;
		}else{
			// Only load the File Browser in the Song Browser scene
			if (GUI.Button (new Rect(Screen.width/2, Screen.height/2, 200, 100), "Browse", buttonStyle)) {
				if (UniFileBrowser.use.allowMultiSelect) {
					UniFileBrowser.use.OpenFileWindow (OpenFiles);
				}
				else {
					UniFileBrowser.use.OpenFileWindow (OpenFile);
				}
			}
			
			if (GUI.Button(new Rect(Screen.width/2 + Screen.width/4, Screen.height/2 + Screen.height/4, 200, 100), "Back", buttonStyle)){
					Application.LoadLevel("MainMenu");
			}
			
			/*
			if (GUI.Button (new Rect(100, 125, 95, 35), "Save")) {
				UniFileBrowser.use.SaveFileWindow (SaveFile);
			}
			if (GUI.Button (new Rect(100, 200, 95, 35), "Open Folder")) {
				UniFileBrowser.use.OpenFolderWindow (true, OpenFolder);
			}
			*/
			
			var col = GUI.color;
			col.a = alpha;
			GUI.color = col;
			GUI.Label (new Rect(100, 275, 500, 1000), message);
			col.a = 1.0f;
			GUI.color = col;
		}
	}
	
	void OpenFile (string pathToFile) {
		var fileIndex = pathToFile.LastIndexOf (pathChar);
		message = "You selected file: " + pathToFile.Substring (fileIndex+1, pathToFile.Length-fileIndex-1);
		
		// Pass file path to Loading Screen, then load loading screen
		LoadingScreen loadScreen = GetComponent<LoadingScreen>();
		loadScreen.RecieveFilePath(pathToFile.ToString());

		if (loadScreen.shouldLoadP == true){
			Application.LoadLevel("LoadingScreen");
		}
		
		Fade();
	}
	
	void OpenFiles (string[] pathsToFiles) {
		message = "You selected these files:\n";
		for (var i = 0; i < pathsToFiles.Length; i++) {
			var fileIndex = pathsToFiles[i].LastIndexOf (pathChar);
			message += pathsToFiles[i].Substring (fileIndex+1, pathsToFiles[i].Length-fileIndex-1) + "\n";
		}
		Fade();
	}

	/*
	void SaveFile (string pathToFile) {
		var fileIndex = pathToFile.LastIndexOf (pathChar);
		message = "You're saving file: " + pathToFile.Substring (fileIndex+1, pathToFile.Length-fileIndex-1);
		Fade();
	}
	
	void OpenFolder (string pathToFolder) {
		message = "You selected folder: " + pathToFolder;
		Fade();
	}
	*/
	
	void Fade () {
		StopCoroutine ("FadeAlpha");	// Interrupt FadeAlpha if it's already running, so only one instance at a time can run
		StartCoroutine ("FadeAlpha");
	}
	
	IEnumerator FadeAlpha () {
		alpha = 1.0f;
		yield return new WaitForSeconds (5.0f);
		for (alpha = 1.0f; alpha > 0.0f; alpha -= Time.deltaTime/4) {
			 yield return null;
		}
		message = "";
	}
}