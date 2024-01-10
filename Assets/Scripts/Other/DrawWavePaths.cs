using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWavePaths : MonoBehaviour
{
	public Transform[] points;

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		if (points != null)
		{
			foreach (var point in points)
			{
				if (point != null)
				{
					Gizmos.DrawSphere(point.position, 0.15f);
				}
			}

			DrawPaths();
		}
	}



	void DrawPaths()
	{
		for (int i = 0; i < points.Length - 1; i++)
		{
			if (points[i] != null && points[i + 1] != null)
			{
				Vector3 thisPoint = points[i].position;
				Vector3 nextPoint = points[i + 1].position;
				Gizmos.DrawLine(thisPoint, nextPoint);
			}
		}
	}
}
