using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : SimpleSingleton<WaveSpawner>
{

	public static event System.Action<int> OnEnemiesDieCount;

	private List<WaveConfig> waves;
	private List<GameObject> divisionInScene;
	private List<GameObject> divisions;

	private float delayBetweenWaves = 2f; //2 secs delay before wave appears

	#region CoRoutines
	IEnumerator SpawnLevel()
	{
		float delay = 1.5f;
		if (GameManager.Instance.isSpeedLevel) delay = 0;

		for (int waveIndex = 0; waveIndex < waves.Count; waveIndex++)
		{
			//show the info text (normal wave or boss) get divisons
			if (waves[waveIndex].IsBoss)
			{
				GameUIController.Instance.ShowWaveInfoText(waveIndex, waves.Count, "ARACHRON");
			}
			else GameUIController.Instance.ShowWaveInfoText(waveIndex, waves.Count);
			divisions = waves[waveIndex].GetDivisions();

			yield return new WaitForSeconds(delay);
			divisionInScene = new List<GameObject>();

			// spawn teh diviesions
			for (int i = 0; i < divisions.Count; i++)
			{
				//divisions objects on scene
				GameObject division = Instantiate(divisions[i]);
				divisionInScene.Add(division);
				yield return null;
			}
			yield return null;

			//waits player to shoot all enemies and then destroy divisions gameObects
			yield return StartCoroutine(NoEnemiesOnWave());
			DestroyWaves();
		}

		GamePlayController.Instance.UpdateState(GameState.LEVELCOMPLETE);
	}
	IEnumerator NoEnemiesOnWave()
	{
		int enemiesCount = EnemyCount.Instance.Count;

		while (EnemyCount.Instance.Count > 0)
		{
			yield return null;
		}
		//  OnEnemiesDieCount(enemiesCount);
		yield return new WaitForSeconds(1);
	}
	#endregion

	#region Public
	public void SetWaves(List<WaveConfig> _waves)
	{
		waves = _waves;
	}
	public void SpawnTheLevel()
	{
		StopAllCoroutines();
		StartCoroutine(SpawnLevel());
	}
	public void DestroyWaves()
	{
		if (divisionInScene != null)
		{
			foreach (var item in divisionInScene)
			{
				Destroy(item);
			}
		}
	}
	#endregion
}
