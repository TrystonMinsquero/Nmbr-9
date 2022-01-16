using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Board
{
    public uint size { get; }
    public List<Level> levels { get; }
    
    public Board(uint boardSize)
    {
        levels = new List<Level>();
        size = boardSize;
        levels.Add(new Level(size));
    }
    public Board(Board board)
    {
        levels = new List<Level>();
        foreach(Level level in board.levels)
            levels.Add(new Level(level));
        size = board.size;
    }
    

    public int GetLevelFromPosition(GamePiece piece, Vector2Int boardPos)
    {
        int level = 0;
        while (level < levels.Count)
        {
            if (levels[level].HasOccupiedSpace(piece, boardPos))
                level++;
            else break;
        }
        if(level >= levels.Count)
            Debug.LogError("didn't add enough levels");
        
        return level;
    }
    
    public int TryPlacePiece(GamePiece piece, Vector2Int boardPos)
    {
        //do checks
        if (!IsInBounds(piece, boardPos))
        {
            Debug.LogWarning("Piece out of Bounds");
            // DisplayWithPiece(piece, boardPos);
            return -1;
        }

        int level = GetLevelFromPosition(piece, boardPos);

        if (!levels[level].IsConnectedAdjacently(piece, boardPos))
        {
            Debug.LogWarning("Must be adjacent other pieces on this level (level " + level + ")");
             DisplayWithPiece(piece, boardPos);
            return -1;
        }

        if (level > 0)
        {
            if (levels[level].IsHanging(piece, boardPos, levels[level - 1]))
            {
                Debug.LogWarning("Can't place on air squares");
                 DisplayWithPiece(piece, boardPos);
                return -1;
            }

            if (!levels[level].IsOnTwoOrMorePieces(piece, boardPos, levels[level - 1]))
            {
                Debug.LogWarning("Must be placed on two unique pieces");
                 DisplayWithPiece(piece, boardPos);
                return -1;
            }
        }

        PlacePiece(piece, boardPos, level);
        // Display();
        return level;
    }

    private void PlacePiece(GamePiece piece, Vector2Int boardPos, int level = -1)
    {
        if (level < 0 || level >= levels.Count)
            level = GetLevelFromPosition(piece, boardPos);
        
        levels[level].PlacePiece(piece, boardPos);
        
        // add additional level if needed
        if(level + 1 >= levels.Count)
            AddLevel();
        
    }
    public bool IsInBounds(GamePiece piece, Vector2Int boardPos)
    {
        Vector2Int index = GameManager.ConvertToIndex(boardPos);

        if (index.x < 0 || index.y < 0)
            return false;
        if (index.x + piece.Height > size)
            return false;
        if (index.y + piece.Width > size)
            return false;
        return true;
    }

    private void AddLevel()
    {
        levels.Add(new Level(size));
    }

    // For Debugging
    public void Display()
    {
        int i = 0;
        foreach (Level level in levels)
        {
            level.Display(i);
            i++;
        }
    }
    
    public void DisplayWithPiece(GamePiece gamePiece, Vector2Int boardPos)
    {
        Board tempBoard = new Board(this);
        tempBoard.PlacePiece(gamePiece, boardPos);
        Debug.Log("Board with " + gamePiece + " place at " + boardPos + ":");
        tempBoard.Display();
    }


}