using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private float spawnTime;
    public List<WayPoints> wayPointsList = new List<WayPoints>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            GameObject clone = GameManager.instance.prefabs.GetEnemy(0);
            Enemy enemy = clone.GetComponent<Enemy>();

            enemy.Setup(wayPointsList[0].wayPoints);

            yield return new WaitForSeconds(spawnTime);
        }
    }
}

[System.Serializable]
public class WayPoints
{
    public Transform[] wayPoints;
}
