using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class UnityEventInterface : MonoBehaviour {

	[SerializeField]UnityEvent unityEvent;

	public void InvokeEvents(){
		unityEvent.Invoke();
	}
}
