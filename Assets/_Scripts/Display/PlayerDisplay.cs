using UnityEngine;

public class PlayerDisplay : MonoBehaviour
{
    public Vector2 offset;
    public GameObject boardDisplayPrefab;
    public GameObject pieceDisplayPrefab;

    [HideInInspector]
    public PieceDisplay pieceDisplay = null;

    private void Awake()
    {
        BoardDisplay boardDisplay = Instantiate(boardDisplayPrefab, transform).GetComponent<BoardDisplay>();
        boardDisplay.Setup(offset, GameManager.instance.boardSize);
    }

    public void SpawnPiece(Player player)
    {
        pieceDisplay = Instantiate(pieceDisplayPrefab).GetComponent<PieceDisplay>();
        pieceDisplay.Setup(player.ActiveGamePiece, player.ActivePosition + offset);
    }

}
