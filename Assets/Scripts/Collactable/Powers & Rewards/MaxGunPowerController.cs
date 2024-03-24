using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MaxGunPowerController : PlayerCollectiblesControllerAbstract
{
	protected override void OnTriggerEnter(Collider other)
	{
		base.OnTriggerEnter(other);
		floatingText.GetComponentInChildren<TextMeshPro>().text = "MAXGUNS";

		Player.Instance.MaxGunsPower();

	}
}
