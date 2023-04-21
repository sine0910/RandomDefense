using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    [SerializeField]
    public GameObject[] enemyPrefabs;
    public List<GameObject>[] enemyPool;

    public GameObject[] towerPrefabs;
    public List<GameObject>[] towerPool;

    public GameObject cardPrefabs;
    public List<GameObject> cardPool;

    private void Awake()
    {
        InitPools();
    }

    void InitPools()
    {
        enemyPool = new List<GameObject>[enemyPrefabs.Length];
        towerPool = new List<GameObject>[towerPrefabs.Length];
        cardPool = new List<GameObject>();

        for (int i = 0; i < enemyPool.Length; i++)
        {
            enemyPool[i] = new List<GameObject>();
        }

        for (int i = 0; i < towerPool.Length; i++)
        {
            towerPool[i] = new List<GameObject>();
        }
    }

    public GameObject GetEnemy(int t)
    {
        GameObject select = null;

        foreach (GameObject item in enemyPool[t])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);

                break;
            }
        }

        if (!select)
        {
            select = Instantiate(enemyPrefabs[t], transform);
            enemyPool[t].Add(select);
        }

        return select;
    }

    public GameObject GetTower(int t)
    {
        GameObject select = null;

        foreach (GameObject item in towerPool[t])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);

                break;
            }
        }

        if (!select)
        {
            select = Instantiate(towerPrefabs[t], transform);
            towerPool[t].Add(select);
        }

        return select;
    }

    public GameObject GetCard(Transform t)
    {
        GameObject select = null;

        foreach (GameObject item in cardPool)
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);

                break;
            }
        }

        if (!select)
        {
            select = Instantiate(cardPrefabs, t);
            cardPool.Add(select);
        }

        return select;
    }
}
