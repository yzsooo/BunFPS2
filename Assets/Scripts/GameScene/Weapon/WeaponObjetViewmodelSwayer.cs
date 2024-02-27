using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponObjectViewmodelSwayer : MonoBehaviour
{
    [SerializeField]
    Transform _viewmodelRoot;
    [SerializeField]
    Vector3 _defaultViewmodelLocalPosition;

    public float SpeedMultiplier = 1;

    [SerializeField]
    Vector2 _mouseInput;
    [SerializeField]
    Vector2 _movementInput;

    [Header("Mouse Rotation")]
    public float RotationAmount;
    Vector3 _rotationVector;

    [Header("Movement Offset")]
    public float OffsetAmount;
    [SerializeField]
    Vector3 _offsetVector;

    [Header("Movement Bobbing")]
    public float BobAmount;
    public float BobSpeed;
    float _bobSpeedCurve;
    [SerializeField]
    Vector3 _bobVector;

    private void Awake()
    {
        //_viewmodelRoot = transform;
        _defaultViewmodelLocalPosition = _viewmodelRoot.position;
    }

    private void Update()
    {
        // calculate Mouse rotation, Movement offset and movement bobbing
        CalculateMouseRotation();
        CalculateMovementOffset();
        CalculateMovementBobbing();
        // combine the 3 vectors and apply it to the viewmodel root
        CompositeViewmodelSwaying();
    }

    public void UpdateMouseVector(Vector2 input)
    {
        _mouseInput = input;
    }
    public void UpdateMovementVector(Vector2 input)
    {
        _movementInput = input;
    }

    private void CalculateMouseRotation()
    {
        Vector2 rot = _mouseInput * RotationAmount;
        _rotationVector = new Vector3(-rot.y, rot.x, 0.0f);
    }

    private void CalculateMovementOffset()
    {
        float offsetX = _movementInput.x * OffsetAmount;
        float offsetY = _movementInput.y * OffsetAmount;


        _offsetVector = _defaultViewmodelLocalPosition;
        _offsetVector.x += offsetX;
        _offsetVector.z += offsetY;
    }

    private void CalculateMovementBobbing()
    {
        // applly bobbing when moving
        if (_movementInput.magnitude > 0)
        {
            _bobSpeedCurve += Time.deltaTime * BobSpeed;
            // Mathf.Abs and Mathf.Sin will return a number curve between 1 and -1 to simulate bobbing effect
            _bobVector.y = Mathf.Abs(Mathf.Sin(_bobSpeedCurve) * BobAmount);
        }

        // reduce bobbing until its 0 when not moving
        if (_movementInput.magnitude == 0)
        {
            _bobVector.y -= Time.deltaTime * BobSpeed;
            _bobVector.y = Mathf.Clamp(_bobVector.y, 0, _bobVector.y);
        }
    }

    private void CompositeViewmodelSwaying()
    {
        // Swaying the position of the viewmodel
        //_viewmodelRoot.localPosition = Vector3.Lerp(
        //    _viewmodelRoot.localPosition,
        //    _offsetVector + _bobVector,
        //    SpeedMultiplier * Time.deltaTime
        //    );
        _viewmodelRoot.localPosition = Vector3.Lerp(
            _viewmodelRoot.localPosition, _offsetVector + _bobVector, SpeedMultiplier * Time.deltaTime
            );
        // Swaying the rotation of the viewmodel
        _viewmodelRoot.localRotation = Quaternion.Slerp(
            _viewmodelRoot.localRotation,
            Quaternion.Euler(_rotationVector),
            SpeedMultiplier * Time.deltaTime
            );
    }
}
