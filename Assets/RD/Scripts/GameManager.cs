using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ACT
{
    NONE,
    BUILD,
    MERGE,
    SALE
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public CameraManager cameram;
    public DataManager data;
    public PrefabManager prefabs;
    public UIManager UI;
    public EnemySpawner enemy;

    [SerializeField]
    SpawnPoint[] spawnPointsList;

    [SerializeField]
    private int Heart;
    private int Gold;

    private List<TowerCard> playerSelectCards = new List<TowerCard>();

    private ACT actStatus;

    void Awake()
    {
        instance = this;

        InitGame();
    }

    //게임 플레이를 위한 모든 데이터와 변수들을 설정한다.
    public void InitGame()
    {
        Heart = 20;
        Gold = 10;

        playerSelectCards.Clear();
    }

    public void PlayerSelectCard(TowerCard card)
    {
        switch (actStatus)
        {
            case ACT.NONE:
                {
                    actStatus = ACT.BUILD;

                    SetSpawnPointPointer(true);

                    playerSelectCards.Add(card);
                    //플레이어가 선택한 카드에 효과를 추가한다
                }
                break;

            case ACT.BUILD:
                {
                    //플레이어가 기존에 선택한 카드의 효과를 지우고 새로운 카드에 효과를 추가한다
                }
                break;

            case ACT.MERGE:
                {
                    //플레이어가 기존에 선택한 카드의 효과를 지우고 새로운 카드에 효과를 추가한다
                }
                break;

            case ACT.SALE:
                {
                    //플레이어가 기존에 선택한 카드의 효과를 지우고 새로운 카드에 효과를 추가한다
                }
                break;
        }
    }

    //빈 타워 슬롯에 선택을 위한 표시를 띄워준다.
    public void OnBuildTower(TowerCard card)
    {

    }

    public void CompleteBuildTower()
    {
        //타워 설치에 관한 변수들을 초기화
        playerSelectCards.Clear();
        actStatus = ACT.NONE;

        SetSpawnPointPointer(false);
    }

    public void OnClickTowerSpawner(SpawnPoint p)
    {
        if (playerSelectCards.Count == 0 || actStatus != ACT.BUILD)
        {
            return;
        }

        //배치 공간을 받아와서 그곳으로 슬라임 타워를 배치한다.
        int i = playerSelectCards[0].towerData.towerIndex;
        SlimeTower tower = prefabs.GetTower(i).GetComponent<SlimeTower>();
        p.SetTower(tower);

        playerSelectCards[0].SetSpawnPos(p);
        //추후 스폰포인트의 레이어에 따라 공중 공격에 대한 판정을 적용시킨다.

        CompleteBuildTower();
    }

    public void SetSpawnPointPointer(bool b)
    {
        foreach (SpawnPoint s in spawnPointsList)
        {
            s.SetActive(b);
        }
    }

    public void DiscountHeart(int c)
    {
        Heart -= c;

        if (Heart < 1)
        {
            //게임 오버
            Debug.Log("GameOver");
        }
    }

    public bool SpendGold(int g)
    {
        if (Gold < g)
        {
            Debug.Log("돈이 부족하다요");
            return false;
        }
        else
        {
            Debug.Log("Used Gold " + g + "\n Remain Gold " + (Gold - g));
            Gold -= g;
            return true;
        }
    }

    public bool IsCanAct(ACT a)
    {
        return actStatus == a;
    }

    public ACT GetAct()
    {
        return actStatus;
    }
}
