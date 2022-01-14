using System;
using UnityEngine;

public class GamePiece
{
    public uint Width { get; private set; }
    public uint Height { get; private set;}
    public Sprite Sprite { get; }
    public int Value { get; }
    public int ID { get; }
    public bool[,] PieceMatrix { get; private set;}
    

    public GamePiece(Card gamePieceType, int id)
    {
        Value = gamePieceType.value;
        Sprite = gamePieceType.sprite;
        PieceMatrix = gamePieceType.ConstructMatrix();
        Height = (uint)PieceMatrix.GetLength(0);
        Width = (uint)PieceMatrix.GetLength(1);
        ID = id;
    }
    
    public GamePiece(GamePiece gamePiece)
    {
        Value = gamePiece.Value;
        Sprite = gamePiece.Sprite;
        PieceMatrix = MatrixHelper.GetMatrixCopy(gamePiece.PieceMatrix);
        Height = gamePiece.Height;
        Width = gamePiece.Width;
        ID = gamePiece.ID;
    }
    
    // public GameObject CreateInstance(Vector2Int boardPos, Vector3 offset, int level = 0)
    // {
    //     GameObject pieceObject = new GameObject();
    //     pieceObject.transform.position = new Vector3(boardPos.x, -boardPos.y, level) + offset + new Vector3(width/2.0f, -height/2.0f);
    //     pieceObject.name = Value;
    //     pieceObject.AddComponent<SpriteRenderer>().sprite = sprite;
    //     pieceObject.GetComponent<SpriteRenderer>().sortingOrder = level;
    //     pieceObject.AddComponent<GamePieceObject>().piece = this; // change later
    //     pieceObject.GetComponent<GamePieceObject>().active = false; // change later
    //     return pieceObject;
    // }

    public void RotateCounterClockwise()
    {
        int N = PieceMatrix.GetLength(0); // height
        int M = PieceMatrix.GetLength(1); // width

        // swap height and width
        Height = (uint) M;
        Width = (uint) N;
        
        bool[,] newMat = new bool[M, N];

        for (int i = 0; i < N; i++)
            for (int j = 0; j < M; j++)
                newMat[M - j - 1, i] = PieceMatrix[i, j];
        PieceMatrix = newMat;
    }
    public void RotateClockwise()
    {
        int N = PieceMatrix.GetLength(0); // height
        int M = PieceMatrix.GetLength(1); // width

        // swap height and width
        Height = (uint) M;
        Width = (uint) N;
        
        bool[,] newMat = new bool[M, N];

        for (int i = 0; i < N; i++)
            for (int j = 0; j < M; j++)
                newMat[j, N - i - 1] = PieceMatrix[i, j];
        
        PieceMatrix = newMat;
    }
   
    // Used to show to the console for debugging
    public string GetMatrixString()
    {
        return MatrixHelper.GetStringMatrix(PieceMatrix);
    }
    
    
    

}