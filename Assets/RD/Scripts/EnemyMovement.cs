using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0;
    private Vector3 moveDir = Vector3.zero;

    public float MoveSpeed => moveSpeed;

    void FixedUpdate()
    {
        transform.position += moveDir * moveSpeed * Time.fixedDeltaTime;
    }

    public void MoveTo(Vector3 d)
    {
        moveDir = d;
    }
}
