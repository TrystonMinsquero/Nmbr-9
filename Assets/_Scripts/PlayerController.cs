using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GamePieceObject currentPiece = null;
    
    // Update is called once per frame
    void Update()
    {
        if (currentPiece == null)
            return;
        Vector2Int moveDir = Vector2Int.zero;
        if (Input.GetKeyDown(KeyCode.W))
            moveDir.y += 1;
        if (Input.GetKeyDown(KeyCode.A))
            moveDir.x -= 1;
        if (Input.GetKeyDown(KeyCode.S))
            moveDir.y -= 1;
        if (Input.GetKeyDown(KeyCode.D))
            moveDir.x += 1;
        
        if(moveDir != Vector2Int.zero)
            currentPiece.Move(moveDir);
        
        if (Input.GetKeyDown(KeyCode.Space))
            currentPiece.Place();
    }
}
