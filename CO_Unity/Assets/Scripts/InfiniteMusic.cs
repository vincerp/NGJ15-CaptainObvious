using UnityEngine;
using System.Collections;
using System.Linq;

[RequireComponent(typeof(AudioSource))]
public class InfiniteMusic : MonoBehaviour {

	[Range(0f, 1f)][SerializeField]float cinematicMusic, gameMusic;
	[SerializeField]int[] levelsWithCinematicMusic;
	bool isCinematic = true;


	void Start () {
		DontDestroyOnLoad(gameObject);
	}

	void OnLevelWasLoaded(int lvl){
		print (lvl);
		bool tryCinematic = levelsWithCinematicMusic.Contains(lvl);

		print(""+tryCinematic+isCinematic);
		if(tryCinematic != isCinematic)
			iTween.AudioTo(gameObject, ((tryCinematic)?cinematicMusic:gameMusic), 1f, 0.25f);

		isCinematic = tryCinematic;
	}
}
