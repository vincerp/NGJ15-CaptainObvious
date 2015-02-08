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

	static private InteractiveObject _pushedObject = null;
	static public InteractiveObject PushedObject
	{
		get{ return _pushedObject; }
	}

	private Transform _handTransform = null;

	[Header("Messages")]
	int timesPicked = 0, timesDropped = 0;
	[SerializeField]List<CountedMessage> pickUpMessages;
	[SerializeField]List<CountedMessage> dropMessages;
	[SerializeField]AudioClip pickupSound, dropSound;
	
	Character playerCharacter;

	void Awake()
	{
		_handTransform = transform.FindChild("Hand");
		playerCharacter = GetComponent<Character>();
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
			if( _pickedUpObject.Count > 0 )
			{
				audio.PlayOneShot(dropSound);
				timesDropped++;
				foreach(var m in dropMessages){
					if(timesPicked == m.count) playerCharacter.Speak(m.message);
				}

				foreach( InteractiveObject obj in _pickedUpObject )
					obj.Dropped();

				Drop();
				audio.PlayOneShot(dropSound);
			}
			else
			{
				if(InteractiveObject.Current.Count>0){
					audio.PlayOneShot(pickupSound);
					timesPicked++;
					foreach(var m in pickUpMessages){
						if(timesPicked == m.count) playerCharacter.Speak(m.message);
					}

					foreach( InteractiveObject obj in InteractiveObject.Current )
						obj.Picked();

					foreach( InteractiveObject obj in InteractiveObject.Current )
						if( obj.onPicked.GetPersistentEventCount() > 0 )
							Pickup( obj );
				}
			}
		}

		if( Input.GetButtonUp("Push"))
		{
			foreach( InteractiveObject obj in InteractiveObject.Current )
				if( obj.onPunched.GetPersistentEventCount() > 0 )
					obj.Pushed();
		}
	}

	private void Pickup( InteractiveObject pickUp )
	{
		if( !_pickedUpObject.Contains( pickUp ) )
		{
			_pickedUpObject.Add( pickUp );

			DistanceJoint2D newJoint = _handTransform.gameObject.AddComponent<DistanceJoint2D>();
			newJoint.connectedBody = pickUp.GetComponent<Rigidbody2D>();
			newJoint.distance = 0.01f;
			newJoint.maxDistanceOnly = true;
		}
	}

	private void Drop(  )
	{
		foreach( InteractiveObject pickUp in _pickedUpObject )
		{
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

		_pickedUpObject.Clear();
	}

	public void StartPushing( InteractiveObject pickUp )
	{
		if( _pushedObject == pickUp )
		{
			StopPushing();
		}
		else
		{
			pickUp.transform.parent.SetParent( transform );

			pickUp.transform.GetComponentInParent<Rigidbody2D>().isKinematic = true;

			foreach( Collider2D col in pickUp.transform.parent.GetComponentsInChildren<Collider2D>() )
				col.enabled = false;

			_pushedObject = pickUp;
		}
	}

	public void StopPushing()
	{
		_pushedObject.transform.GetComponentInParent<Rigidbody2D>().isKinematic = false;

		foreach( Collider2D col in _pushedObject.transform.parent.GetComponentsInChildren<Collider2D>() )
			col.enabled = true;

		_pushedObject.transform.parent.SetParent( null );
		_pushedObject = null;
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

