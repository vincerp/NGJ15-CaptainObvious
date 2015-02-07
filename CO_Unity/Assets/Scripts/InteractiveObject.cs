using UnityEngine;
using UnityEngine.Events;
using System.Collections;

interface Interactable
{
	void Picked();
	void Dropped();
	void Punched();
	void Interacted();
	void Pushed();
}

public class InteractiveObject : MonoBehaviour, Interactable {

	static InteractiveObject current = null;
	static public InteractiveObject Current
	{
		get{ return current; }
	}

	[SerializeField]UnityEvent onPicked;
	[SerializeField]UnityEvent onDropped;
	[SerializeField]UnityEvent onPunched;
	[SerializeField]UnityEvent onInteracted;
	[SerializeField]UnityEvent onPushed;


	[SerializeField]Transform interactionSign;
	Vector3 signScale;

	void Start(){
		signScale = interactionSign.localScale;
		interactionSign.localScale = Vector3.zero;
	}

	protected void OnTriggerEnter2D(Collider2D col){
		if(col.tag == "Player"){
			//show interaction sign
			Hashtable args = new Hashtable(){
				{"scale", signScale},
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

	public static bool isObjectToInteract(){
		return current != null;
	}

	virtual public void Picked(){ onPicked.Invoke(); }
	virtual public void Dropped(){ onDropped.Invoke(); }
	virtual public void Punched(){ onPushed.Invoke(); }
	virtual public void Interacted(){ onInteracted.Invoke(); }
	virtual public void Pushed(){ onPushed.Invoke(); }
}
