using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Transform cardPrevPos;
    [SerializeField]
    private Transform cardNextPos;
    [SerializeField]
    private Transform[] cardPosList;

    [SerializeField]
    private List<TowerCard> cardList;

    [SerializeField]
    private Transform cardParent;

    int slotPage = 0;

    private TowerCard selectedTowerCard;

    [SerializeField]
    private bool isCanAct = true;

    void Start()
    {
        
    }

    private void Update()
    {
        
    }

    //�ൿ ������ üũ�ϴ� �Լ� �ൿ�� ������ ��� false�� ��ȯ�ϸ� isCanAct ������ false�� �ٲ��ش�.
    public bool CheckAct()
    {
        if (isCanAct)
        {
            isCanAct = false;
            return false;
        }
        else
        {
            return true;
        }
    }

    public void Spawn()
    {
        MakeTowerCard();
    }

    void MakeTowerCard()
    {
        if (CheckAct())
        {
            return;
        }

        //TODO ���� �������� ī�� �ε����� �޾� �������� ī�带 �����ϵ��� �Ѵ�.
        int i = 0;
        TowerCardData d = new TowerCardData();

        TowerCard c = GameManager.instance.prefabs.GetCard(cardParent).GetComponent<TowerCard>();
        c.transform.position = cardNextPos.position;
        c.Setup(d);

        cardList.Add(c);

        SortCardSlot();

        isCanAct = true;
    }

    void SortCardSlot()
    {
        //ī�带 ��, �Ӽ�, ���� Ÿ�Կ� ���߾� �����Ѵ�
        //�տ��� ���� ���� ���� ���� Ÿ���� ������� �������� �Ӽ��� �ʷ�, ����, �Ķ�, ��Ȳ ������ ���ĵȴ�. ���� Ÿ��<��=0, Ȱ=1>
        cardList = cardList.OrderByDescending(x => x.towerData.starCount)
            .ThenByDescending(x => x.towerData.propertyType)
            .ThenBy(x => x.towerData.attackType).ToList();

        //�켱 �� ī�彽���� �ִ��� Ȯ���Ѵ�.
        if (cardList.Count < 3 || cardList.Count > slotPage + 3)
        {
            //�� ī�彽���� ���� ��� ���� �������� ���ʴ�� ī�带 ��ġ��Ų��.
            for (int i = 0; i < cardPosList.Length; i++)
            {
                //i�� slotPage�� ���� ���� ī��Ʈ���� ���� ��� ��ġ�� �� �ִ� ī�尡 �ִٴ� ��
                if (i + slotPage < cardList.Count)
                {
                    //ī�带 ������ ���������� �̵�
                    cardList[i + slotPage].transform.position = cardPosList[i].position;
                }
                else
                {
                    break;
                }
            }
        }
    }

    //���� �ڿ������� ī���� �������� �����Ͽ�����
    IEnumerator CardMovement()
    {
        while (true)
        {
            
        }
    }

    //������ ī���� �����͸� �޾� �����Ų��.
    public void OnClickCard(TowerCard card)
    {
        GameManager.instance.PlayerSelectCard(card);
    }
}
