using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

public class GameDataManager : Singleton<GameDataManager>
{
    [HideInInspector] public int LevelScore { get; set; }
    [HideInInspector] public int LevelCoins { get; set; }
    [HideInInspector] public int LevelIndex { get; set; }
    [HideInInspector] public DateTime currentTime { get; set; }
    [HideInInspector] public DateTime nextSessionTime;
    //Data
    [HideInInspector] public int selectedShip;
    [HideInInspector] public int gems;
    [HideInInspector] public int shipUpgradeCards;
    [HideInInspector] public int batteryLife;
    [HideInInspector] public bool isGameStartedFirstTime;
     public List<bool> isLevelUnlocked;
    [HideInInspector] public LevelCompletedDifficulty[] levelCompletedDifficulty;
    [HideInInspector] public int coins;
    [HideInInspector] public int levelsCount;
    [HideInInspector] private int levelsComplete = 0;
    [HideInInspector] public int squadsUnlocked = 1; //?
    [HideInInspector] public DateTime sessionTime;
    [HideInInspector] public int enemiesKilled;
    [HideInInspector] public float musicVolume;
    [HideInInspector] public float soundVolume;
    [HideInInspector] public GameDifficulty currentDifficulty;
    [HideInInspector] public bool[] dailyRewards;
    [HideInInspector] public bool[][] shipsSkills;//?
    [HideInInspector] public int CurrentLevel { get; set; }
    //Ships details
    public Ship[] ships; //ship details
    [HideInInspector] public Ship[] squad;
     public bool[] isShipUnlocked;  //bool to true to unlock a ship in the array
    [HideInInspector] public List<int> shipsPower;
    [HideInInspector] public List<int> shipsRank;
    public List<GameObject> shipsPrefab;
    private int shipsCount;
    /// <summary>
    /// /////
    /// </summary>

    private GameData data;
    private LevelCompletedDifficulty gameDifficulty;

    void Start()
    {
        CurrentLevel = 0;
    }
    private void OnApplicationQuit()
    {
        Save();
    }

    public void InitializeGameDate()
    {
        currentTime = DateTime.UtcNow;

        Load();
        if (data != null)
        {
            isGameStartedFirstTime = data.IsGameStartedFirstTime;
            nextSessionTime = DateTime.UtcNow;
            SessionController.instance.RewardCheckOnStart(sessionTime, nextSessionTime);
            sessionTime = nextSessionTime;
        }
        else
        {
            isGameStartedFirstTime = true;
        }

        if (isGameStartedFirstTime)
        {
            ships = ShipManager.Instance.shipsDetails;
            shipsCount = ships.Length;
            levelsCount = Stages.Instance.StagesCount;

            sessionTime = DateTime.UtcNow;
            coins = 200;
            gems = 10;
            shipUpgradeCards = 0;
            batteryLife = 90;
            enemiesKilled = 0;
            levelCompletedDifficulty = new LevelCompletedDifficulty[5];
            shipsPower = new List<int>();
            shipsRank = new List<int>();
            dailyRewards = new bool[4];
            currentDifficulty = GameDifficulty.EASY;
            musicVolume = 0.7f;
            soundVolume = 0.5f;
            isGameStartedFirstTime = false;

            UnlockSquads();
           squad = new Ship[3] { ships[0], ships[1], ships[2] }; // squad

            //Unlock ships
            isShipUnlocked = new bool[shipsCount];
            isShipUnlocked[0] = true;
            for (int i = 1; i < shipsCount; i++)
            {
                isShipUnlocked[i] = false;
            }

            isLevelUnlocked = new List<bool>();
            for (int i = 0; i < levelsCount; i++)
            {
                isLevelUnlocked.Add(false);
            }
            isLevelUnlocked[0] = true; //test

            //unlocking first ship locks the others
            for (int i = 0; i < dailyRewards.Length; i++)
            {
                dailyRewards[i] = false;
            }
            for (int i = 0; i < levelCompletedDifficulty.Length; i++)
            {
                levelCompletedDifficulty[i] = LevelCompletedDifficulty.NONE;
            }

            //test
            // levelCompletedDifficulty[0] = LevelCompletedDifficulty.MEDIUM;
            //  levelCompletedDifficulty[1] = LevelCompletedDifficulty.EASY;


            data = new GameData();

            data.Coins = coins;
			data.Gems = gems;
			data.ShipUpgradeCards = shipUpgradeCards;

			data.IsGameStartedFirstTime = isGameStartedFirstTime;
            data.IsLevelUnlocked = isLevelUnlocked;
            data.Squad = squad;
            data.IsShipUnlocked = isShipUnlocked;
            data.ShipsPower = shipsPower;
            data.SelectedShip = selectedShip;
            data.ShipsRank = shipsRank;
            data.SquadsUnlocked = squadsUnlocked;
            data.CurrentDifficulty = currentDifficulty;
            data.SessionTime = sessionTime;
            data.EnemiesKilled = enemiesKilled;
            data.MusicVolume = musicVolume;
            data.SoundVolume = soundVolume;
            data.DailyRewards = dailyRewards;
            data.Squad = squad;
            data.ShipsPrefab = shipsPrefab;


            Save();
            Load();
        }
    }
    public void Save()
    {
        FileStream file = null;

        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Create(Application.persistentDataPath + "/GamaData.dat");
            if (data != null)
            {
                data.Coins = coins;
                data.Gems = gems;
                data.ShipUpgradeCards = shipUpgradeCards;
                data.IsGameStartedFirstTime = isGameStartedFirstTime;
                data.IsLevelUnlocked = isLevelUnlocked;
               data.Squad = squad;
                data.IsShipUnlocked = isShipUnlocked;
                data.ShipsRank = shipsRank;
                data.SquadsUnlocked = squadsUnlocked;

                data.SelectedShip = selectedShip;
                data.CurrentDifficulty = currentDifficulty;
                data.BatteryLife = batteryLife;
                data.SessionTime = sessionTime;
                data.EnemiesKilled = enemiesKilled;
                data.MusicVolume = musicVolume;
                data.SoundVolume = soundVolume;
                data.Skills = shipsSkills;
                data.DailyRewards = dailyRewards;
                data.Squad = squad;
                data.ShipsPrefab = shipsPrefab;

                bf.Serialize(file, data);
            }
        }
#pragma warning disable CS0168 // Variable is declared but never used
        catch (Exception e)
#pragma warning restore CS0168 // Variable is declared but never used
        {

        }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }
    }
    private void Load()
    {
        FileStream file = null;
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Open(Application.persistentDataPath + "/GameData.dat", FileMode.Open);
            data = (GameData)bf.Deserialize(file);
        }
#pragma warning disable CS0168 // Variable is declared but never used
        catch (Exception e)
#pragma warning restore CS0168 // Variable is declared but never used
        {

        }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }
    }

    private void UnlockSquads()
    {
        if (levelsComplete < 5)
        {
            squadsUnlocked = 1;
        }
    }
}

public enum GameDifficulty
{
    EASY = 1,
    MEDIUM = 2,
    HARD = 3
}

public enum LevelCompletedDifficulty
{
    NONE = 0,
    EASY = 1,
    MEDIUM = 2,
    HARD = 3
}
[Serializable]
class GameData
{
    public List<GameObject> ShipsPrefab { get; set; }

    public int SelectedShip { get; set; }
    public Ship[] Squad;
    public int Coins { get; set; }
    public int ShipUpgradeCards { get; set; }
    public bool[] IsShipUnlocked { get; set; }
    public List<int> ShipsPower { get; set; }
    public List<int> ShipsRank { get; set; }
    public int Gems { get; set; }
    public int SquadsUnlocked { get; set; }
    public bool IsGameStartedFirstTime { get; set; }
    public List<bool> IsLevelUnlocked { get; set; }
    public int BatteryLife { get; set; }
    public DateTime SessionTime { get; set; }
    public GameDifficulty CurrentDifficulty { get; set; }
    public int EnemiesKilled { get; set; }
    public float MusicVolume { get; set; }
    public float SoundVolume { get; set; }
    public bool[] DailyRewards { get; set; }
    public bool[][] Skills { get; set; }
    public LevelCompletedDifficulty[] levelCompletedDifficulty { get; set; }


}
