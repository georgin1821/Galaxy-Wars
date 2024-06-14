using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowersController : SimpleSingleton<PowersController>
{

	[SerializeField] private Power[] powers;

	private int acumWeight;
	private int random;

	private void Start()
	{
		AccumulatedWeight();
	}


	//instatiate a random power at enemy pos
	//Runs when an enemy dies
	public void InstatiatePower(Transform posTrans)
	{
		GameObject powerPrefab = GetPowerFromWeights();
		if (powerPrefab != null) // if all powers have zero weight
		{
			Instantiate(GetPowerFromWeights(), posTrans.position, Quaternion.identity);
		}
	}

		public GameObject GetPowerFromWeights()
	{
		random = Random.Range(0, 1) * acumWeight;

		// Propability();
		foreach (var item in powers)
		{
			if (item.accumWeight > random)
			{
				return item.powerPrefab;
			}
		}
		return null;
	}

	private void AccumulatedWeight()
	{
		foreach (var item in powers)
		{
			acumWeight += item.weight;
			item.accumWeight = acumWeight;
		}
	}
}

[System.Serializable]
public class Power
{
	public GameObject powerPrefab;
	public int weight;
	[HideInInspector] public float accumWeight;
}