using UnityEngine;
using System.Collections;

public class Phonebooth : MonoBehaviour {

    public string fireDeptText;

	[SerializeField]float waitForBubble = 8.1f;
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
        yield return new WaitForSeconds(waitForBubble);
        GetComponent<Character>().Speak(fireDeptText);
    }
}
