using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivisionSpawn : MonoBehaviour
{
	public static event Action OnLastEnemyAtFormation;

	[HideInInspector] public List<Transform> waypoints;
	[HideInInspector] public List<Transform> formationWaypoints;

	[SerializeField] DivisionStates divSet;
	[SerializeField] float delayToSpawn;
	[SerializeField] DivisionConfiguration divisionConfig;

	private GameObject[] enemyPrefabs;
	private int index;
	#region Unity
	private void OnValidate()
	{
		switch (divSet)
		{
			case DivisionStates.Endless:
				divisionConfig.general.endlessMove = true;
				divisionConfig.general.isFormMoving = false;
				divisionConfig.general.formation = null;
				divisionConfig.formMove = null;
				divisionConfig.aISettings = null;
				divisionConfig.general.isChasingPlayer = false;
				divisionConfig.general.isSwampForm = false;
				break;
			case DivisionStates.Formation:
				divisionConfig.general.endlessMove = false;
				break;
			case DivisionStates.TwoPoints:
				divisionConfig.general.endlessMove = false;
				divisionConfig.smooth.smoothMovement = true;
				break;
			case DivisionStates.ChasingPlayer:
				divisionConfig.general.isChasingPlayer = true;
				divisionConfig.general.isFormMoving = false;
				divisionConfig.general.endlessMove = false;
				divisionConfig.formMove = null;
				break;
		}
	}
	void Start()
	{
		waypoints = new List<Transform>();
		formationWaypoints = new List<Transform>();

		if (divisionConfig.general.path != null)
		{
			foreach (Transform child in divisionConfig.general.path.transform)
			{
				waypoints.Add(child);
			}
		}

		if (divisionConfig.general.formation != null)
		{
			foreach (Transform child in divisionConfig.general.formation.transform)
			{
				formationWaypoints.Add(child);
			}
		}

		EnemyCount.Instance.CountEnemiesAtScene(divisionConfig.spawns.numberOfEnemies);
		StartCoroutine(InstantiateDivision());
	}
	#endregion
	private void Handler()
	{
		OnLastEnemyAtFormation?.Invoke();
	}
	private GameObject InstatiatePrefab(DivisionStates set)
	{
		int i = UnityEngine.Random.Range(0, enemyPrefabs.Length);
		int idPos;
		if (set == DivisionStates.TwoPoints) idPos = index;
		else idPos = 0;

		GameObject enemy = Instantiate(enemyPrefabs[i],
						   waypoints[idPos].position,
						   Quaternion.identity);
		enemy.transform.Rotate(new Vector3(0, 0, 180));
		return enemy;
	}
	IEnumerator InstantiateDivision()
	{
		EnemyPathfinding ep = null;
		yield return new WaitForSeconds(delayToSpawn);
		enemyPrefabs = divisionConfig.general.enemyPrefabs;
		for (index = 0; index < divisionConfig.spawns.numberOfEnemies; index++)
		{
			GameObject newEnemy = InstatiatePrefab(divSet);
			ep = newEnemy.GetComponent<EnemyPathfinding>();
			ep.SetDivisionConfiguration(divisionConfig, this, id: index);

			switch (divSet)
			{
				case DivisionStates.Endless:
					ep.StartingState(EnemyMovingState.DivisionToPath);
					break;

				case DivisionStates.Formation:
					ep.StartingState(EnemyMovingState.DivisionToPath);
					break;

				case DivisionStates.TwoPoints:
					ep.StartingState(EnemyMovingState.PointToPoint);
					break;
				case DivisionStates.ChasingPlayer:
					ep.StartingState(EnemyMovingState.ChasingPlayer);
					break;
			}
			yield return new WaitForSeconds(divisionConfig.spawns.timeBetweenSpawns);
		}
		ep.OnPositionForamtion += Handler;
	}
}
public enum DivisionStates
{
	Endless,
	Formation,
	TwoPoints,
	ChasingPlayer
}