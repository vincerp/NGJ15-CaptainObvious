using UnityEngine;
using System.Collections;

public class SpeechBubble : MonoBehaviour 
{
	private SpriteRenderer _speechBubbleSprite = null;
	private TextMesh _speechTextRenderer = null;

	public void Activate(string text = null)
	{
		if( !string.IsNullOrEmpty(text) )
			_speechTextRenderer.text = text;

		_speechBubbleSprite.enabled = true;
	}

	public void Deactivate()
	{
		_speechBubbleSprite.enabled = false;
	}
}
