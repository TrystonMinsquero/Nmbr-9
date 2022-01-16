using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerDisplay), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    private Player _player;
    private PlayerDisplay _display;

    private Controls _controls;

    private void Awake()
    {
        _controls = new Controls();

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
        int level = _player.Move(moveDir);
        // get if passed bounds check
        if(level >= 0)
            _display.MovePiece(new Vector3Int(moveDir.x, moveDir.y, 0), level);
    }

    public void Place(InputAction.CallbackContext ctx)
    {
        int level = _player.Place();
        if (level >= 0)
        {
            _display.PlacePiece(level);
        }
    }

    public void RotateClockwise(InputAction.CallbackContext ctx)
    {
        Debug.Log($"Rotated {name} Clockwise");
        _display.RotatePieceClockwise(_player.RotateClockwise());
    }
    
    public void RotateCounterClockwise(InputAction.CallbackContext ctx)
    {
        Debug.Log($"Rotated {name} Counter Clockwise");
        _display.RotatePieceCounterClockwise(_player.RotateCounterClockwise());
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
