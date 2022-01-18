using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerDisplay : MonoBehaviour
{
    public Vector2 offset;
    public GameObject pieceDisplayPrefab;

    private PieceDisplay _activePieceDisplay = null;
    private BoardDisplay _boardDisplay;

    private Player _player;

    public virtual void Setup(ref Player player)
    {
        _player = player;
        
        _boardDisplay = GetComponentInChildren<BoardDisplay>();
        _boardDisplay.Setup(_player.Board.size);
        transform.position = offset;

        _player.OnMove += MovePiece;
        _player.OnPlace += PlacePiece;
        _player.OnRotateClockwise += RotatePieceClockwise;
        _player.OnRotateCounterClockwise += RotatePieceCounterClockwise;
    }

    public void SpawnPiece(Player player)
    {
        _activePieceDisplay = Instantiate(pieceDisplayPrefab, transform).GetComponent<PieceDisplay>();
        Vector3 spawnPoint = player.ActivePosition + (Vector2)_boardDisplay.transform.position;
        spawnPoint.z = player.GetCurrentLevel();
        _activePieceDisplay.Setup(player.ActiveGamePiece, spawnPoint);
    }

    public void MovePiece(Vector2Int moveDir, int level)
    {
        if(level >= 0)
            _activePieceDisplay.Move(moveDir, level);
        else
        {
            // print bad message
        }
    }

    public void PlacePiece(int level)
    {
        if (level >= 0)
        {
            _activePieceDisplay.Place(level);
            _activePieceDisplay = null;
        }
        else
        {
            // print bad message
        }
    }

    public void RotatePieceClockwise(int level)
    {
        if (level >= 0)
            _activePieceDisplay.RotateClockwise(level);
        else
        {
            // print bad message
        }
    }

    public void RotatePieceCounterClockwise(int level)
    {
        if(level >= 0)
            _activePieceDisplay.RotateCounterClockwise(level);
        else
        {
            // print bad message
        }
    }
}
