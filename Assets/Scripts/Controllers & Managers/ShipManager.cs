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
        public ShipName shipName;
        public Sprite shipSprite;
        public GameObject shipPrefab;

        public int shipPower;
    }
    [System.Serializable]
    public class Ship
    {
        public ShipObject ships;
    }
}
