using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDisplay : MonoBehaviour
{
    public List<GamePiece> deck;
    

    // Start is called before the first frame update
    void Start()
    {  
        
        foreach(GamePiece piece in deck)
        {
            GameObject pieceObj = new GameObject();
            pieceObj.AddComponent<SpriteRenderer>().sprite = piece.sprite;
            piece.GetMatrix();
        }
    }

    
}
