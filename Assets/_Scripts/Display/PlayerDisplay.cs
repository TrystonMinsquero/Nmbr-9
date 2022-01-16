using System;
using UnityEngine;

public class PlayerDisplay : MonoBehaviour
{
    public Vector2 offset;
    public GameObject pieceDisplayPrefab;

    [HideInInspector]
    public PieceDisplay activePieceDisplay = null;
    public BoardDisplay boardDisplay;

    public void Setup(uint boardSize)
    {
        boardDisplay = GetComponentInChildren<BoardDisplay>();
        boardDisplay.Setup(offset, boardSize);
        transform.position = offset;
    }

    public void SpawnPiece(Player player)
    {
        activePieceDisplay = Instantiate(pieceDisplayPrefab, transform).GetComponent<PieceDisplay>();
        Vector3 spawnPoint = player.ActivePosition + (Vector2)boardDisplay.transform.position;
        activePieceDisplay.Setup(player.ActiveGamePiece, spawnPoint);
    }
    
}
