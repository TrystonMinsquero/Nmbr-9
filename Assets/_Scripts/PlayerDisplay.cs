using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDisplay : MonoBehaviour
{
    public Vector3 offset;
    public List<GamePiece> deck;
    public Board board;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(board.size.x, board.size.y);
        //board.TryPlacePiece(deck[0], Vector2Int.zero);
        deck[0].CreateInstance(Vector2Int.zero, offset);
        deck[1].CreateInstance(new Vector2Int(3,0), offset);
        deck[2].CreateInstance(new Vector2Int(0,7), offset);
    }

    
}
