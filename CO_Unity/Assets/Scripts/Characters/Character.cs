using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour 
{
	[SerializeField]private float speechTime = 2f;
	[SerializeField]private float repeatPeriod = 5f;


	private bool _isDead = false;
	private SpeechBubble _sBubble = null;

	
	private Animator _animator;
	

	public void Speak( string speech )
	{
		_sBubble.Activate( speech );
		_sBubble.Invoke( "Deactivate", speechTime );

		Subtitles.Instance.DisplaySubtitles( speech );
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
