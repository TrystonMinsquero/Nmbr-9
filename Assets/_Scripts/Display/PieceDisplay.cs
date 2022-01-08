using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(SpriteRenderer))]
public class PieceDisplay : MonoBehaviour
{
    private GamePiece _gamePiece;
    private SpriteRenderer _sr;

    private Vector3 _offset;

    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    public void Setup(GamePiece piece, Vector3 spawnPoint)
    {
        _sr = GetComponent<SpriteRenderer>();
        _gamePiece = piece;
        _sr.sprite = piece.Sprite;
        _offset = GetOffset();
        transform.position = spawnPoint + _offset;
    }
    
    
    public void Move(Vector2Int direction)
    {
        transform.position += new Vector3(direction.x, direction.y);
    }

    public void RotateClockwise()
    {
        transform.eulerAngles -= Vector3.forward * 90;
        transform.position -= _offset;
        _offset = GetOffset();
        transform.position += _offset;

        // bool rotated = 
        // pieceDisplay.transform.position.z = pieceDisplay.transform.eulerAngles.z % 180 == 0 ? 
    }
    
    public void RotateCounterClockwise()
    {
        transform.eulerAngles += Vector3.forward * 90;
        transform.position -= _offset;
        _offset = GetOffset();
        transform.position += _offset;
    }

    private Vector2 GetOffset()
    {
        
        Vector2 offset;
        offset.x = _gamePiece.Width % 2 == 0 ? .5f : 0;
        offset.y = _gamePiece.Height % 2 == 0 ? .5f : 0;
        return offset;
    }
}
