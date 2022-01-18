using System;
using _Scripts;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PieceDisplay2D : PieceDisplay
{
    [SerializeField] private int baseTint = 135;
    [SerializeField] private int tintIncrease = 35;
    
    public override void Setup(GamePiece piece, Vector3 spawnPoint)
    {
        base.Setup(piece, spawnPoint);
        _sr = GetComponent<SpriteRenderer>();
        _sr.sprite = piece.Sprite;
        UpdateLevel((int)spawnPoint.z);
    }
    
    protected override void UpdateLevel(int level)
    {
        if(level < 0)
            _sr.color = new Color(255, 255, 255);
        else
        {
            int colorVal = baseTint + level * tintIncrease;
            _sr.color = new Color(colorVal/255f, colorVal/255f, colorVal/255f);
        }
        
        transform.SetZ(-level);
    }
}
