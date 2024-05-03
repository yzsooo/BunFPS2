using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraRecoil : MonoBehaviour
{
    [SerializeField]
    Vector3 _compositeVector;

    public Vector3 CompositeVector
    {
        get { return _compositeVector; }
    }

    Vector3 _targetVector;

    float _recoilKickSeconds;
    float _recoilRecoverSeconds;
    float _recoilInterpolate;
    public float RecoilInterpolate
    {
        get { return _recoilInterpolate; }
    }
    private void Update()
    {
        CalculateCompositeVector();
    }

    // update camera's recoil vars and update the camera's target rotation vector
    public void AddRecoil(Vector3 direction, float kickSeconds, float recoverSeconds, float maxAngle)
    {
        // Make new target vector as the composite of current recoil and newly added recoil with set maximum angle
        _targetVector = _compositeVector + direction;
        _targetVector = Vector3.ClampMagnitude(_targetVector, maxAngle);

        _recoilKickSeconds = kickSeconds;
        _recoilRecoverSeconds = recoverSeconds;

        _recoilInterpolate = _recoilKickSeconds + _recoilRecoverSeconds;
    }

    // update the camera's composite vector depending on the recoilInterpolate value
    // >1 initial recoil
    // < 1 && > 0 recovering from the recoil
    // <= 0 finished recoil, reset target and composite vector
    void CalculateCompositeVector()
    {
        // initial recoil
        if (_recoilInterpolate > 1)
        {
            // rotate compositeVector until it reaches the target vector by lerp
            float kickSeconds = _recoilInterpolate - _recoilRecoverSeconds;
            _compositeVector = Vector3.Lerp(_targetVector, Vector3.zero, kickSeconds / _recoilKickSeconds);
            _recoilInterpolate -= Time.deltaTime;
        }
        // recovering from recoil
        else if (_recoilInterpolate < 1 && _recoilInterpolate > 0)
        {
            // rotate compositeVector until it reaches the zero position by lerp
            _compositeVector = Vector3.Lerp(Vector3.zero, _targetVector, _recoilInterpolate);
            _recoilInterpolate -= Time.deltaTime;
        }
        // finished recoil
        else if (_recoilInterpolate <= 0)
        {
            _targetVector = Vector3.zero;
            _compositeVector = Vector3.zero;
            _recoilInterpolate = 0;
        }
    }
}
