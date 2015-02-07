using UnityEngine;
using UnityEngine.Events;

public class Trigger2dAction : MonoBehaviour {
	
	[SerializeField]bool onlyOnce = false;
	bool triggered = false;

	[SerializeField]UnityEvent onTriggerEnter;

	void OnTriggerEnter2D(Collider2D col){
		if(col.tag == "Player"){
			if(onlyOnce && triggered) return;
			onTriggerEnter.Invoke();
			if(onlyOnce)triggered = true;
		}
	}
}
