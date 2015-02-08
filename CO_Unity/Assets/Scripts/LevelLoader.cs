using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {

	[SerializeField]string levelName;

	public void Load (float time = 0) {
        StartCoroutine(DoLoad(time));
	}

    IEnumerator DoLoad(float time)
    {
        yield return new WaitForSeconds(time);
        Application.LoadLevel(levelName);
    }
}
