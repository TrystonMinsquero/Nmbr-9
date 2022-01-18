using System;
using UnityEngine;
using UnityEngine.EventSystems;

[Serializable]
public class Player
{
    public Board Board { get; }
    public GamePiece ActiveGamePiece { get; private set; }
    public Vector2Int ActivePosition { get; private set; }

    public event Action<Vector2Int, int> OnMove;
    public event Action<int> OnPlace;
    public event Action<int> OnRotateClockwise;
    public event Action<int> OnRotateCounterClockwise;

    public Player(uint boardSize)
    {
        Board = new Board(boardSize);
        int mid = (int)(boardSize / 2) ;
        ActivePosition = Vector2Int.one * mid;
    }
    
    public void StartTurn(GamePiece piece)
    {
        ActiveGamePiece = new GamePiece(piece);
        // Check for piece size
        if (Board.size < piece.Height || Board.size < piece.Width)
            throw new Exception($"Game board is not big enough for the {piece.Height} by {piece.Width} piece");
        Vector2Int mid = Vector2Int.one * (int) (Board.size/ 2);
        Vector2Int offset = new Vector2Int(-Mathf.CeilToInt(ActiveGamePiece.Width/ 2f), Mathf.CeilToInt(ActiveGamePiece.Height / 2f));
        ActivePosition = mid;
    }

    // Trys to place the active piece on the board at the active position,
    // returns the level of the piece place if placed, otherwise returns -1
    public void Place()
    {
        int level = Board.TryPlacePiece(ActiveGamePiece, ActivePosition);
        
        if (level >= 0)
        {
            //Debug.Log("Placed " + ActiveGamePiece.Value + " at " + ActivePosition);
            ActiveGamePiece = null;
            OnPlace?.Invoke(level);
        }
        else
        {
            // Did not follow rules
            OnPlace?.Invoke(-1);
        }
    }

    // moves the active piece on the board in the direction, returns the level of the piece if moved,
    // otherwise -1 if there is piece would be out of bounds
    public void Move(Vector2Int direction)
    {
        Vector2Int newPosition = ActivePosition + direction;

        if (Board.IsInBounds(ActiveGamePiece, newPosition))
        {
            ActivePosition = newPosition;
            // Debug.Log("Moved " + ActiveGamePiece.Value + " to " + ActivePosition);
            int level = Board.GetLevelFromPosition(ActiveGamePiece, ActivePosition);
            OnMove?.Invoke(direction, level);
        }
        else
        {
            // Its out of bounds
            Debug.LogWarning("Cannot place piece out of bounds");
            OnMove?.Invoke(ActivePosition, -1);
        }

    }
    
    // moves the active piece on the board to that position, returns the level of the piece if moved,
    // otherwise -1 if there is piece would be out of bounds
    public void MoveTo(Vector2Int position)
    {
        Vector2Int newPosition = position;

        if (Board.IsInBounds(ActiveGamePiece, newPosition))
        {
            ActivePosition = newPosition;
            // Debug.Log("Moved " + ActiveGamePiece.Value + " to " + ActivePosition);
            int level = Board.GetLevelFromPosition(ActiveGamePiece, ActivePosition);
            OnMove?.Invoke(ActivePosition, level);
        }
        else
        {
            // Its out of bounds
            Debug.LogWarning("Cannot place piece out of bounds");
            OnMove?.Invoke(ActivePosition, -1);
        }
    }
    
    // rotates the activePiece clockwise, returns the level of the piece if rotated,
    // otherwise -1 if there is piece would be out of bounds
    public void RotateClockwise()
    {
        GamePiece rotatedPiece = new GamePiece(ActiveGamePiece);
        rotatedPiece.RotateClockwise();
        // Check to make sure the rotated piece is still in bounds
        if (Board.IsInBounds(rotatedPiece, ActivePosition))
        {
            ActiveGamePiece.RotateClockwise();
            int level = Board.GetLevelFromPosition(ActiveGamePiece, ActivePosition);
            OnRotateClockwise?.Invoke(level);
        }
        else
        {
            Debug.LogWarning("Rotating the piece clockwise puts it out of bounds");
            OnRotateClockwise?.Invoke(-1);
        }
        
    }
    
    // rotates the activePiece counter clockwise, returns the level of the piece if rotated,
    // otherwise -1 if there is piece would be out of bounds
    public void RotateCounterClockwise()
    {
        
        GamePiece rotatedPiece = new GamePiece(ActiveGamePiece);
        rotatedPiece.RotateCounterClockwise();
        // Check to make sure the rotated piece is still in bounds
        if (Board.IsInBounds(rotatedPiece, ActivePosition))
        {
            ActiveGamePiece.RotateCounterClockwise();
            int level = Board.GetLevelFromPosition(ActiveGamePiece, ActivePosition);
            OnRotateCounterClockwise?.Invoke(level);
        }
        else
        {
            Debug.LogWarning("Rotating the piece counter clockwise puts it out of bounds");
            OnRotateClockwise?.Invoke(-1);
        }
    }


    public int CalculateScore()
    {
        int total = 0;
        for (int i = 0; i < Board.levels.Count; i++)
            total += Board.levels[i].GetTotalScore() * i;
        return total;
    }

    public int GetCurrentLevel()
    {
        return Board.GetLevelFromPosition(ActiveGamePiece, ActivePosition);
    }

    public void DebugDisplay()
    {
        for (int i = 0; i < Board.levels.Count - 1; i++)
        {
            Debug.Log("Level " + i + ":");
            Board.levels[i].Display();
        }
    }

    public void DebugDisplayWithActivePiece()
    {
        Board.DisplayWithPiece(ActiveGamePiece, ActivePosition);
    }
    
    public void DebugDisplayScore(string name)
    {
        Debug.Log($"{name}'s total score was {CalculateScore()}");
    }
}
