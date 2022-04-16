using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEnergy : Energy
{
    public override void Death()
    {
        base.Death();
        SceneManager.LoadScene(0);
    }
}
