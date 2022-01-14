using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PieceDisplay : MonoBehaviour
{
    private GamePiece _gamePiece;
    private SpriteRenderer _sr;

    private Vector3 _rotateOffset;

    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    public void Setup(GamePiece piece, Vector3 spawnPoint)
    {
        _sr = GetComponent<SpriteRenderer>();
        _gamePiece = piece;
        _sr.sprite = piece.Sprite;
        _rotateOffset = Vector3.zero;
        transform.position = spawnPoint + new Vector3(-.5f, -.5f);
    }
    
    
    public void Move(Vector2Int direction)
    {
        transform.position += new Vector3(direction.x, direction.y);
    }

    public void RotateClockwise()
    {
        transform.eulerAngles -= Vector3.forward * 90;
        
        // Reset positional offset
        transform.position -= SetRotateOffset();
        transform.position += _rotateOffset;
    }
    
    public void RotateCounterClockwise()
    {
        transform.eulerAngles += Vector3.forward * 90;
        
        // Reset positional offset
        transform.position -= SetRotateOffset();
        transform.position += _rotateOffset;
    }

    // Sets _offset to the correct Vector2, but returns the old offset
    private Vector3 SetRotateOffset()
    {
        Vector2 oldOffset = _rotateOffset;
        switch (Mathf.RoundToInt(transform.eulerAngles.z))
        {
            // normal
            case 0: _rotateOffset = Vector2.zero;
                break;
            // counter once
            case 90: _rotateOffset = new Vector2(0, -_gamePiece.Height);
                break;
            // clockwise once
            case 270:
            case -90: _rotateOffset = new Vector2(_gamePiece.Width, 0);
                break;
            // inverted
            case 180:
            case -180: _rotateOffset = new Vector2(_gamePiece.Width, -_gamePiece.Height);
                break;
            default:
                throw new Exception($"rotation z value should be 0, 90, -90, or -180, rotation z value = {transform.rotation.eulerAngles.z}");
        }
        return oldOffset;
    }
}
