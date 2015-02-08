using UnityEngine;
using System.Collections;

public class Phonebooth : MonoBehaviour {

    public string fireDeptText;
	// Use this for initialization
	void Start () {
	
	}

    public void Call()
    {
        if (audio.isPlaying == false)
        {
            StartCoroutine(DoCall());


        }
    }

    IEnumerator DoCall()
    {
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
        GetComponent<Character>().Speak(fireDeptText);
    }
}
