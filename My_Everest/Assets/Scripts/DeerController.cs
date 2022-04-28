using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerController : BearController
{
    protected void Start()
    {
        base.Start();
    }

    void Update()
    {
        var playerDist = getDistance(player.transform.position);
        if (playerDist > PlayerDistance)
        {
            timer += Time.deltaTime;
            if (timer > TimeSolution)
            {
                timer = 0;
                MoveDirection = GetMoveDirection();
            }
        }
        else
        {
            var vect = transform.position - player.transform.position;
            MoveDirection = new Vector2(calcDir(vect.x), calcDir(vect.y));
        }
        Move(MoveDirection.x, MoveDirection.y);
    }
}
