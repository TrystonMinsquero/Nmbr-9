using System;
using UnityEngine;

[Serializable]
public class Player
{
    private Board _board;
    public GamePiece ActiveGamePiece { get; private set; }
    public Vector2Int ActivePosition { get; private set; }

    public Player(uint boardSize)
    {
        _board = new Board(boardSize);
        int mid = (int)(boardSize / 2) ;
        ActivePosition = Vector2Int.one * mid;
    }
    
    public void StartTurn(GamePiece piece)
    {
        ActiveGamePiece = piece;
        // Check for piece size
        if (GameManager.instance.boardSize < piece.Height || GameManager.instance.boardSize < piece.Width)
            throw new Exception($"Game board is not big enough for the {piece.Height} by {piece.Width} piece");
        Vector2Int mid = Vector2Int.one * (int) (GameManager.instance.boardSize/ 2);
        Vector2Int offset = new Vector2Int(-Mathf.CeilToInt(ActiveGamePiece.Width/ 2f), Mathf.CeilToInt(ActiveGamePiece.Height / 2f));
        ActivePosition = mid;
    }

    public void Place()
    {
        if (_board.TryPlacePiece(ActiveGamePiece, ActivePosition))
        {
            //Debug.Log("Placed " + ActiveGamePiece.Value + " at " + ActivePosition);
            ActiveGamePiece = null;
        } 
        // else display message
        
    }

    public void Move(Vector2Int direction)
    {
        Vector2Int newPosition = ActivePosition + direction;
        
        // Bounds check

        ActivePosition = newPosition;
        
        Debug.Log("Moved " + ActiveGamePiece.Value + " to " + ActivePosition);
        
    }

    public int CalculateScore()
    {
        int total = 0;
        for (int i = 0; i < _board.levels.Count; i++)
            total += _board.levels[i].GetTotalScore() * i;
        return total;
    }

    public void DebugDisplay()
    {
        for (int i = 0; i < _board.levels.Count - 1; i++)
        {
            Debug.Log("Level " + i + ":");
            _board.levels[i].Display();
        }
    }

    public void DebugDisplayWithActivePiece()
    {
        _board.DisplayWithPiece(ActiveGamePiece, ActivePosition);
    }
    
    public void DebugDisplayScore(string name)
    {
        Debug.Log($"{name}'s total score was {CalculateScore()}");
    }
}
