using UnityEngine;

public class PlayerDisplay : MonoBehaviour
{
    public Vector2 offset;
    public GameObject boardDisplayPrefab;
    public GameObject pieceDisplayPrefab;

    [HideInInspector]
    public PieceDisplay pieceDisplay = null;
    public BoardDisplay boardDisplay;

    private void Awake()
    {
        boardDisplay = Instantiate(boardDisplayPrefab, transform).GetComponent<BoardDisplay>();
        boardDisplay.Setup(offset, GameManager.instance.boardSize);
    }

    public void SpawnPiece(Player player)
    {
        pieceDisplay = Instantiate(pieceDisplayPrefab, transform).GetComponent<PieceDisplay>();
        Vector3 spawnPoint = player.ActivePosition + (Vector2)boardDisplay.transform.position;
        pieceDisplay.Setup(player.ActiveGamePiece, spawnPoint);
    }
}
