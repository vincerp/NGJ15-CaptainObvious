using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Subtitles : MonoBehaviour 
{
	static private Subtitles _instance;
	static public Subtitles Instance
	{
		get{ return _instance; }
	}

	[SerializeField]float displayTime = 2f;

	Text txt;

	void Awake()
	{
		_instance = this;
		txt = GetComponent<Text>();
	}

	public void DisplaySubtitles(string text){
		StopAllCoroutines();
		txt.text = text;
		StartCoroutine(WaitAndClear());
	}

	IEnumerator WaitAndClear(){
		yield return new WaitForSeconds(displayTime);
		txt.text = "";
	}
}
