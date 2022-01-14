using System;

public static class MatrixHelper
{
    public static string GetStringMatrix(bool[,] matrix)
    {
        string str = "";
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
                str += matrix[i, j] ? "1 ": "0 ";
            str += "\n";
        }
        return str;
    }
    
    public static string GetStringMatrix(GamePiece[,] matrix)
    {
        int pad = 5;
        string str = "";

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                str += matrix[i, j] != null ? (matrix[i,j].Value).ToString().PadRight(pad): "[]".PadRight(pad);
            }
            str += "\n";
        }
        return str;
    }

    public static GamePiece[,] GetMatrixCopy(GamePiece[,] matrix)
    {
        int height = matrix.GetLength(0);
        int width = matrix.GetLength(1);
        
        GamePiece[,] newMat = new GamePiece[height, width];
        
        for(int i = 0; i < height; i++)
        for (int j = 0; j < width; j++)
            if(matrix[i, j] != null)
                newMat[i, j] = new GamePiece(matrix[i, j]);
        return newMat;
    }
    
    public static bool[,] GetMatrixCopy(bool[,] matrix)
    {
        int height = matrix.GetLength(0);
        int width = matrix.GetLength(1);
        
        bool[,] newMat = new bool[height, width];
        
        for(int i = 0; i < height; i++)
        for (int j = 0; j < width; j++)
            newMat[i, j] = matrix[i, j];
        return newMat;
    }
    
}