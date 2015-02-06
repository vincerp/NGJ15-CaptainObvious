using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class AssetUtils : MonoBehaviour {
	
	public static Object[] GetAssetsOfType(System.Type type, string fileExtension)
	{
		List<Object> tempObjects = new List<Object>();
		DirectoryInfo directory = new DirectoryInfo(Application.dataPath);
		FileInfo[] goFileInfo = directory.GetFiles("*" + fileExtension, SearchOption.AllDirectories);
		
		int i = 0; int goFileInfoLength = goFileInfo.Length;
		FileInfo tempGoFileInfo; string tempFilePath;
		Object tempGO;
		for (; i < goFileInfoLength; i++)
		{
			tempGoFileInfo = goFileInfo[i];
			if (tempGoFileInfo == null)
				continue;
			
			tempFilePath = tempGoFileInfo.FullName;
			tempFilePath = tempFilePath.Replace(@"\", "/").Replace(Application.dataPath, "Assets");
			
			//Debug.Log(tempFilePath + "\n" + Application.dataPath);
			
			tempGO = AssetDatabase.LoadAssetAtPath(tempFilePath, typeof(Object)) as Object;
			if (tempGO == null)
			{
				//Debug.LogWarning("Skipping Null");
				continue;
			}
			else if (tempGO.GetType() != type)
			{
				//Debug.LogWarning("Skipping " + tempGO.GetType().ToString());
				continue;
			}
			
			tempObjects.Add(tempGO);
		}
		
		return tempObjects.ToArray();
	}
	
	public static List<T> GetAssetsOfType<T>(string fileExtension) where T:Object
	{
		List<T> tempObjects = new List<T>();
		DirectoryInfo directory = new DirectoryInfo(Application.dataPath);
		FileInfo[] goFileInfo = directory.GetFiles("*" + fileExtension, SearchOption.AllDirectories);
		
		int i = 0; int goFileInfoLength = goFileInfo.Length;
		FileInfo tempGoFileInfo; string tempFilePath;
		Object tempGO;
		for (; i < goFileInfoLength; i++)
		{
			tempGoFileInfo = goFileInfo[i];
			if (tempGoFileInfo == null)
				continue;
			
			tempFilePath = tempGoFileInfo.FullName;
			tempFilePath = tempFilePath.Replace(@"\", "/").Replace(Application.dataPath, "Assets");
			
			//Debug.Log(tempFilePath + "\n" + Application.dataPath);
			
			tempGO = AssetDatabase.LoadAssetAtPath(tempFilePath, typeof(Object)) as Object;
			if (tempGO == null)
			{
				//Debug.LogWarning("Skipping Null");
				continue;
			}
			else if (tempGO.GetType() != typeof(T))
			{
				//Debug.LogWarning("Skipping " + tempGO.GetType().ToString());
				continue;
			}
			
			tempObjects.Add((T)tempGO);
		}
		
		return tempObjects;
	}
}
