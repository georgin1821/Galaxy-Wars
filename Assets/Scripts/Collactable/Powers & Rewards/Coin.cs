using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : PlayerCollectiblesControllerAbstract
{

    private float speed = -1f; // starts to move upwards and accelerates
    private float accelaration = 2.5f;

   protected override void Update()
    {
        {
            if (Vector3.Distance(Player.Instance.transform.position, this.transform.position) > distanceFromPlayer && !isAttractedFromPlayer)
            {
                speed = Mathf.Clamp(speed + (accelaration * Time.deltaTime), -1f, 5f);
                transform.Translate(new Vector3(0, -forwardSpeed, 0) * speed * Time.deltaTime);
            }
            else
            {
                Vector3 target = Player.Instance.transform.position;
                transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothTime, 15);
				isAttractedFromPlayer = true;

			}
		}
    }


    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
            AudioController.Instance.PlayAudio(AudioType.CollectGold);
            int currentCoins = GameDataManager.Instance.coins;
            GameDataManager.Instance.coins += 100;
            GamePlayController.Instance.levelCoins += 100;
            StartCoroutine(GameUIController.Instance.UpdateScoreRoutine(currentCoins, GameDataManager.Instance.coins));
        }
    }

}
