using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerCollectiblesControllerAbstract : MonoBehaviour
{
	[Header("General Stats")]
	[SerializeField][Range(0f, 2f)] protected float distanceFromPlayer;
	[SerializeField][Range(0f, .2f)] protected float smoothTime;
	[SerializeField] protected float forwardSpeed;
	[SerializeField] [Range(0f, 2f)] protected float speedRandomFactor;

	[Header("VFXS Prefabs")]
	[SerializeField] GameObject gainPowerVFX;
	[SerializeField] AudioType collectClip;

	protected Vector3 velocity = Vector3.zero;
	protected bool isAttractedFromPlayer = false;

	protected void Start()
	{
		forwardSpeed = Random.Range(forwardSpeed - speedRandomFactor, forwardSpeed + speedRandomFactor);

	}
	protected virtual void Update()
	{
		// if the distance from player is greater than dfp power goes down and 
		//if less than dfp power goes to player with the SmoothDamp method

		//Movement
		if (Vector3.Distance(Player.Instance.transform.position, this.transform.position) > distanceFromPlayer && !isAttractedFromPlayer)
		{
			transform.Translate(-Vector3.up * forwardSpeed * Time.deltaTime);
		}
		else
		{
			Vector3 target = Player.Instance.transform.position;

			transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothTime, Mathf.Infinity, Time.deltaTime);
			isAttractedFromPlayer = true;
		}
	}

	protected virtual void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			Destroy(gameObject);
			AudioController.Instance.PlayAudio(collectClip);
			Instantiate(gainPowerVFX, Player.Instance.transform.position, Quaternion.identity);
		}
	}

}
