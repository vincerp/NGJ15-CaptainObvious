using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour 
{
	public float speechTime = 1f;


	private bool _isDead = false;
	private SpeechBubble _sBubble = null;

	
	private Animator _animator;
	

	public void Speak( string speech )
	{
		_sBubble.Activate( speech );
		_sBubble.Invoke( "Deactivate", speechTime );
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
			GameObject speechGO = GameObject.Instantiate(Resources.Load("Characters/SpeechBubble")) as GameObject;
			_sBubble = speechGO.GetComponent<SpeechBubble>();
		}
	}

	void Start()
	{

	}

	void Update()
	{

	}

}
