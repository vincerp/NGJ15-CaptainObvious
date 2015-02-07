using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Subtitles : MonoBehaviour {

	[SerializeField]float displayTime = 3f;

	Text txt;
	void Start () {
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
