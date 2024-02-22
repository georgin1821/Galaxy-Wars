using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class EndLevelUIController : MonoBehaviour
{
	public TMP_Text rewardedCoins, rewardedGems, rewardedCards;
	void Start()
    {
       rewardedCoins.text = Stages.Instance.stages[  GameDataManager.Instance.CurrentLevel].rewCoins.ToString();
       rewardedCards.text = Stages.Instance.stages[  GameDataManager.Instance.CurrentLevel].rewShipCards.ToString();
       rewardedGems.text = Stages.Instance.stages[  GameDataManager.Instance.CurrentLevel].rewGems.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
