using UnityEngine;
using System.Collections;

public class SkipVideo : MonoBehaviour {

	[SerializeField]UnityEngine.Events.UnityEvent onPress;
	[SerializeField]float waitForSkip;
	bool isLoading = false;

	void Update () {
		if(isLoading || Time.timeSinceLevelLoad < waitForSkip) return;

		if(Input.GetButtonUp("Punch") || Input.GetButtonUp("Interact") || Input.GetButtonUp("Jump") || Input.GetButtonUp("PickUp")){
			onPress.Invoke();
			isLoading = true;
		}
	}
}
