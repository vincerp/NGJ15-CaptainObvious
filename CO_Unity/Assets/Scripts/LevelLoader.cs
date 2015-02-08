using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {

	[SerializeField]string levelName;
	private static LevelLoader _instance;
	[SerializeField]float prewait = 0f;
	void Awake()
	{
		_instance = this;
	}

	public void Load () {
		Application.LoadLevel(levelName);
	}

	public void LoadWithFade () {
		StartCoroutine(lwf ());
	}

	IEnumerator lwf(){
		yield return new WaitForSeconds(prewait);
		iTween.CameraFadeAdd();
		iTween.CameraFadeTo(1f, 0.5f);
		yield return new WaitForSeconds(0.5f);
		Application.LoadLevel(levelName);
	}
}
