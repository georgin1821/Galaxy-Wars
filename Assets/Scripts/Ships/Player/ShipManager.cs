using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : Singleton<ShipManager>
{
	public List<Ship> shipsDB = new List<Ship>();//do not change name!!!
	protected override void Awake()
	{
		base.Awake();
		InitializeShips();
	}
	private void InitializeShips()
	{
		foreach (var ship in shipsDB)
		{
			ship.rank = 1;
			ship.power = ship.startingPower;
		}
	}

	[System.Serializable]
	public class ShipObject
	{
		//public Ship ship;//squad, unlocked ships, power,prefab, sprite
	}
}
[System.Serializable]
public class Ship
{
	public ShipName name;
	public Sprite sprite;
	public GameObject prefab;
	public int startingPower;

	[HideInInspector] public int power;
	[HideInInspector] public int rank = 1;
	[HideInInspector] public bool isUnloked = false;
}
