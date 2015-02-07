using UnityEngine;
using System.Collections;

public class Holse : InteractiveObject {

	private LineRenderer _lineRenderer;

	void Awake()
	{
		_lineRenderer = transform.GetComponent<LineRenderer>();
		_lineRenderer.SetVertexCount(transform.parent.childCount);
	}

	void Update()
	{
		for (int i = 0; i < transform.parent.childCount; i++ )
			_lineRenderer.SetPosition( i, transform.parent.GetChild(i).position );

	}
}
