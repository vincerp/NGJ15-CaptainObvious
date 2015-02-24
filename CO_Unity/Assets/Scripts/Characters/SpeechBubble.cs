using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpeechBubble : MonoBehaviour 
{
	private Text _speechTextRenderer = null;
	private Image _speechBubbleRenderer = null;

	private Vector3 originalScale;
	private Vector3 currentScale;
	private float originalLettersCount;

	[SerializeField]float someNumber = 0.9f;

	void Awake()
	{
		_speechTextRenderer = GetComponentInChildren<Text>();
		_speechBubbleRenderer = transform.FindChild("Bubble").GetComponent<Image>();

		originalScale = _speechBubbleRenderer.transform.localScale;
		originalLettersCount = _speechTextRenderer.text.Length + 1;

		currentScale = _speechTextRenderer.transform.localScale;
	}

	public void Activate(string text = null)
	{
		if( !string.IsNullOrEmpty(text) )
		{
			_speechTextRenderer.text = text;

			_speechBubbleRenderer.transform.localScale = new Vector3(originalScale.x * ((float) text.Length / (float) originalLettersCount) * someNumber, 
			                                                        originalScale.y, originalScale.z);

		}

		gameObject.SetActive(true);
	}

	public void Deactivate()
	{
		gameObject.SetActive(false);
	}

	void Update()
	{
		_speechTextRenderer.transform.localScale = new Vector3(currentScale.x * 
		                                                       Mathf.Sign(transform.parent.localScale.x), 
		                                                       currentScale.y, currentScale.z);
	}
}
