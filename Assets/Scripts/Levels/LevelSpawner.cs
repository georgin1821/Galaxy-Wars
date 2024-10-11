using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : SimpleSingleton<LevelSpawner>
{
	[SerializeField] List<LevelConfig> levels;
	public List<LevelConfig> Levels { get { return levels; } }

	public int LevelIndex { get; private set; } // what is this

	// set the list of all waves at the current level(Waves)
	// calls the method who instatiate all the waves on the WaveSpawner script
	public void SpawnLevelWithIndex(int index)
	{
		if (index < levels.Count)
		{
			WaveSpawner.Instance.SetWaves(levels[index].Waves);
			WaveSpawner.Instance.SpawnTheLevel();
		}
	}
}
