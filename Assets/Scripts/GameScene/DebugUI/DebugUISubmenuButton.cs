using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugUISubmenuButton : MonoBehaviour
{
    // Function to be called when the button is pressed in game
    // to be overridden for each button script
    public virtual void UseButton()
    {
        Debug.Log(name);
    }
}
