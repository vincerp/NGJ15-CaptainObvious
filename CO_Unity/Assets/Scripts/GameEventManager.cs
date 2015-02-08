using UnityEngine;
using System.Collections;

public class GameEventManager : MonoBehaviour 
{
	public GameObject heroComicsEffectPrefab;

	public GameObject street1Light;
	public GameObject street2Light;
	public GameObject trigger1ToDeactivate;
	public GameObject trigger2ToActivate;

	public void EndLevelSequence()
	{
//		Invoke ("CallBatman", 3f);
//		Invoke ("LoadNextLevel", 8f);
		Invoke ("LoadWithFade", 3f);
	}

	public void CallBatman()
	{
		Instantiate(heroComicsEffectPrefab, Camera.main.ScreenToWorldPoint( new Vector3( Screen.width / 2f, Screen.height /2f,
		                                                                                -Camera.main.transform.position.z)), Quaternion.identity );
	}

	public void LoadNextLevel()
	{
		Application.LoadLevel("Level2_FireLevel");
	}

	public void LoadWithFade()
	{
		//LevelLoader.LoadWithFade("Level2_FireLevel");
	}

	public void CheckIfBothStreetLightsAreGreen()
	{
		if( street1Light.activeInHierarchy && street2Light.activeInHierarchy )
		{
			trigger1ToDeactivate.SetActive(false);
			trigger2ToActivate.SetActive(true);
		}
	}
}
