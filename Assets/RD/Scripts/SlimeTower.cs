using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ATTACK_MODE
{
    NEAREST_TO_GOAL,
    NEAREST_TO_TOWER,
    FARTHEST
}

public class SlimeTower : MonoBehaviour
{
    [SerializeField]
    LayerMask[] targetLayer;

    [SerializeField]
    private Scanner scanner;

    [SerializeField]
    private float attackRange = 3.0f;
    private float attackRate = 1.0f;
    private float attackPoint = 1.0f;
    private int attackCount = 1;

    private float timer;

    public bool on;

    private void Start()
    {
        on = true;
        StartCoroutine(AttackAct());

        scanner.Init(this, attackRange, attackCount, targetLayer);
    }

    IEnumerator AttackAct()
    {
        while (on)
        {
            if (scanner.target != null && scanner.target.Count > 0)
            {
                List<Enemy> targetList = scanner.target;

                for (int i = 0; i < attackCount; i++)
                {
                    if (targetList.Count > i)
                    {
                        targetList[i].Hit(attackPoint);
                    }
                    else
                    {
                        break;
                    }
                }
                yield return new WaitForSeconds(attackRate);
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
