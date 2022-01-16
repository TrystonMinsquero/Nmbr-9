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
        ActiveGamePiece = new GamePiece(piece);
        // Check for piece size
        if (_board.size < piece.Height || _board.size < piece.Width)
            throw new Exception($"Game board is not big enough for the {piece.Height} by {piece.Width} piece");
        Vector2Int mid = Vector2Int.one * (int) (_board.size/ 2);
        Vector2Int offset = new Vector2Int(-Mathf.CeilToInt(ActiveGamePiece.Width/ 2f), Mathf.CeilToInt(ActiveGamePiece.Height / 2f));
        ActivePosition = mid;
    }

    // Trys to place the active piece on the board at the active position,
    // returns the level of the piece place if placed, otherwise returns -1
    public int Place()
    {
        int level = _board.TryPlacePiece(ActiveGamePiece, ActivePosition);
        
        if (level >= 0)
        {
            //Debug.Log("Placed " + ActiveGamePiece.Value + " at " + ActivePosition);
            ActiveGamePiece = null;
            return level;
        }

        return -1;

    }

    // moves the active piece on the board in the direction, returns the level of the piece if moved,
    // otherwise -1 if there is piece would be out of bounds
    public int Move(Vector2Int direction)
    {
        Vector2Int newPosition = ActivePosition + direction;

        if (_board.IsInBounds(ActiveGamePiece, newPosition))
        {
            ActivePosition = newPosition;
            // Debug.Log("Moved " + ActiveGamePiece.Value + " to " + ActivePosition);
            return _board.GetLevelFromPosition(ActiveGamePiece, ActivePosition);;
        }
        
        // Its out of bounds
        Debug.LogWarning("Cannot place piece out of bounds");
        return -1;

    }
    
    // rotates the activePiece clockwise, returning what the new level it would be placed
    public int RotateClockwise()
    {
        ActiveGamePiece.RotateClockwise();
        return GetCurrentLevel();
    }
    
    // rotates the activePiece counter clockwise, returning what the new level it would be placed
    public int RotateCounterClockwise()
    {
        ActiveGamePiece.RotateCounterClockwise();
        return GetCurrentLevel();
    }

    public int CalculateScore()
    {
        int total = 0;
        for (int i = 0; i < _board.levels.Count; i++)
            total += _board.levels[i].GetTotalScore() * i;
        return total;
    }

    public int GetCurrentLevel()
    {
        return _board.GetLevelFromPosition(ActiveGamePiece, ActivePosition);
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
