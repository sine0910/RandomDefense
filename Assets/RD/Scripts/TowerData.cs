using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerData
{
    public float attackRange = 3.0f;
    public float attackRate = 1.0f;
    public float attackPoint = 1.0f;
    public int attackCount = 1;

    public int attackType = 0;

    public TowerData(float range, float rate, float point, int count, int type)
    {
        attackRange = range;
        attackRate = rate;
        attackPoint = point;
        attackCount = count;
        attackType = type;
    }
}
