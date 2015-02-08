using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {

	[SerializeField]string levelName;

	public void Load () {
		Application.LoadLevel(levelName);
	}
}
