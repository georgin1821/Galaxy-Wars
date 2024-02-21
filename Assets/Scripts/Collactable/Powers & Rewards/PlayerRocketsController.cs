using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRocketsController : PlayerCollectiblesControllerAbstract
{

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        Player.Instance.FireRockets();

    }
}
