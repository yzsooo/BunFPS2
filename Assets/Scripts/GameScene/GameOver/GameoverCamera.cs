using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameoverCamera : MonoBehaviour
{
    public Transform cameraAnchor;
    public float cameraTurnSpeed;

    private void Update()
    {
        cameraAnchor.Rotate(Vector3.up * cameraTurnSpeed * Time.deltaTime);
    }
}
