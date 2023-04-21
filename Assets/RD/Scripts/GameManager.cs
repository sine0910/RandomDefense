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

    //���� �÷��̸� ���� ��� �����Ϳ� �������� �����Ѵ�.
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
                    //�÷��̾ ������ ī�忡 ȿ���� �߰��Ѵ�
                }
                break;

            case ACT.BUILD:
                {
                    //�÷��̾ ������ ������ ī���� ȿ���� ����� ���ο� ī�忡 ȿ���� �߰��Ѵ�
                }
                break;

            case ACT.MERGE:
                {
                    //�÷��̾ ������ ������ ī���� ȿ���� ����� ���ο� ī�忡 ȿ���� �߰��Ѵ�
                }
                break;

            case ACT.SALE:
                {
                    //�÷��̾ ������ ������ ī���� ȿ���� ����� ���ο� ī�忡 ȿ���� �߰��Ѵ�
                }
                break;
        }
    }

    //�� Ÿ�� ���Կ� ������ ���� ǥ�ø� ����ش�.
    public void OnBuildTower(TowerCard card)
    {

    }

    public void CompleteBuildTower()
    {
        //Ÿ�� ��ġ�� ���� �������� �ʱ�ȭ
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

        //��ġ ������ �޾ƿͼ� �װ����� ������ Ÿ���� ��ġ�Ѵ�.
        int i = playerSelectCards[0].towerData.towerIndex;
        SlimeTower tower = prefabs.GetTower(i).GetComponent<SlimeTower>();
        p.SetTower(tower);

        playerSelectCards[0].SetSpawnPos(p);
        //���� ��������Ʈ�� ���̾ ���� ���� ���ݿ� ���� ������ �����Ų��.

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
            //���� ����
            Debug.Log("GameOver");
        }
    }

    public bool SpendGold(int g)
    {
        if (Gold < g)
        {
            Debug.Log("���� �����ϴٿ�");
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
