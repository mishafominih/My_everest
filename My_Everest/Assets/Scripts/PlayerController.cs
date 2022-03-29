using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public FixedJoystick Joystick;
    public float MoveSpeed = 1;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        var horizontal = Joystick.Horizontal;
        var vertical = Joystick.Vertical;
        Move(horizontal, vertical);
        animator.SetInteger("MoveType", GetMoveAnimation(horizontal, vertical));
    }

    private void Move(float x, float y)
    {
        var deltaTime = Time.deltaTime;
        transform.position += new Vector3(
            x * deltaTime * MoveSpeed,
            y * deltaTime * MoveSpeed,
            0
        );
    }

    private int GetMoveAnimation(float x, float y)
    {
        if (x == 0 && y == 0)
            return 0;
        if (Mathf.Abs(x) > Mathf.Abs(y))
            return x > 0 ? 2 : 4;
        else
            return y > 0 ? 1 : 3;
    }
}
