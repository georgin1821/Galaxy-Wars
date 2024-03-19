using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyWeaponAbstract : MonoBehaviour
{
	[SerializeField] protected GameObject prjectilePrefab;

	[Space(5)]
	[SerializeField] protected float chanceToFire;
	[SerializeField][Range(1, 5)] protected int delayToShoot = 2;

	protected int autoRepeat = 3;

	abstract protected void FireChance();
	abstract public void Firing();

}
