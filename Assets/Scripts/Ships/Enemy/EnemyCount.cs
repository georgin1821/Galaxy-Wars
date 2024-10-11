using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCount : SimpleSingleton<EnemyCount>
{

    public int Count;

    public  void CountEnemiesAtScene(int number)
    {
        Count += number;
    }

}
