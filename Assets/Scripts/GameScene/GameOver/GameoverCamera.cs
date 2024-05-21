using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameoverCamera : MonoBehaviour
{
    public Transform cameraAnchor;
    public float cameraTurnSpeed;

    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        // rotate around the player's position overtime
        cameraAnchor.Rotate(Vector3.up * cameraTurnSpeed * Time.deltaTime);
    }
}
