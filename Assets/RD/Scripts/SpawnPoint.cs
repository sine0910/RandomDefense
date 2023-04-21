using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    private int layer = 0;

    [SerializeField]
    private Transform towerPos;

    [SerializeField]
    private GameObject point;

    private SlimeTower tower;

    bool isSetTower = false;

    public void SetTower(SlimeTower t)
    {
        isSetTower = true;
        tower = t;
        tower.transform.position = towerPos.position;
    }

    public bool GetIsSetTower()
    {
        if (isSetTower)
        {
            return true;
        }
        return false;
    }

    public void SetActive(bool b)
    {
        point.SetActive(b);
    }

    private void OnMouseDown()
    {
        Debug.Log("SpawnPoint OnMouseDown");
        //GameManager.instance.UI.OnClickTowerSpawner(this);
    }

    private void OnMouseUp()
    {
        Debug.Log("SpawnPoint OnMouseUp");

        if (isSetTower)
        {
            GameManager.instance.OnClickTowerSpawner(this);
        }
        else
        {
            
        }
    }
}
