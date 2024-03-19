using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class EnemyProjectileController : MonoBehaviour
{

	[SerializeField] [Range(1, 10)]float speed;
	[SerializeField][Range(15, 50)] int rotationSpeed;

	[SerializeField] bool isTargetingPlayer = false;
	[SerializeField] bool isSeekingPlayer = false;

	private void OnValidate()
	{
		if (isSeekingPlayer) isTargetingPlayer = false;
	}


	private void Start()
	{
		if (isTargetingPlayer)
		{
			if (Player.Instance != null)
			{
				Vector3 target = Player.Instance.transform.position;
				Vector3 dir = target - transform.position;

				transform.rotation = Quaternion.FromToRotation(Vector3.down, dir);
			}
		}
	}
	void Update()
	{
		if (isSeekingPlayer)
		{
			if (Player.Instance != null)
			{
				Vector3 target = Player.Instance.transform.position;
				Vector3 dir = (target - transform.position).normalized;

				Quaternion rot = Quaternion.LookRotation(Vector3.forward, -dir);
				transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, rotationSpeed * Time.deltaTime);
			}
		}

		transform.Translate(Vector3.down * speed * Time.deltaTime);
	}

}
