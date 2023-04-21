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

    //행동 가능을 체크하는 함수 행동이 가능할 경우 false를 반환하며 isCanAct 변수를 false로 바꿔준다.
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

        //TODO 추후 랜덤으로 카드 인덱스를 받아 랜덤으로 카드를 생성하도록 한다.
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
        //카드를 별, 속성, 공격 타입에 맞추어 정렬한다
        //앞에서 부터 높은 별을 지닌 타워가 순서대로 보여지며 속성은 초록, 빨강, 파랑, 주황 순으로 정렬된다. 공격 타입<검=0, 활=1>
        cardList = cardList.OrderByDescending(x => x.towerData.starCount)
            .ThenByDescending(x => x.towerData.propertyType)
            .ThenBy(x => x.towerData.attackType).ToList();

        //우선 빈 카드슬롯이 있는지 확인한다.
        if (cardList.Count < 3 || cardList.Count > slotPage + 3)
        {
            //빈 카드슬롯이 있을 경우 왼쪽 순서부터 차례대로 카드를 배치시킨다.
            for (int i = 0; i < cardPosList.Length; i++)
            {
                //i와 slotPage를 합한 수가 카운트보다 작을 경우 배치할 수 있는 카드가 있다는 것
                if (i + slotPage < cardList.Count)
                {
                    //카드를 지정된 포지션으로 이동
                    cardList[i + slotPage].transform.position = cardPosList[i].position;
                }
                else
                {
                    break;
                }
            }
        }
    }

    //추후 자연스러운 카드의 움직임을 구현하여야함
    IEnumerator CardMovement()
    {
        while (true)
        {
            
        }
    }

    //선택한 카드의 데이터를 받아 적용시킨다.
    public void OnClickCard(TowerCard card)
    {
        GameManager.instance.PlayerSelectCard(card);
    }
}
