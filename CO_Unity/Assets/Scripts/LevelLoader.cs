﻿using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {

	[SerializeField]string levelName;

	public void Load () {
		Application.LoadLevel(levelName);
	}

	public void LoadWithFade () {
		StartCoroutine(lwf ());
	}

	IEnumerator lwf(){
		iTween.CameraFadeAdd();
		iTween.CameraFadeTo(1f, 0.5f);
		yield return new WaitForSeconds(0.5f);
		Application.LoadLevel(levelName);
	}
}
