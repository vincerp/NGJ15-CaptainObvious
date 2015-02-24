using UnityEngine;
using System.Collections;

public class SetSpriteColor : MonoBehaviour {

	SpriteRenderer _s;
	[SerializeField]Color toColor;


	void Start () {
		_s = GetComponent<SpriteRenderer>();
	}


	public void SetColor () {
		_s.color = toColor;
	}
}
