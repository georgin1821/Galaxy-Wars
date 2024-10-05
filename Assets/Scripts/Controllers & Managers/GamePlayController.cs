using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public sealed class GamePlayController : SimpleSingleton<GamePlayController>
{

	//-------Events
	public static event Action<GameState> OnGameStateChange;
	public static event Action OnScrollingBGEnabled;

	[Header("Prefabs")]
	[SerializeField] private GameObject[] scrollingBgsPrefabs;


	//------public fields
	public GameState state; //---------*to be serialize only--------------
	[HideInInspector] public bool andOfAnimation;
	[HideInInspector] public float Difficulty;
	[HideInInspector] public int levelCoins;

	//-------private fields
	private GameObject[] shipsPrefabs;
	private GameDifficulty gameDifficulty;
	private GameObject[] sbgGameObjects;
	private GameObject playerPrefab;
	private Vector3 shipStartingPos = new Vector3(0, -10, 0);
	private int score;
	private int levelScore;
	private AudioType victoryClip;
	private AudioType soundtrack;


	private int currentSquadShip = 0;

	//int batterySpend;
	public int ShipPower { get; private set; }

	//Dev inspector

	#region Unity
	private void Start()
	{
		InitializeGame();

		victoryClip = AudioType.victory;
		soundtrack = LevelSpawner.Instance.Levels[GameDataManager.Instance.CurrentLevel].MusicClip;
		PlayClipWithFade(soundtrack);

		UpdateState(GameState.INIT);
	}
	private void Update()
	{
		switch (state)
		{
			case GameState.INIT:
				if (andOfAnimation) UpdateState(GameState.LOADLEVEL);
				break;
		}
	}
	#endregion

	#region private methods
	private void InitializeGame()
	{
		/* ------REFACTORING depends from other app systems-------
         */
		SetSquadPrefabs();
		SetLevelDifficulty();
	}
	private void SetSquadPrefabs()
	{
		//set the player squad prefabs from GameDataManager
		GameObject pr1 = GameDataManager.Instance.squad[0].shipPrefab;
		GameObject pr2 = GameDataManager.Instance.squad[1].shipPrefab;
		GameObject pr3 = GameDataManager.Instance.squad[2].shipPrefab;
		shipsPrefabs = new GameObject[] { pr1, pr2, pr3 };
	}
	public void SetLevelDifficulty()
	{

		//set a float for each difficulty level
		gameDifficulty = GameDataManager.Instance.currentDifficulty;

		switch (gameDifficulty)
		{
			case GameDifficulty.EASY:
				Difficulty = 0;
				break;

			case GameDifficulty.MEDIUM:
				Difficulty = 1.5f;
				break;

			case GameDifficulty.HARD:
				Difficulty = 3f;
				break;
		}
	}
	private void ClearsLevel()
	{
		// destroy enemies objects and scrolling bgs objs at scene
		Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
		if (enemies.Length != 0)
		{
			foreach (var enemy in enemies)
			{
				Destroy(enemy.gameObject);
			}
		}
		if (sbgGameObjects != null)
		{
			foreach (var sbg in sbgGameObjects)
			{
				Destroy(sbg.gameObject);
			}
		}
		WaveSpawner.Instance.DestroyWaves();
	}
	private void InstantiateScrollingBackgrounds()
	{
		sbgGameObjects = new GameObject[scrollingBgsPrefabs.Length];

		for (int i = 0; i < scrollingBgsPrefabs.Length; i++)
		{
			sbgGameObjects[i] = Instantiate(scrollingBgsPrefabs[i]);
		}

		OnScrollingBGEnabled?.Invoke();
	}
	private void InstantiatePlayer()
	{
		Player player = FindAnyObjectByType<Player>();
		if (player == null)
		{
			Instantiate(playerPrefab, shipStartingPos, Quaternion.identity);
		}
	}
	#endregion

	public void UpdateState(GameState newState)
	{
		state = newState;
		switch (newState)
		{
			case GameState.INIT:
				Time.timeScale = 1;

				//Clears remaining enemies  sbgs and divs game Objects
				ClearsLevel();

				//Instantiation
				playerPrefab = shipsPrefabs[0];
				InstantiateScrollingBackgrounds();
				InstantiatePlayer();
				BeginIntroSequence(false);

				//UI
				score = 0;
				GameUIController.Instance.UpdateScore(score);
				EnemyCount.instance.Count = 0;
				levelScore = 0;
				levelCoins = 0;
				//update state to next only when player anim finishes

				break;

			case GameState.LOADLEVEL:

				LevelSpawner.Instance.SpawnLevelWithIndex(GameDataManager.Instance.CurrentLevel);

				UpdateState(GameState.PLAY);
				break;

			case GameState.PLAY:
				Time.timeScale = 1;
				break;

			case GameState.LEVELCOMPLETE:
				Player.Instance.StopShootingClip();

				int unlockedLevel = GameDataManager.Instance.CurrentLevel;
				if (GameDataManager.Instance.isLevelUnlocked[unlockedLevel + 1] == false)
				{
					GameDataManager.Instance.isLevelUnlocked[unlockedLevel + 1] = true;
				}

				if (GameDataManager.Instance.levelCompletedDifficulty[unlockedLevel] < (LevelCompletedDifficulty)gameDifficulty)

				{
					GameDataManager.Instance.levelCompletedDifficulty[unlockedLevel] = (LevelCompletedDifficulty)gameDifficulty;
				}
				GameDataManager.Instance.LevelCoins = levelCoins;
				GameDataManager.Instance.LevelScore = levelScore;
				GameDataManager.Instance.batteryLife -= 10;

				GameDataManager.Instance.LevelIndex = LevelSpawner.Instance.LevelIndex;
				GameDataManager.Instance.Save();

				StopAudioWithFade(soundtrack);
				PlayClipWithFade(victoryClip);

				StartCoroutine(LoadNextLvlWithDelay());
				break;

			case GameState.PLAYERDEATH:
				currentSquadShip++;
				if (currentSquadShip >= shipsPrefabs.Length)
				{
					UpdateState(GameState.DEFEAT);
					return;
				}
				Destroy(Player.Instance.gameObject);
				Player.Instance.DestroySingleton();
				PlayerController.Instance.DestroySingleton();
				playerPrefab = shipsPrefabs[currentSquadShip];
				Instantiate(playerPrefab, shipStartingPos, Quaternion.identity);
				StartCoroutine(PlayerStartingAnim(false));
				Player.Instance.ShieldsUp();
				UpdateState(GameState.PLAY);
				break;

			case GameState.DEFEAT:
				Time.timeScale = 0;
				Player.Instance.StopShootingClip();
				// WaveSpawner.Instance.StopAllCoroutines();
				break;

			case GameState.LEVELCOMPLETE_UI:
				break;

			case GameState.RETRY:
				Time.timeScale = 1;
				UpdateState(GameState.INIT);
				break;

			case GameState.PAUSE:
				Time.timeScale = 0;
				Player.Instance.StopShootingClip();
				break;

			case GameState.EXIT:
				AudioController.Instance.StopAudio(soundtrack, true);
				break;
		}

		OnGameStateChange?.Invoke(state);

	}

	#region public methods
	public void BeginIntroSequence(bool isRiviving)
	{
		StartCoroutine(PlayerStartingAnim(isRiviving));
	}
	public void AddToScore(int scoreValue)
	{
		int startScore = score;
		score += scoreValue;
		levelScore += scoreValue;
		StartCoroutine(GameUIController.Instance.UpdateScoreRoutine(startScore, score));
	}
	#endregion
	//LevelUpSystem
	private void PlayClipWithFade(AudioType clip)
	{
		if (clip != AudioType.None)
		{
			AudioController.Instance.PlayAudio(clip, true);
		}
	}
	private void StopAudioWithFade(AudioType clip)
	{
		if (clip != AudioType.None)
		{
			AudioController.Instance.StopAudio(clip, true);
		}
	}

	#region Couritines
	private IEnumerator PlayerStartingAnim(bool isRiviving)
	{
		andOfAnimation = false; // for external ref

		if (!GameManager.Instance.isSpeedLevel)
		{
			AnimationClip clip = Player.Instance.GetComponent<Animator>().runtimeAnimatorController.animationClips[0];
			float animtime = clip.length;
			float t;  // time to wait for begin animation
			if (isRiviving)
			{
				t = 0;
			}
			else
			{
				t = 1;
			}
			yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(t));
			Player.Instance.PlayerAnimation();
			yield return new WaitForSecondsRealtime(animtime);
			andOfAnimation = true;
		}
		else
		{
			yield return null;
			andOfAnimation = true;

		}

	}
	private IEnumerator LoadNextLvlWithDelay()
	{
		yield return new WaitForSecondsRealtime(3);
		UpdateState(GameState.LEVELCOMPLETE_UI);
		GameManager.Instance.loadingFrom = LoadingFrom.LVLCOMP;
		LoadingWithFadeScenes.Instance.LoadScene("LevelSelect");

	}
	#endregion
}

public enum GameState
{
	INIT,
	LOADLEVEL,
	PLAY,
	LEVELCOMPLETE,
	RETRY,
	PAUSE,
	PLAYERDEATH,
	DEFEAT,
	LEVELCOMPLETE_UI,
	EXIT
}