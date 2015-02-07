using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	public GameObject heroComicsEffectPrefab;

	public void EndLevelSequence()
	{
		Instantiate(heroComicsEffectPrefab, Camera.main.ScreenToWorldPoint( new Vector3( Screen.width / 2f, Screen.height /2f,
		                                                                              -Camera.main.transform.position.z)), Quaternion.identity );
	}
}
