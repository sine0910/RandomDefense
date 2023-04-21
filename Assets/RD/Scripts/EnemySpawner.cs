using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private float spawnTime;

    [SerializeField]
    private List<WayPoints> wayPointsList;
    private List<Enemy> enemyList;

    public List<Enemy> EnemyList => enemyList;

    void Awake()
    {
        enemyList = new List<Enemy>();

        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            GameObject clone = GameManager.instance.prefabs.GetEnemy(0);
            Enemy enemy = clone.GetComponent<Enemy>();
            EnemyData data = GameManager.instance.data.GetEnemyData(0);

            enemy.Setup(this, data, wayPointsList[0].wayPoints);
            enemyList.Add(enemy);

            yield return new WaitForSeconds(spawnTime);
        }
    }

    public void DestroyEnemy(Enemy e)
    {
        enemyList.Remove(e);
    }
}

[System.Serializable]
public class WayPoints
{
    public Transform[] wayPoints;
}
