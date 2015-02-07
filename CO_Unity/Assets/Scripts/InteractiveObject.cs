using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class InteractiveObject : MonoBehaviour {

	static InteractiveObject current;

	[SerializeField]Transform interactionSign;
	Vector3 signScale;

	[SerializeField]UnityEvent onInteract;

	void Start(){
		signScale = interactionSign.localScale;
		interactionSign.localScale = Vector3.zero;
	}

	protected void OnTriggerEnter2D(Collider2D col){
		if(col.tag == "Player"){
			//show interaction sign
			Hashtable args = new Hashtable(){
				{"scale", Vector3.one},
				{"time", 0.3f},
				{"easetype", "easeOutBack"}
			};

			iTween.ScaleTo(interactionSign.gameObject, args);
		}

		current = this;
	}

	void OnTriggerExit2D(Collider2D col){
		if(col.tag == "Player"){
			//hide interaction sign
			Hashtable args = new Hashtable(){
				{"scale", Vector3.zero},
				{"time", 0.3f},
				{"easetype", "easeInBack"}
			};
			
			iTween.ScaleTo(interactionSign.gameObject, args);
		}

		if(current == this) current = null;
	}

	public static bool TryInteract(){
		if(current){
			current.onInteract.Invoke();
			return true;
		}
		return false;
	}
}
