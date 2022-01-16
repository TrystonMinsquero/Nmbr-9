using System;
using _Scripts;
using UnityEngine;

public abstract class PieceDisplay : MonoBehaviour
{
    protected GamePiece _gamePiece;
    protected Vector3 _rotateOffset;
    protected SpriteRenderer _sr;

    public virtual void Setup(GamePiece piece, Vector3 spawnPoint)
    {
        _gamePiece = piece;
        _rotateOffset = Vector3.zero;
        transform.position = spawnPoint + new Vector3(-.5f, -.5f);
        _sr = GetComponent<SpriteRenderer>();
        _sr.sprite = piece.Sprite;
    }

    public virtual void Place(int level)
    {
        UpdateLevel(level);
    }

    public void Move(Vector3Int direction, int level)
    {
        transform.position += new Vector3(direction.x, direction.y, direction.z);
        UpdateLevel(level);
    }

    public void RotateClockwise(int level)
    {
        transform.eulerAngles -= Vector3.forward * 90;
        
        // Reset positional offset
        transform.position -= SetRotateOffset();
        transform.position += _rotateOffset;
        
        UpdateLevel(level);
    }
    
    public void RotateCounterClockwise(int level)
    {
        transform.eulerAngles += Vector3.forward * 90;
        
        // Reset positional offset
        transform.position -= SetRotateOffset();
        transform.position += _rotateOffset;

        UpdateLevel(level);
    }

    protected abstract void UpdateLevel(int level);
    
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
