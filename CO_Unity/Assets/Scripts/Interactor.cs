using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Interactor : MonoBehaviour 
{
	static private List<InteractiveObject> _pickedUpObject = new List<InteractiveObject>();
	static public List<InteractiveObject> CurrentPickedObject 
	{
		get{ return _pickedUpObject; }
	}
	
//	private DistanceJoint2D _handJoint = null;
	private Transform _handTransform = null;

	void Awake()
	{
//		_handJoint = GetComponentInChildren<DistanceJoint2D>();
		_handTransform = transform.FindChild("Hand");
	}

	void Update () {
		if(Input.GetButtonUp("Interact")){
			foreach( InteractiveObject obj in InteractiveObject.Current )
				obj.Interacted();
		}

		if( Input.GetButtonUp("Punch"))
		{
			foreach( InteractiveObject obj in InteractiveObject.Current )
				obj.Punched();
		}

		if( Input.GetButtonUp("PickUp"))
		{
			foreach( InteractiveObject obj in InteractiveObject.Current )
				obj.Picked();
		}
	}

	public void Pickup( InteractiveObject pickUp )
	{
		if( _pickedUpObject.Contains( pickUp ) )
		{
			Drop(pickUp);
		}
		else
		{
			_pickedUpObject.Add( pickUp );
//			_handJoint.connectedBody = pickUp.GetComponent<Rigidbody2D>();

			DistanceJoint2D newJoint = _handTransform.gameObject.AddComponent<DistanceJoint2D>();
			newJoint.connectedBody = pickUp.GetComponent<Rigidbody2D>();
		}
	}

	public void Drop( InteractiveObject pickUp )
	{
		_pickedUpObject.Remove( pickUp );
//		_handJoint.connectedBody = null;
		DistanceJoint2D jointToRemove = null;

		DistanceJoint2D[] distanceJoints = _handTransform.GetComponents<DistanceJoint2D>();
		foreach( DistanceJoint2D joint in distanceJoints )
		{
			if( joint.connectedBody == pickUp.GetComponent<Rigidbody2D>() )
			{
				jointToRemove = joint;
				break;
			}
		}

		Destroy( jointToRemove );
	}

	public void MoveTo(GameObject to){
		iTween.CameraFadeAdd();
		iTween.CameraFadeTo(1f, 0.3f);
		StartCoroutine(MovePostFade(to.transform.position));
	}

	IEnumerator MovePostFade(Vector3 to){
		print(to);
		yield return new WaitForSeconds(0.3f);
		transform.position = to;
		iTween.CameraFadeTo(0f, 0.3f);
	}
}

