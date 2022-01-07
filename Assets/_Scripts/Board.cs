using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Board
{
    public Vector2Int size = new Vector2Int(25, 25);
    private List<GamePiece[,]> _levels = new List<GamePiece[,] > ();
    
    public Board()
    {
        AddLevel();
    }

    public int GetLevelFromPosition(GamePiece piece, Vector2Int boardPos)
    {
        if (!IsInBounds(piece, boardPos))
            return -1;
        List<Vector2Int> checkPositions = new List<Vector2Int>();
        bool[,] pieceMatrix = piece.GetMatrix();

        // find all relevant positions
        for (int i = 0; i < piece.width; i++)
            for (int j = 0; j < piece.height; j++)
                if(pieceMatrix[j,i])
                    checkPositions.Add(boardPos + new Vector2Int(i, j));
        
        // find the highest level 
        int level = 0;
        while(level < _levels.Count)
            foreach (Vector2Int pos in checkPositions)
            {
                if (_levels[level][pos.x, pos.y] == null)
                {
                    level++;
                    break;
                }
            }

        return level;
    }
    
    public bool TryPlacePiece(GamePiece piece, Vector2Int boardPos)
    {
        //do checks
        
        PlacePiece(piece, boardPos);
        return true;
    }

    private void PlacePiece(GamePiece piece, Vector2Int boardPos)
    {
        int level = GetLevelFromPosition(piece, boardPos);
        if (level < 0)
            throw new Exception("boardPosition is out of bounds");

        var pieceMatrix = piece.GetMatrix();
        
        // update all relevant positions
        for (int i = 0; i < piece.width; i++)
            for (int j = 0; j < piece.height; j++)
                if(pieceMatrix[j,i])
                    _levels[level][boardPos.x + i, boardPos.y + j] = piece;
        
        // add additional level if possible
        if(level + 1 == _levels.Count)
            AddLevel();
        
    }

    private bool IsInBounds(GamePiece piece, Vector2Int boardPos)
    {
        if (boardPos.x < 0 || boardPos.y < 0)
            return false;
        if (boardPos.x + piece.width >= size.x)
            return false;
        if (boardPos.y + piece.height >= size.y)
            return false;
        return true;
    }
    private bool IsHanging(GamePiece piece, Vector2Int boardPos)
    {
        
        throw new NotImplementedException();
    }
    
    private bool IsOverlapping(GamePiece piece, Vector2Int boardPos)
    {
        throw new NotImplementedException();
    }

    private void AddLevel()
    {
        _levels.Add(new GamePiece[size.x, size.y]);
        // initialize all pieces to null
        for(int i = 0; i < size.x; i++)
            for (int j = 0; j < size.y; j++)
                _levels[_levels.Count - 1][i, j] = null;
    }
}