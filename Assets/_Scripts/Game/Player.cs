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
        Vector2Int mid = Vector2Int.one * (int) (_board.size / 2);
        Vector2Int offset = new Vector2Int((int) -ActiveGamePiece.Width / 2, (int) ActiveGamePiece.Height / 2);
        ActivePosition = Vector2Int.zero;//mid + offset;
    }

    public void Place()
    {
        if (_board.TryPlacePiece(ActiveGamePiece, ActivePosition))
        {
            Debug.Log("Placed " + ActiveGamePiece.Value + " at " + ActivePosition);
            //Display();
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

    public void RotateCounterClockwise()
    {
        ActiveGamePiece.RotateCounterClockwise();
        ActivePosition += GetOffsetAfterRotate(ActiveGamePiece.Height, ActiveGamePiece.Width);

    }
    
    private Vector2Int GetOffsetAfterRotate(uint height, uint width)
    {
        Vector2Int offset = Vector2Int.zero;
        if (height == width)
            return offset;
        // This formula doesn't really work for larger matrices
        float goldenRatio = (1 + Mathf.Sqrt(5)) / 2;
        if ((float) Math.Max(height, width) / Math.Min(height, width) > goldenRatio)
            offset.x += 1;
        if(height != width)
            offset.y += 1;
        return height > width ? offset : -offset;
    }
    
    public void Display()
    {
        for (int i = 0; i < _board.levels.Count - 1; i++)
        {
            Debug.Log("Level " + i + ":");
            _board.levels[i].Display();
        }
    }
}
