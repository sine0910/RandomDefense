using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TowerCard : MonoBehaviour, IPointerClickHandler
{
    public GameObject[] starImage;

    public Image profileImage;
    public Image typeImage;
    public Image attackImage;

    public Text nameText;

    public GameObject batchObj;

    private SpawnPoint towerPos;

    public TowerCardData towerData;

    public bool isSet;

    public void Setup(TowerCardData t)
    {
        towerData = t;
    }

    public void SetSpawnPos(SpawnPoint t)
    {
        towerPos = t;
        isSet = true;
    }

    public void SetTowerOnSpawnPoint(SpawnPoint p)
    {
        towerPos = p;
    }

    public int GetTowerIndex()
    {
        return towerData.towerIndex;
    }

    float clickTime;
    float doubleTouchTimer = 0.75f;
    public void OnPointerClick(PointerEventData eventData)
    {
        switch (GameManager.instance.GetAct())
        {
            case ACT.NONE:
                {
                    if (isSet)
                    {
                        //더블터치가 가능한 시점에서만 더블터치를 지원한다.
                        if ((Time.time - clickTime) < doubleTouchTimer)
                        {
                            //카메라를 자신의 타워가 있는 곳으로 이동시키는 함수를 실행시킨다.

                            clickTime = 0;
                            return;
                        }
                        else
                        {
                            clickTime = Time.time;
                        }
                    }
                    else
                    {
                        Debug.Log("Card OnClick");
                        GameManager.instance.UI.OnClickCard(this);
                    }
                }
                break;

             default:
                {
                    Debug.Log("Card OnClick");
                    GameManager.instance.UI.OnClickCard(this);
                }
                break;
        }
    }
}

public class TowerCardData
{
    public int starCount;
    public int attackType;
    public int propertyType;
    public string name;

    public int towerIndex;

    public TowerCardData()
    {
        
    }
}
