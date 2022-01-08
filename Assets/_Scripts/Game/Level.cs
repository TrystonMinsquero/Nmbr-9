using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Level
{
    private GamePiece[,] _levelMatrix;
    private List<GamePiece> _piecesPlaced;
    private uint _size;

    public Level(uint size)
    {
        _levelMatrix = new GamePiece[size, size];
        _piecesPlaced = new List<GamePiece>();
        _size = size;
    }

    public Level(GamePiece[,] levelMatrix)
    {
        _levelMatrix = levelMatrix;
        _piecesPlaced = new List<GamePiece>();
    }
    
    public void PlacePiece(GamePiece piece, Vector2Int boardPos)
    {
        Vector2Int index = GameManager.ConvertToIndex(boardPos);
        //Debug.Log("Placing down:\n "+ piece.GetMatrixString());
        // update all relevant positions
        for (int i = 0; i < piece.Height; i++)
            for (int j = 0; j < piece.Width; j++)
                if(piece.PieceMatrix[i,j])
                    _levelMatrix[index.x + i, index.y + j] = piece;
        
        _piecesPlaced.Add(piece);
    }

    #region Checks

    private bool IsSpaceOccupied(Vector2Int index)
    {
        return _levelMatrix[index.x, index.y] != null;
    }
    
    private bool IsSpaceOccupied(int i, int j)
    {
        return _levelMatrix[i, j] != null;
    }

    public bool HasOccupiedSpace(GamePiece piece, Vector2Int boardPos)
    {
        
        Vector2Int index = GameManager.ConvertToIndex(boardPos);
        // checks all indexes a piece covers
        for (int i = 0; i < piece.Height; i++)
            for (int j = 0; j < piece.Width; j++)
                if (piece.PieceMatrix[i, j] && IsSpaceOccupied(index.x + i, index.y + j))
                    return true;
        return false;
    }
    
    private bool HasFreeSpace(GamePiece piece, Vector2Int boardPos)
    {
        Vector2Int index = GameManager.ConvertToIndex(boardPos);
        // update all relevant positions
        for (int i = 0; i < piece.Height; i++)
            for (int j = 0; j < piece.Width; j++)
                if (piece.PieceMatrix[i, j] && !IsSpaceOccupied(index.x + i, index.y + j))
                    return true;
        return false;
    }

    private bool AdjacentSpaceOccupied(Vector2Int index)
    {
        // Check up
        if (index.x + 1 < _size && IsSpaceOccupied(index.x + 1, index.y))
            return true;
        // Check down
        if (index.x - 1 > 0 && IsSpaceOccupied(index.x - 1, index.y))
            return true;
        // Check right
        if (index.y + 1 < _size && IsSpaceOccupied(index.x, index.y + 1))
            return true;
        // Check left
        if (index.y - 1 > 0 && IsSpaceOccupied(index.x, index.y - 1))
            return true;

        return false;
    }
    
    public bool IsConnectedAdjacently(GamePiece piece, Vector2Int boardPos)
    {
        if (_piecesPlaced.Count == 0)
            return true;
        
        Vector2Int index = GameManager.ConvertToIndex(boardPos);
        //Check for all relevant positions for any occupied adjacent spaces
        for (int i = 0; i < piece.Height; i++)
            for (int j = 0; j < piece.Width; j++)
                if (piece.PieceMatrix[i, j])
                    if(AdjacentSpaceOccupied(index + new Vector2Int(i,j)))
                        return true;

        return false;
    }
    public bool IsHanging(GamePiece piece, Vector2Int boardPos, Level lowerLevel)
    {
        if (lowerLevel == null)
            return true;
        if (lowerLevel.HasFreeSpace(piece, boardPos))
            return true;

        return false;
    }
    
    public bool IsOnTwoOrMorePieces(GamePiece piece, Vector2Int boardPos, Level lowerLevel)
    {
        if (lowerLevel == null)
            return true;
        if (lowerLevel.HasFreeSpace(piece, boardPos))
            return false;
        
        Vector2Int index = GameManager.ConvertToIndex(boardPos);
        HashSet<int> idsFound = new HashSet<int>();
        // update all relevant positions
        for (int i = 0; i < piece.Height; i++)
        for (int j = 0; j < piece.Width; j++)
            if (piece.PieceMatrix[i, j])
                idsFound.Add(lowerLevel._levelMatrix[index.x + i, index.y + j].ID);
        
        return idsFound.Count > 1;
    }
    #endregion
    
    // for debugging
    public void Display(int level = -1)
    {
        if(level > -1)
            Debug.Log("Level " + level + ": \n"+ MatrixHelper.GetStringMatrix(_levelMatrix));
        else
            Debug.Log(MatrixHelper.GetStringMatrix(_levelMatrix));
    }
}