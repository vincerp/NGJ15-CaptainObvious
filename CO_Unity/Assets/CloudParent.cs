using UnityEngine;
using System.Collections.Generic;

public class CloudParent : MonoBehaviour {

	[SerializeField]float moveSpeed;
	List<Transform> cloudPos = new List<Transform>();
	[SerializeField]float minX;
	[SerializeField]float addX;

	void Start () {
		foreach(Transform child in transform){
			cloudPos.Add(child);
		}
	}
	
	void Update () {
		foreach(var c in cloudPos){
			c.Translate(Vector3.right*moveSpeed);
			if(c.position.x < minX) c.Translate(Vector3.right*addX);
		}
	}
}
