using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity
{
    
    // player lose
    // call gameovermanager's gameover function
    public override void OutOfHealth()
    {
        Debug.Log(EntityName + " is dead");
        GameSceneManager.GameSceneManagerInstance.GameoverManager.GameOver();
    }
}
