using UnityEngine;
using System.Collections;

public class Interactor : MonoBehaviour {

	void Update () {
		if(Input.GetButton("Interact")){
			if(InteractiveObject.TryInteract()){
				//Interaction successful
				//TODO: Play character activation animation
			}//else = not possible to interact
		}
	}
}
