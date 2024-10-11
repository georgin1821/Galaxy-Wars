using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(IPlayerWeapon))]
public class Player : SimpleSingleton<Player>
{
	[Header("GameDev Settings")]
	public bool isAlwaysShooting = true;
	public bool isAbleToFire = true;
	[SerializeField] bool collideWithEnemy = true;

	[Space(10)]
	[Header("VFX")]
	// are in gameprfabs children components
	[SerializeField] private GameObject engineVFX;
	[SerializeField] private GameObject shootingVFX;

	[Tooltip("Shooting")]
	public Transform firePos;

	[Space(5)]
	[Header("Powers")]
	[SerializeField] private GameObject rocketPrefab;
	private GameObject[] targets;
	private int rocketsToShoot = 7;
	private float arcAngle = 40;
	private int gunUpgrades = 1;
	private int shieldsDuration = 5;
	private bool playerHasShield;

	[Space(5)]
	private AudioSource audioSource;
	private Animator anim;
	private Coroutine co;
	private IPlayerWeapon[] weapons;

	#region unity circle
	protected override void Awake()
	{
		base.Awake();

		audioSource = GetComponent<AudioSource>();
		anim = GetComponent<Animator>();
		weapons = GetComponents<IPlayerWeapon>();
	}
	private void OnEnable()
	{
		// runs after Awake method so runs after singleton initialization!
		if (GamePlayController.IsInitialized)
		{
			GamePlayController.OnGameStateChange += GameStateChangeHandle;
		}
		else
		{
			Debug.Log("GamePlayController instance is null");
		}
	}
	private void OnDisable()
	{
		GamePlayController.OnGameStateChange -= GameStateChangeHandle;
	}
	private void Start()
	{
		StopShootingClip();
		audioSource.loop = true; // plays the continuous shooting sound
		targets = new GameObject[rocketsToShoot]; // every rocket has a target
	}
	private void Update()
	{
		//dev test perpose to be removed!!!
		if (Input.GetMouseButtonDown(2))
		{
			InstatiateRockets();
		}
	}
	#endregion


	public void StopShootingClip()
	{
		audioSource.Stop();
	}

	#region powers
	public void InstatiateRockets()
	{
		if (GamePlayController.Instance.state == GameState.PLAY)
		{
			targets = GameObject.FindGameObjectsWithTag("Enemy");
			for (int i = 0; i < rocketsToShoot; i++)
			{
				GameObject rocket = Instantiate(rocketPrefab, firePos.position, Quaternion.identity);
				rocket.transform.Rotate(0, 0, arcAngle - i * 15);
				AudioController.Instance.PlayAudio(AudioType.PalyerShootRockets);
			}
		}
	}
	public void ShieldsUp()
	{
		if (co != null)
		{
			StopCoroutine(co);
		}
		co = StartCoroutine(ShieldsCountDown());
	}
	public void UpgradeGun()
	{
		if (GamePlayController.Instance.state == GameState.PLAY)
		{
			foreach (var weapon in weapons)
			{
				weapon.WeaponUpgrades++;
				gunUpgrades++; // the same ranks for all weapons and same maximum
			}

			GameUIController.Instance.UpdateWeaponRankStatus(gunUpgrades);
		}
	}
	public void MaxGunsPower()
	{
		if (GamePlayController.Instance.state == GameState.PLAY)
		{
			foreach (var weapon in weapons)
			{
				weapon.WeaponUpgrades++;
				gunUpgrades++; // the same ranks for all weapons and same maximum
			}

			GameUIController.Instance.UpdateWeaponRankStatus(gunUpgrades);
		}
	}
	#endregion


	public void DamagePlayer()
	{
		if (!collideWithEnemy || playerHasShield) return;

		PlayerDeath();
	}
	#region public methods
	public void PlayerAnimation()
	{
		transform.position = new Vector3(0, -6, 0);
		anim.Play("Intro");
	}
	#endregion


	#region private methods
	private void PlayerDeath()
	{
		GamePlayController.Instance.UpdateState(GameState.PLAYERDEATH);
		AudioController.Instance.PlayAudio(AudioType.PlayerDeath);
	}
	private void GameStateChangeHandle(GameState state)
	{
		if (state == GameState.INIT)
		{
			foreach (var weapon in weapons)
			{
				weapon.WeaponUpgrades = 1;
			}
		}
		if (state == GameState.PLAY)
		{
			audioSource.Play();
			engineVFX.GetComponent<ParticleSystem>().Play();
			shootingVFX.GetComponent<ParticleSystem>().Play();
		}
	}
	private void OnTriggerEnter(Collider other)
	{
		{
			if (!collideWithEnemy || playerHasShield) return;
			if (GamePlayController.Instance.state == GameState.PLAY && other.tag == "Enemy")
			{
				PlayerDeath();
			}
		}
	}
	private IEnumerator ShieldsCountDown()
	{
		playerHasShield = true;
		GameObject shield = transform.Find("Shields").gameObject;
		shield.SetActive(true);
		AudioController.Instance.PlayAudio(AudioType.PlayerShields);
		yield return new WaitForSeconds(shieldsDuration);
		playerHasShield = false;
		shield.SetActive(false);
	}
	#endregion
}