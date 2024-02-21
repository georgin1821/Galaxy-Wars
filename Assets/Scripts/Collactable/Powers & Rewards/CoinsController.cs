using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsController : SimpleSingleton<CoinsController> 
{

    [SerializeField] GameObject coinPrefab;

    float randomPosFactor = 0.5f;

    //is called from enemy when is dying
    public void DropGold(Transform trans, bool multipleCoins, int totalCoins)
    {
        if (multipleCoins)
        {
            totalCoins = Random.Range(totalCoins - 1, totalCoins + 2);

            for (int i = 0; i < totalCoins; i++)
            {
                float posX = Random.Range(trans.position.x - randomPosFactor, trans.position.x + randomPosFactor);
                Vector3 pos = trans.position;
                pos.x = posX;

                Instantiate(coinPrefab, pos, Quaternion.identity);
            }
        }
        else
        {
            Instantiate(coinPrefab, trans.position, Quaternion.identity);
        }
    }
}
