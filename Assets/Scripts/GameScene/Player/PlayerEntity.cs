using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity
{
    //public override void Awake()
    //{
    //    base.Awake();
    //}

    public override void OutOfHealth()
    {
        Debug.Log(EntityName + " is dead");
        GameSceneManager.GameSceneManagerInstance.GameoverManager.GameOver();
    }
}
