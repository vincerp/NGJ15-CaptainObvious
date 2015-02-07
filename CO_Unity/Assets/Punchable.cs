using UnityEngine;
using UnityEngine.Events;

public class Punchable : MonoBehaviour {

	[SerializeField]UnityEvent onPunched;

	void OnPunched () {
		onPunched.Invoke();
	}
}
