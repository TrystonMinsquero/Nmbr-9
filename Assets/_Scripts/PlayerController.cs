using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerDisplay), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    private Player _player;
    private PlayerDisplay _display;
    private PlayerInput _playerInput;

    private Controls _controls;

    private void Awake()
    {
        _controls = new Controls();
        _playerInput = GetComponent<PlayerInput>();

        _controls.Gameplay.Move.started += Move;
        _controls.Gameplay.PlacePiece.started += Place;
        _controls.Gameplay.RotateClockwise.started += RotateClockwise;
        _controls.Gameplay.RotateCounterClockwise.started += RotateCounterClockwise;
    }

    public void Set(ref Player player)
    {
        _player = player;
        _display = GetComponent<PlayerDisplay>();
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        Vector2 input = ctx.ReadValue<Vector2>();
        Vector2Int moveDir = new Vector2Int((int) input.x, (int) input.y);
        bool moved = _player.Move(moveDir);
        // get if passed bounds check
        if(moved)
            _display.activePieceDisplay?.Move(moveDir);
    }

    public void Place(InputAction.CallbackContext ctx)
    {
        int level = _player.Place();
        if (level >= 0)
        {
            _display.activePieceDisplay.Place(level);
            _display.activePieceDisplay = null;
        }
    }

    public void RotateClockwise(InputAction.CallbackContext ctx)
    {
        Debug.Log($"Rotated {name} Clockwise");
        _player.RotateClockwise();
        _display.activePieceDisplay.RotateClockwise();
        _player.DebugDisplayWithActivePiece();
    }
    
    public void RotateCounterClockwise(InputAction.CallbackContext ctx)
    {
        Debug.Log($"Rotated {name} Counter Clockwise");
        _player.RotateCounterClockwise();
        _display.activePieceDisplay.RotateCounterClockwise();
        _player.DebugDisplayWithActivePiece();
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }
}
