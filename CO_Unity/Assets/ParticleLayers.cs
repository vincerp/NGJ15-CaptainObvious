using UnityEngine;
using System.Collections;

public class ParticleLayers : MonoBehaviour {
	
	[SerializeField]string sortingLayer;
	[SerializeField]int sortingOrder;

	void Start () {
		particleSystem.renderer.sortingLayerName = sortingLayer;
		particleSystem.renderer.sortingOrder = sortingOrder;
	}
}
