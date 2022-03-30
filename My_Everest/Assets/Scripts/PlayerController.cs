using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : BaseController
{
    public FixedJoystick Joystick;

    void Update()
    {
        var horizontal = Joystick.Horizontal;
        var vertical = Joystick.Vertical;
        Move(horizontal, vertical);
    }
}
