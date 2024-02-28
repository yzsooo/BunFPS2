using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    // Components
    public PlayerManager pm;
    Camera cam;
    public PlayerCameraRecoil CameraRecoil;

    // Sensitivity
    public float sensitivity = 1.0f;
    float _baseSensitivity = 20f;
    float Sensitivity
    {
        get { return _baseSensitivity * sensitivity; }
    }

    // Camera calculation vars
    Vector2 _mouseInput;
    Vector2 _rotationVector;
    Vector3 _intendedRotation;
    Vector3 _actualRotation;
    Vector3 _compositeRotation;
    float _compositeRotationInterpolate;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        CameraRecoil = GetComponent<PlayerCameraRecoil>();
    }

    public void InputMouseLook(Vector2 input)
    {
        _mouseInput = input;
    }

    public void Update()
    {
        if (!pm.MouseLock) { return; }
        CalculateRotationVector();
        CalculateActualRotation();
        CalculateRecoil();
        ApplyCameraRotation();
    }

    private void CalculateRotationVector()
    {
        // calculate up-down look
        _rotationVector.x -= (_mouseInput.y * Time.deltaTime) * Sensitivity;
        _rotationVector.x = Mathf.Clamp(_rotationVector.x, -80f, 80f);

        _intendedRotation = _rotationVector;
    }

    private void CalculateActualRotation()
    {
        _actualRotation = Vector3.Lerp(_intendedRotation, _intendedRotation + _compositeRotation, _compositeRotationInterpolate);
    }

    void CalculateRecoil()
    {
        // Add recoil to camera by adding the recoil vector into the intended camera look by lerp
        _actualRotation = Vector3.Lerp(_intendedRotation, _intendedRotation + CameraRecoil.CompositeVector, CameraRecoil.RecoilInterpolate);
    }

    private void ApplyCameraRotation()
    {
        // add left-right look by rotating parent transform
        pm.playerTransform.Rotate(Vector3.up * (_mouseInput.x * Time.deltaTime) * Sensitivity);
        cam.transform.localRotation = Quaternion.Euler(_actualRotation.x, _actualRotation.y, 0f);
    }

}
