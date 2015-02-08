using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class InfiniteMusic : MonoBehaviour {

	void Start () {
		DontDestroyOnLoad(gameObject);
	}
}
