using UnityEngine;
using UnityEngine.Events;

public class Trigger2dAction : MonoBehaviour {

	[SerializeField]string triggerTag = "Player";
	[SerializeField]bool onlyOnce = false;
	bool triggered = false;

	[SerializeField]UnityEvent onTriggerEnter;

	void OnTriggerEnter2D(Collider2D col){
		if(col.tag == triggerTag){
			if(onlyOnce && triggered) return;
			onTriggerEnter.Invoke();
			if(onlyOnce)triggered = true;
		}
	}
	
	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag == triggerTag){
			if(onlyOnce && triggered) return;
			onTriggerEnter.Invoke();
			if(onlyOnce)triggered = true;
		}
	}
}
