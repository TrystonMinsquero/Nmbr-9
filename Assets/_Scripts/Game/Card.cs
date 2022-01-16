using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "new GamePiece")]
public class Card : ScriptableObject
{
    public int value;
    public Sprite sprite;

    public GamePiece GamePiece(int id = -1)
    {
        return new GamePiece(this, id);
    }

    public Color GetDominantColor()
    {
        Dictionary<Color, int> colorsFoundCount = new Dictionary<Color, int>();
        
        for (int x = 0; x < sprite.rect.width; x++)
        {
            for (int y = 0; y < sprite.rect.height; y++)
            {
                Color pixel = sprite.texture.GetPixel((int)sprite.rect.x + x, (int)sprite.rect.y + y);
                if (pixel.a != 0)
                {
                    if (colorsFoundCount.ContainsKey(pixel))
                        colorsFoundCount[pixel] += 1;
                    else
                        colorsFoundCount[pixel] = 1;
                }
            }
        }
        
        // if the sprite is completely transparent, return transparent black
        if(colorsFoundCount.Count <= 0)
            return new Color(0,0,0,0);
        
        // set dominant color as an arbitrary color, I know it's inefficient by one step. If it bothers you, fix it
        Color dominantColor = colorsFoundCount.Keys.ToList()[0];
        int highestCount = colorsFoundCount[dominantColor];

        foreach (var color in colorsFoundCount.Keys)
        {
            if (colorsFoundCount[color] > highestCount)
            {
                dominantColor = color;
                highestCount = colorsFoundCount[color];
            }
        }

        return dominantColor;
    }
    
    public bool[,] ConstructMatrix()
    {
        uint imageWidth = (uint)sprite.rect.width;
        uint imageHeight = (uint)sprite.rect.height;
        
        uint unitSize = (uint)sprite.pixelsPerUnit; // size per unit for the image

        // Dynamically find height and width of matrix
        int height = (int) (imageHeight / unitSize);
        int width = (int) (imageWidth / unitSize);
        
        bool[,] pieceMatrix = new bool[height, width];
        
        // This can be used if given height & width
        // bool[,] pieceMatrix = new bool[height, width];
        //
        // if (imageWidth / unitSize != width)
        //     Debug.LogError("Image width / unitSize does not match given width: " +
        //         imageWidth + "/" + unitSize + " = " + width);
        // if (imageHeight / unitSize != height)
        //     Debug.LogError("Image height / unitSize does not match given height: " +
        //         imageHeight + "/" + unitSize + " = " + height);
        
        
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
}
