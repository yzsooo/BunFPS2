using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    // Components that needs input
    private PlayerManager _pm;

    // vars for input map
    private PlayerInput _playerInput;
    private PlayerInput.MoveActions _moveActions;
    private PlayerInput.LookActions _lookActions;
    private PlayerInput.AttackActions _attackActions;

    void Awake()
    {
        InitializeInput();
        SetComponents();
        BindInputActions();
    }

    void InitializeInput()
    {
        _playerInput = new PlayerInput();
        _moveActions = _playerInput.Move;
        _lookActions = _playerInput.Look;
        _attackActions = _playerInput.Attack;
    }

    void SetComponents()
    {
        _pm = GetComponent<PlayerManager>();
    }

    void BindInputActions()
    {
        // move actions
        _moveActions.Jump.performed += ctx => _pm.movement.InputJump();

        // look actions
        _lookActions.LockMouse.performed += ctx => _pm.FlipMouseLock();

        // attack actions
        _attackActions.Attack1.started += ctx => _pm.attack.InputStartAttack1();
        _attackActions.Attack1.canceled += ctx => _pm.attack.InputStopAttack1();

    }

    private void Update()
    {
        ProcessInputActions();
    }

    void ProcessInputActions()
    {
        // move actions
        _pm.movement.InputMovementVector(_moveActions.Move.ReadValue<Vector2>());

        // look actions
        _pm.look.InputMouseLook(_lookActions.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        _moveActions.Enable();
        _lookActions.Enable();
        _attackActions.Enable();
    }

    private void OnDisable()
    {
        _moveActions.Disable();
        _lookActions.Disable();
        _attackActions.Disable();
    }
}
