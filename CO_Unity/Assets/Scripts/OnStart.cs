using UnityEngine;
using UnityEngine.Events;

public class OnStart : MonoBehaviour {

	[SerializeField]UnityEvent onStart;
	void Start () {
		onStart.Invoke();
	}
}
