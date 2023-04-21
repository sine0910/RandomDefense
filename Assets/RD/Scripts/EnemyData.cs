using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyData
{
    public int health;
    public float speed;
    public int attack;

    public EnemyData(int h, float s, int a)
    {
        health = h;
        speed = s;
        attack = a;
    }
}
