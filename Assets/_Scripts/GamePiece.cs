using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "new GamePiece")]
public class GamePiece : ScriptableObject
{
    public uint width, height;
    public int num;
    public Sprite sprite;

    private int id = -1;
    private bool[,] _pieceMatrix = null;

    public GameObject CreateInstance(Vector2Int boardPos, Vector3 offset, int level = 0)
    {
        GameObject pieceObject = new GameObject();
        pieceObject.transform.position = new Vector3(boardPos.x, -boardPos.y, level) + offset + new Vector3(width/2.0f, -height/2.0f);
        pieceObject.name = name;
        pieceObject.AddComponent<SpriteRenderer>().sprite = sprite;
        pieceObject.GetComponent<SpriteRenderer>().sortingOrder = level;
        pieceObject.AddComponent<GamePieceObject>().piece = this; // change later
        pieceObject.GetComponent<GamePieceObject>().active = false; // change later
        return pieceObject;
    }
    
    public bool[,] GetMatrix()
    {
        if (_pieceMatrix == null)
            _pieceMatrix = ConstructMatrix();
        return _pieceMatrix;
    }

    public void RotateCounterClockwise()
    {
        int N = _pieceMatrix.GetLength(0);
        int M = _pieceMatrix.GetLength(1);

        bool[,] newMat = new bool[M, N];

        for (int i = 0; i < N; i++)
            for (int j = 0; j < M; j++)
                newMat[M - j - 1, i] = _pieceMatrix[i, j];
        _pieceMatrix = newMat;
    }
    public void RotateClockwise()
    {
        int N = _pieceMatrix.GetLength(0); // height
        int M = _pieceMatrix.GetLength(1); // width

        height = (uint) M;
        width = (uint) N;

        bool[,] newMat = new bool[M, N];

        for (int i = 0; i < N; i++)
            for (int j = 0; j < M; j++)
                newMat[j, N - i - 1] = _pieceMatrix[i, j];
        
        _pieceMatrix = newMat;
    }

    private bool[,] ConstructMatrix()
    {
        uint imageWidth = (uint)sprite.rect.width;
        uint imageHeight = (uint)sprite.rect.height;
        
        uint unitSize = (uint)sprite.pixelsPerUnit; // size per unit for the image

        // This should work for most cases
        // bool[,] pieceMatrix = new bool[(int) (imageHeight / unitSize), (int) (imageWidth / unitSize)];
        
        bool[,] pieceMatrix = new bool[height, width];
        
        if (imageWidth / unitSize != width)
            Debug.LogError("Image width / unitSize does not match given width: " +
                imageWidth + "/" + unitSize + " = " + width);
        if (imageHeight / unitSize != height)
            Debug.LogError("Image height / unitSize does not match given height: " +
                imageHeight + "/" + unitSize + " = " + height);
        
        //iterate through the sprite by the unit size
        for (uint y = 0; y < imageHeight; y += unitSize)
        {
            for (uint x = 0; x < imageWidth; x += unitSize)
            {
                // iterate through each part of the unit size and check if entire block is transparent
                // if the unit is all transparent, then the game piece does not cover that
                uint i = 0;
                uint j = 0;
                bool transparent = true;
                while (i < unitSize && transparent)
                {
                    while (j < unitSize && transparent)
                    {
                        if (sprite.texture.GetPixel(
                                    (int) (sprite.rect.x + x + i),
                                    (int) (sprite.rect.y + y + j))
                                .a != 0)
                            transparent = false;
                        j++;
                    }

                    i++;
                }
                pieceMatrix[height - 1 - (y / unitSize), x / unitSize] = !transparent;
                
            }
        }
        return pieceMatrix;
    }
    public string GetMatrixString()
    {
        
        string str = "";
        bool[,] matrix = GetMatrix();

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
                str += matrix[i, j] ? "1 ": "0 ";
            str += "\n";
        }

        return str;
    }
    public static string DisplayMatrix(bool[,] matrix)
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

}