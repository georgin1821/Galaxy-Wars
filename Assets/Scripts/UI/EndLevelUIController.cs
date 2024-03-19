using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class EndLevelUIController : MonoBehaviour
{
	public TMP_Text rewardedCoins, rewardedGems, rewardedCards, dificultyTxt,  stageTct,scoreTxt;
	void Start()
    {
       rewardedCoins.text = GameDataManager.Instance.LevelCoins.ToString();
       rewardedCards.text = Stages.Instance.stages[  GameDataManager.Instance.CurrentLevel].rewShipCards.ToString();
       rewardedGems.text = Stages.Instance.stages[  GameDataManager.Instance.CurrentLevel].rewGems.ToString();
        dificultyTxt.text = GameDataManager.Instance.currentDifficulty.ToString();
        stageTct.text = GameDataManager.Instance.CurrentLevel.ToString();
        scoreTxt.text = GameDataManager.Instance.LevelScore.ToString();

	}

	public void SetInfo()
	{
		
	}
	void Update()
    {
        
    }
}
