using UnityEngine;
using System.Collections.Generic;

public class Puncher : MonoBehaviour {

	List<Collider2D> current = new List<Collider2D>();

	[SerializeField]float radius = 1f;

	void Update () {
		if(Input.GetButton("Punch")){
			//TODO: Play animation
			//TODO: Play swoosh sound
			var allStuff = Physics2D.OverlapCircleAll(transform.position, radius);
			foreach(var c in allStuff){
				c.SendMessage("OnPunched", SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(transform.position, radius);
	}
}
