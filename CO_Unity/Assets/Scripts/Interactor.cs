using UnityEngine;
using System.Collections;

public class Interactor : MonoBehaviour 
{
	private InteractiveObject _pickedUpObject = null;
	private DistanceJoint2D _handJoint = null;

	void Awake()
	{
		_handJoint = GetComponentInChildren<DistanceJoint2D>();
	}

	void Update () {
		if(Input.GetButton("Interact")){
			if(InteractiveObject.isObjectToInteract()){
				InteractiveObject.Current.Interacted();
			}
		}

		if( Input.GetButton("Punch"))
		{
			if(InteractiveObject.isObjectToInteract())
			{
				InteractiveObject.Current.Punched();
			}
		}

		if( Input.GetButton("PickUp"))
		{
			if(InteractiveObject.isObjectToInteract())
			{
				InteractiveObject.Current.Picked();
			}
		}
	}

	public void Pickup( InteractiveObject pickUp )
	{
		if( _pickedUpObject != null && _pickedUpObject == pickUp )
		{
			Drop(_pickedUpObject);
		}
		else
		{
			_pickedUpObject = pickUp;
			_handJoint.connectedBody = pickUp.GetComponent<Rigidbody2D>();
		}
	}

	public void Drop( InteractiveObject pickUp )
	{
		_pickedUpObject = null;
		_handJoint.connectedBody = null;
	}
}

