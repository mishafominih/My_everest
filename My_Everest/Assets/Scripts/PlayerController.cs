using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : BaseController
{
    public FixedJoystick Joystick;

    void Update()
    {
        var horizontal = Joystick.Horizontal;
        var vertical = Joystick.Vertical;
        if(horizontal != 0 || vertical != 0)
        {
            GetComponent<Energy>().ChangeEnergy(UpgradeInfo.WalkLostEnergy);
        }
        Move(horizontal, vertical);
    }
}
