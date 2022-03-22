using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed = 1;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        InputManager.MoveUpEvent.Add(() =>
            {
                animator.SetInteger("MoveType", 1);
                Move(0, 1);
            });
        InputManager.MoveRightEvent.Add(() =>
            {
                animator.SetInteger("MoveType", 2);
                Move(1, 0);
            });
        InputManager.MoveDownEvent.Add(() =>
            {
                animator.SetInteger("MoveType", 3);
                Move(0, -1);
            });
        InputManager.MoveLeftEvent.Add(() =>
            {
                animator.SetInteger("MoveType", 4);
                Move(-1, 0);
            });
        InputManager.StayEvent.Add(() =>
        {
            animator.SetInteger("MoveType", 0);
        });
    }

    void Update()
    {

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
}
