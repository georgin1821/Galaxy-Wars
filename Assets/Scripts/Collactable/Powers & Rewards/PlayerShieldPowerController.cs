using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerShieldPowerController : PlayerCollectiblesControllerAbstract
{
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
		floatingText.GetComponentInChildren<TextMeshPro>().text = "SHIELDS";

		Player.Instance.ShieldsUp();
    }
}
