using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

interface Interactable
{
	void Picked();
	void Dropped();
	void Punched();
	void Interacted();
	void Pushed();
}

public class InteractiveObject : MonoBehaviour, Interactable {

	static List<InteractiveObject> currentItems = new List<InteractiveObject>();
	static public List<InteractiveObject> Current
	{
		get{ return currentItems; }
	}


	[SerializeField]Transform interactionSign;

	public List<InteractiveObject> objToContainToInteract = new List<InteractiveObject>();
	
	[SerializeField]UnityEvent onPicked;
	[SerializeField]UnityEvent onDropped;
	[SerializeField]UnityEvent onPunched;
	[SerializeField]UnityEvent onInteracted;
	[SerializeField]UnityEvent onPushed;

	private Vector3 signScale;

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

			if( !currentItems.Contains( this ) )
			{
				currentItems.Add( this );
				Debug.Log("currentItems added " + gameObject.name);
			}
		}
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

			if(currentItems.Contains( this) )
			{
				currentItems.Remove( this ) ;
				Debug.Log("currentItems removed " + gameObject.name);
			}
		}
	}

	public static bool isObjectToInteract(){
		return currentItems.Count > 0;
	}

	public bool isInteractionValidated( List<InteractiveObject> itemsInHands )
	{
		if( objToContainToInteract.Count == 0 )
			return true;

		bool everythingIsIncluded = true;
		foreach( InteractiveObject item in objToContainToInteract )
			if( !itemsInHands.Contains(item) )
				everythingIsIncluded = false;

		return everythingIsIncluded;
//		return objToContainToInteract.Contains( itemInHands );
	}

	virtual public void Picked(){ onPicked.Invoke(); }
	virtual public void Dropped(){ onDropped.Invoke(); }
	virtual public void Punched(){ onPushed.Invoke(); }
	virtual public void Interacted()
	{ 
		Debug.Log( "Interacted: " + gameObject.name);
		if( isInteractionValidated( Interactor.CurrentPickedObject ) )
			onInteracted.Invoke(); 
		else
			Debug.LogWarning("Put some negative sounds here");
	}
	virtual public void Pushed(){ onPushed.Invoke(); }
}
