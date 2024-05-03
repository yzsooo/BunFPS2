using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartScene : DebugUISubmenuButton
{
    // reload the scene by calling the sceneloader singleton
    public override void UseButton()
    {
        SceneLoader.SceneLoaderInstance.RestartScene();
    }
}
