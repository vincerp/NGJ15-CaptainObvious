using UnityEngine;
using System.Collections.Generic;

public class Puncher : MonoBehaviour {

	List<Collider2D> current = new List<Collider2D>();

	[SerializeField]float radius = 1f;

	int timesPunched = 0;
	[SerializeField]List<CountedMessage> messages = new List<CountedMessage>();

	Character playerCharacter;

	void Start(){
		playerCharacter = GetComponentInParent<Character>();
	}

	void Update () {
		if(Input.GetButtonDown("Punch")){
			//TODO: Play animation
			//TODO: Play swoosh sound
			var allStuff = Physics2D.OverlapCircleAll(transform.position, radius);
			foreach(var c in allStuff){
				c.SendMessage("OnPunched", SendMessageOptions.DontRequireReceiver);
			}
			timesPunched++;
			foreach(var m in messages){
				if(timesPunched == m.count) playerCharacter.Speak(m.message);
			}
		}
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