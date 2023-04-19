using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public SpriteRenderer spriter;

    private int wayPointCount;
    private Transform[] wayPoints;
    private int currentIndex = 0;

    public float moveSpeed;
    public Vector3 moveDir = Vector3.zero; 

    bool dead = false;

    public void Awake()
    {
        spriter = GetComponent<SpriteRenderer>();
    }

    public void Setup(Transform[] w)
    {
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
            Death();
        }
    }

    private void Death()
    {
        dead = true;
        gameObject.SetActive(false);
    }
}
