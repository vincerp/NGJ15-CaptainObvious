using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public static class ScriptableObjectUtility
{
	/// <summary>
	//	This makes it easy to create, name and place unique new ScriptableObject asset files.
	/// </summary>
	public static T CreateAsset<T> (string newName = "", string usePath = "", bool selectAfterCreating = true) where T : ScriptableObject
	{
		T asset = ScriptableObject.CreateInstance<T> ();
		
		string path = AssetDatabase.GetAssetPath (Selection.activeObject);
		if (path == "") 
		{
			path = "Assets";
		} 
		else if (Path.GetExtension (path) != "") 
		{
			path = path.Replace (Path.GetFileName (AssetDatabase.GetAssetPath (Selection.activeObject)), "");
		}

		string n = ((newName != "")?newName:"New " + typeof(T).ToString());
		string p = (usePath!="")?usePath:path;
		
		string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath (p + "/" + n + ".asset");
		
		AssetDatabase.CreateAsset (asset, assetPathAndName);
		
		AssetDatabase.SaveAssets ();
		EditorUtility.FocusProjectWindow ();
		if(selectAfterCreating)Selection.activeObject = asset;

		return asset;
	}
}