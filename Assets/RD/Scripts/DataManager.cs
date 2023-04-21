using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField]
    private List<EnemyData> enemyDatas;
    private List<TowerData> towerDatas;

    public EnemyData GetEnemyData(int i)
    {
        return enemyDatas[i];
    }

    public TowerData GetTowerData(int i)
    {
        return towerDatas[i];
    }

    public int GetEnemyDataCount()
    {
        return enemyDatas.Count;
    }

    public int GetTowerDataCount()
    {
        return towerDatas.Count;
    }
}
