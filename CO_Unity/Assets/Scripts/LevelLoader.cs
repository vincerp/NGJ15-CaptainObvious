using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {

	[SerializeField]string levelName;

	void Load () {
		Application.LoadLevel(levelName);
	}
}
