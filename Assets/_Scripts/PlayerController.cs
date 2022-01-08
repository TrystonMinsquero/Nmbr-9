using UnityEngine;

[RequireComponent(typeof(PlayerDisplay))]
public class PlayerController : MonoBehaviour
{
    private Player _player;
    private PlayerDisplay _display;

    public void Set(ref Player player)
    {
        _player = player;
        _display = GetComponent<PlayerDisplay>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (_player.ActiveGamePiece == null)
        {
            _display.pieceDisplay = null;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _player.ActiveGamePiece.RotateCounterClockwise();
            _display.pieceDisplay.RotateCounterClockwise();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _player.ActiveGamePiece.RotateClockwise();
            _display.pieceDisplay.RotateClockwise();
        }
        
        Vector2Int moveDir = Vector2Int.zero;
        if (Input.GetKeyDown(KeyCode.W))
            moveDir.y += 1;
        if (Input.GetKeyDown(KeyCode.A))
            moveDir.x -= 1;
        if (Input.GetKeyDown(KeyCode.S))
            moveDir.y -= 1;
        if (Input.GetKeyDown(KeyCode.D))
            moveDir.x += 1;

        if (moveDir != Vector2Int.zero)
        {
            _player.Move(moveDir);
            _display.pieceDisplay.Move(moveDir);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _player.Place();
            
        }
    }
}
