using UnityEngine;
using System.Collections;

public class Interactor : MonoBehaviour 
{
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
}

