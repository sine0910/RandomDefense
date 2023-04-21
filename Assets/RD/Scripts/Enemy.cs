using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemySpawner enemySpawner;

    public SpriteRenderer spriter;

    private int wayPointCount;
    private Transform[] wayPoints;
    private int currentIndex = 0;

    private SortingLayer layer;

    public float moveSpeed;
    public Vector3 moveDir = Vector3.zero;

    public float disToGoal;

    [SerializeField]
    private float healthPoint;
    private int attackPoint;

    bool dead = false;

    public void Awake()
    {
        spriter = GetComponent<SpriteRenderer>();
    }

    public void Setup(EnemySpawner e, EnemyData d, Transform[] w)
    {
        enemySpawner = e;

        healthPoint = d.health;
        attackPoint = d.attack;
        moveSpeed = d.speed;

        wayPointCount = w.Length;
        wayPoints = new Transform[wayPointCount];
        wayPoints = w;

        transform.position = wayPoints[currentIndex].position;

        StartCoroutine(OnMove());
    }

    IEnumerator OnMove()
    {
        NextMoveTo();

        while (!dead)
        {
            transform.Translate(moveDir * moveSpeed * Time.fixedDeltaTime);

            disToGoal = Vector3.Distance(transform.position, wayPoints[wayPointCount - 1].position);

            if (Vector3.Distance(transform.position, wayPoints[currentIndex].position) <= 0.1f)
            {
                NextMoveTo();
            }
            else if (transform.position.x < wayPoints[currentIndex].position.x)
            {
                spriter.flipX = false;
            }
            else if (transform.position.x > wayPoints[currentIndex].position.x)
            {
                spriter.flipX = true;
            }

            yield return new WaitForFixedUpdate();
        }
    }

    private void NextMoveTo()
    {
        if (currentIndex < wayPointCount - 1)
        {
            transform.position = wayPoints[currentIndex].position;

            currentIndex++;

            moveDir = (wayPoints[currentIndex].position - transform.position).normalized;
        }
        else
        {
            GameManager.instance.DiscountHeart(attackPoint);
            Death();
        }
    }

    public void Hit(float d)
    {
        Debug.Log("Enemy Hit: " + d);

        healthPoint -= d;

        if (healthPoint < 0)
        {
            Death();
        }
    }

    public void Death()
    {
        dead = true;
        gameObject.SetActive(false);
        enemySpawner.DestroyEnemy(this);
    }
}
