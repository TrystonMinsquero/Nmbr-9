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
                str += matrix[i, j] != null ? (matrix[i,j].Value).ToString().PadRight(pad): "X".PadRight(pad);
            }
            str += "\n";
        }
        return str;
    }
    
   
}