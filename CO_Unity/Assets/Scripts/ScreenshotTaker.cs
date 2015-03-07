using UnityEngine;
using System.Collections;
using System.IO;

public class ScreenshotTaker : MonoBehaviour {

	[SerializeField]KeyCode printscreenKeycode = KeyCode.F12;
	const string SCREENS_FOLDER = "Screens";
	[SerializeField]string projectName;
	static int ssId = 0;

	void LateUpdate(){
		if(Input.GetKeyDown(printscreenKeycode)){

			TakeScreenshot();
		}
	}

	void TakeScreenshot () {
		string folderName = Application.dataPath;
		string[] s = folderName.Split('/');
		s[s.Length-1] = SCREENS_FOLDER;
		folderName = string.Join("/", s);
		if(!Directory.Exists(folderName)){
			Directory.CreateDirectory(folderName);
		}
		print(folderName);

		ssId++;
		string _name = SCREENS_FOLDER +"/"+projectName+"_"+System.DateTime.Now.ToShortDateString().Replace('/', '.')+"-"+System.DateTime.Now.ToShortTimeString().Replace(":", ".").Replace(" ", "")+ssId+".png";
		print("Taking screenshot: " + _name);
		Application.CaptureScreenshot(_name, 2);
	}
}
