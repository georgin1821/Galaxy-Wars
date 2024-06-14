using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Wave Config")]

public class WaveConfig : ScriptableObject
{
	[SerializeField] private List<GameObject> divisions;

	[SerializeField] private bool isBoss;
	[SerializeField] public bool IsBoss { get { return isBoss; } }


	public List<GameObject> GetDivisions()
	{
		return divisions;
	}

}
