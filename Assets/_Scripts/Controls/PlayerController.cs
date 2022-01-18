using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerDisplay), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    private Player _player;
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
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        Vector2 input = ctx.ReadValue<Vector2>();
        Vector2Int moveDir = new Vector2Int((int) input.x, (int) input.y);
        _player.Move(moveDir);

    }

    public void Place(InputAction.CallbackContext ctx)
    {
        _player.Place();
    }

    public void RotateClockwise(InputAction.CallbackContext ctx)
    {
        //Debug.Log($"Rotated {name} Clockwise");
        
        _player.RotateClockwise();
    }
    
    public void RotateCounterClockwise(InputAction.CallbackContext ctx)
    {
        //Debug.Log($"Rotated {name} Counter Clockwise");
        
        _player.RotateCounterClockwise();
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
