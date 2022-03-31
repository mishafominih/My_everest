using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    public float MoveSpeed = 1;

    private Animator animator;
    
    protected void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected void Move(float x, float y)
    {
        var deltaTime = Time.deltaTime;
        transform.position += new Vector3(
            x * deltaTime * MoveSpeed,
            y * deltaTime * MoveSpeed,
            0
        );
        animator.SetInteger("MoveType", GetMoveAnimation(x, y));
    }

    protected int GetMoveAnimation(float x, float y)
    {
        if (x == 0 && y == 0)
            return 0;
        if (Mathf.Abs(x) > Mathf.Abs(y))
            return x > 0 ? 2 : 4;
        else
            return y > 0 ? 1 : 3;
    }
}
