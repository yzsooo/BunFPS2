using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartScene : DebugUISubmenuButton
{
    public override void UseButton()
    {
        GameSceneManager.GameSceneManagerInstance.RestartScene();
    }
}
