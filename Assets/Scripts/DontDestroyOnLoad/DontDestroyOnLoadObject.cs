using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // dont destroy this scene or its child objects
        DontDestroyOnLoad(gameObject);
    }

}
