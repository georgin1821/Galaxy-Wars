using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : Singleton<ShipManager>
{
	public Ship[] shipsDetails;
	protected override void Awake()
	{
		base.Awake();
	}

	[System.Serializable]
	public class ShipObject
	{
		//squad, unlocked ships, power,prefab, sprite
	}
}
[System.Serializable]
public class Ship
{
	public ShipName name;
	public Sprite shipSprite;
	public GameObject shipPrefab;

	public int startingPower;

	[HideInInspector] public int currentPower;
	[HideInInspector] public int rank = 1;
}
