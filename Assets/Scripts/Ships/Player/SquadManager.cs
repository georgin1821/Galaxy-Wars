using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadManager : Singleton<SquadManager>
{
	public List<Ship> squad;
	protected override void Awake()
	{
		base.Awake();
		//SetSquad();
	}


	public void SetSquad()
	{
		squad = new List<Ship>();
		squad.Add(GameDataManager.Instance.ships[0]);
		squad.Add(GameDataManager.Instance.ships[1]);
		squad.Add(GameDataManager.Instance.ships[2]);
		squad.Add(GameDataManager.Instance.ships[3]);
	}

}
