using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpeechBubble : MonoBehaviour 
{
//	private SpriteRenderer _speechBubbleSprite = null;
	private Text _speechTextRenderer = null;

	void Awake()
	{
		_speechTextRenderer = GetComponentInChildren<Text>();
	}

	public void Activate(string text = null)
	{
		if( !string.IsNullOrEmpty(text) )
			_speechTextRenderer.text = text;

//		_speechBubbleSprite.enabled = true;
		gameObject.SetActive(true);
	}

	public void Deactivate()
	{
//		_speechBubbleSprite.enabled = false;
		gameObject.SetActive(false);
	}
}
