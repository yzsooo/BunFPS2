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

    public void AddRecoil(Vector3 direction, float kickSeconds, float recoverSeconds, float maxAngle)
    {
        // Make new target vector as the composite of current recoil and newly added recoil with set maximum angle
        _targetVector = _compositeVector + direction;
        _targetVector = Vector3.ClampMagnitude(_targetVector, maxAngle);

        _recoilKickSeconds = kickSeconds;
        _recoilRecoverSeconds = recoverSeconds;

        _recoilInterpolate = _recoilKickSeconds + _recoilRecoverSeconds;
    }

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
