using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour 
{
	[SerializeField]private float speechTime = 2f;
	[SerializeField]private float repeatPeriod = 5f;


	private bool _isDead = false;
	private SpeechBubble _sBubble = null;

	
	private Animator _animator;

	[SerializeField]AudioClip voiceLoop;
	AudioSource mySource;


	public void Speak( string speech )
	{
		mySource.volume = 1f;
		_sBubble.Activate( speech );
		_sBubble.Invoke( "Deactivate", speechTime );

		Subtitles.Instance.DisplaySubtitles( speech );
		StartCoroutine(StopVoice());
	}

	IEnumerator StopVoice(){
		yield return new WaitForSeconds(speechTime*0.5f);
		//mySource.volume = 0f;
		iTween.AudioTo(mySource.gameObject, 0f, 1f, 0.2f);
	}

	public void Die()
	{
		_isDead = true;
		_animator.SetTrigger("event_Died");
	}

	void Awake()
	{
		_animator = GetComponent<Animator>();
		_sBubble = GetComponentInChildren<SpeechBubble>();

		if( _sBubble == null )
		{
			GameObject speechPrefab = Resources.Load("Characters/SpeechBubble", typeof( GameObject) ) as GameObject;
			GameObject speechGO = GameObject.Instantiate(speechPrefab) as GameObject;
			speechGO.transform.SetParent( transform );
			speechGO.transform.localPosition = speechPrefab.transform.localPosition;

			_sBubble = speechGO.GetComponent<SpeechBubble>();
			_sBubble.Deactivate();
		}

		mySource = new GameObject("voice", typeof(AudioSource)).audio;
		mySource.transform.parent = transform;
		mySource.loop = true;
		mySource.clip = voiceLoop;
		mySource.Play();
		mySource.volume = 0f;
	}

	/*IEnumerator Start()
	{
		yield return StartCoroutine( "RepeatSpeaking", "Help!");
	}*/

	void Update()
	{

	}

	IEnumerator RepeatSpeaking(string text)
	{
		while( true )
		{
			yield return new WaitForSeconds(repeatPeriod);
			Speak( text );
		}
	}
}
