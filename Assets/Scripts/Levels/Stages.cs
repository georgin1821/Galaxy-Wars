using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stages : Singleton<Stages>
{

	public  List<StageConfiguration> stages;
	public List<StageConfiguration> stagesEasy;
	public List<StageConfiguration> stagesmedium;
	public List<StageConfiguration> stagesHard;
	protected int stagesCount;
	public int StagesCount { get { return stagesCount; } }

	protected override void Awake()
	{
		base.Awake();
		//Setstages();
	}
	public void Setstages()
	{
		stagesEasy.Clear();
		StageConfiguration stage0 = new StageConfiguration
		{
			rewCoins = 1,
			rewGems = 1,
			rewShipCards = 1
		};
		StageConfiguration stage1 = new StageConfiguration
		{
			rewCoins = 1,
			rewGems = 1,
			rewShipCards = 1
		};
		StageConfiguration stage2 = new StageConfiguration
		{
			rewCoins = 1,
			rewGems = 1,
			rewShipCards = 1
		};
		StageConfiguration stage3 = new StageConfiguration
		{
			rewCoins = 1,
			rewGems = 1,
			rewShipCards = 1
		};
		StageConfiguration stage4 = new StageConfiguration
		{
			rewCoins = 1,
			rewGems = 1,
			rewShipCards = 1
		};

		stages.Insert(0, stage0);
		stages.Insert(1, stage1);
		stages.Insert(2, stage2);
		stages.Insert(3, stage3);

		stagesEasy = new List<StageConfiguration>();
		stagesmedium = new List<StageConfiguration>();
		stagesHard = new List<StageConfiguration>();

		for (int i = 0; i < stages.Count; i++)
		{
			StageConfiguration temp = new StageConfiguration
			{
				rewGems = stages[i].rewGems * 1,
				rewCoins = stages[i].rewCoins * 1,
				rewShipCards = stages[i].rewShipCards * 1
			};
			stagesEasy.Add(temp);
		}
		for (int i = 0; i < stages.Count; i++)
		{
			StageConfiguration temp = new StageConfiguration
			{
				rewGems = stages[i].rewGems * 2,
				rewCoins = stages[i].rewCoins * 2,
				rewShipCards = stages[i].rewShipCards * 2
			}; stagesmedium.Add(temp);

		}
		for (int i = 0; i < stages.Count; i++)
		{
			StageConfiguration temp = new StageConfiguration
			{
				rewGems = stages[i].rewGems * 3,
				rewCoins = stages[i].rewCoins * 3,
				rewShipCards = stages[i].rewShipCards * 3
			};
			stagesHard.Add(temp);
		}
		stagesCount = stages.Count;
	}

}

[System.Serializable]
public class StageConfiguration
{
	public int rewGems;
	public int rewCoins;
	public int rewShipCards;

}
