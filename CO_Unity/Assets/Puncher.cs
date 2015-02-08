using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Puncher : MonoBehaviour {

	List<Collider2D> current = new List<Collider2D>();

	[SerializeField]float radius = 1f;
	
	int timesPunched = 0;
	int timesNotPunched = 0;
	[SerializeField]List<CountedMessage> messages = new List<CountedMessage>();
	[SerializeField]List<CountedMessage> messagesNotPunched = new List<CountedMessage>();

	Character playerCharacter;
	
	[SerializeField]AudioClip punchAudio, punchHit;
	[SerializeField]float punchToHitTime = 0.3f;
	AudioSource audioSource;

	void Start(){
		playerCharacter = GetComponentInParent<Character>();
		audioSource = GetComponentInParent<AudioSource>();
	}

	void Update () {
		if(Input.GetButtonDown("Punch")){
			if(Mathf.Abs(Input.GetAxis("Move")) < 0.05f){
				//TODO: Play animation
				//TODO: Play swoosh sound
				var allStuff = Physics2D.OverlapCircleAll(transform.position, radius);
				foreach(var c in allStuff){
					c.SendMessage("OnPunched", SendMessageOptions.DontRequireReceiver);
				}
				if(allStuff.Any(x => x.GetComponent<Punchable>() != null)) StartCoroutine(PunchHitAudio());
				audioSource.PlayOneShot(punchAudio);
				timesPunched++;
				foreach(var m in messages){
					if(timesPunched == m.count) playerCharacter.Speak(m.message);
				}
			} else {
				timesNotPunched++;
				foreach(var m in messagesNotPunched){
					if(timesNotPunched == m.count) playerCharacter.Speak(m.message);
				}
			}
		}
	}

	IEnumerator PunchHitAudio(){
		yield return new WaitForSeconds(punchToHitTime);
		audioSource.PlayOneShot(punchHit);
        audioSource.volume = 0.5f;
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(transform.position, radius);
	}
}

[System.Serializable]
public class CountedMessage{
	public int count;
	public string message;

	public CountedMessage(int count, string message){
		this.count = count;
		this.message = message;
	}


}