using UnityEngine;
using UnityEditor;
using System.Collections;

public class AudioAssetsUtils {
	
	[MenuItem("Assets/Audio/Set Audio Files as 2D", true)]
	[MenuItem("Assets/Audio/Set Audio Files as 3D", true)]
	public static bool HasAudiosSelected(){
		Object[] audios = Selection.GetFiltered(typeof(AudioClip), SelectionMode.Assets);
		return audios.Length > 0;
	}

	[MenuItem("Assets/Audio/Set Audio Files as 2D", false, 1)]
	public static void SetAudioFilesAs2D(){
		EditorApplication.Beep();
		Object[] audios = Selection.GetFiltered(typeof(AudioClip), SelectionMode.Assets);

		int amount = 0;
		foreach(AudioClip a in audios){
			string path = AssetDatabase.GetAssetPath(a);
			AudioImporter ai = AudioImporter.GetAtPath(path) as AudioImporter;

			if(ai.threeD){
				ai.threeD = false;
				amount++;
			}
			AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceSynchronousImport);
		}

		Debug.Log(""+amount+" AudioClips converted to 2D.");
	}
	
	[MenuItem("Assets/Audio/Set Audio Files as 3D", false, 1)]
	public static void SetAudioFilesAs3D(){
		EditorApplication.Beep();
		Object[] audios = Selection.GetFiltered(typeof(AudioClip), SelectionMode.Assets);
		
		int amount = 0;
		foreach(AudioClip a in audios){
			string path = AssetDatabase.GetAssetPath(a);
			AudioImporter ai = AudioImporter.GetAtPath(path) as AudioImporter;
			
			if(!ai.threeD){
				ai.threeD = true;
				amount++;
			}
			AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceSynchronousImport);
		}
		
		Debug.Log(""+amount+" AudioClips converted to 3D.");
	}

	public static void ChangeAudio3DSettings(AudioClip a, bool threeD){
		string path = AssetDatabase.GetAssetPath(a);
		AudioImporter ai = AudioImporter.GetAtPath(path) as AudioImporter;
		ai.threeD = threeD;
		AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceSynchronousImport);
		Debug.Log("AudioClip \"" + path + "\" 3D is now " + ai.threeD);
	}
}
