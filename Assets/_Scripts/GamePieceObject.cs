using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePieceObject : MonoBehaviour
{

    public bool active = true;
    public GamePiece piece;
    public Vector2Int currentBoardPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Move(Vector2Int direction)
    {
        transform.Translate(new Vector3(direction.x, direction.y));
        currentBoardPosition += direction;
        //check bounds
    }

    public void Place()
    {
        
    }
}
