using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerDisplay : MonoBehaviour
{
    public Vector2 offset;
    public GameObject pieceDisplayPrefab;

    private PieceDisplay _activePieceDisplay = null;
    private BoardDisplay _boardDisplay;

    public virtual void Setup(uint boardSize)
    {
        _boardDisplay = GetComponentInChildren<BoardDisplay>();
        _boardDisplay.Setup(boardSize);
        transform.position = offset;
    }

    public void SpawnPiece(Player player)
    {
        _activePieceDisplay = Instantiate(pieceDisplayPrefab, transform).GetComponent<PieceDisplay>();
        Vector3 spawnPoint = player.ActivePosition + (Vector2)_boardDisplay.transform.position;
        spawnPoint.z = -player.GetCurrentLevel();
        _activePieceDisplay.Setup(player.ActiveGamePiece, spawnPoint);
    }

    public void MovePiece(Vector3Int moveDir, int level) { _activePieceDisplay.Move(moveDir, level); }

    public void PlacePiece(int level)
    {
        _activePieceDisplay.Place(level);
        _activePieceDisplay = null;
    }

    public void RotatePieceClockwise(int level) { _activePieceDisplay.RotateClockwise(level); }

    public void RotatePieceCounterClockwise(int level) { _activePieceDisplay.RotateCounterClockwise(level); }
}
