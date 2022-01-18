using System;
using _Scripts;
using UnityEngine;

public class PieceDisplay3D : PieceDisplay
{
    [SerializeField] private GameObject pieceTilePrefab;

    private float _pieceDepth;

    public override void Setup(GamePiece piece, Vector3 spawnPoint)
    {
        base.Setup(piece, spawnPoint);
        _pieceDepth = pieceTilePrefab.transform.localScale.z;
        UpdateLevel((int)spawnPoint.z);
        Construct3DTiles();
    }

    protected override void UpdateLevel(int level)
    {
        transform.SetZ(-_pieceDepth * level - _pieceDepth);
    }

    private void Construct3DTiles()
    {
        for (int j = 0; j < _gamePiece.Height; j++)
        {
            for (int i = 0; i < _gamePiece.Width; i++)
            {
                if (_gamePiece.PieceMatrix[j, i])
                {
                    GameObject tile = Instantiate(pieceTilePrefab, transform);
                    tile.GetComponent<MeshRenderer>().material.color = _gamePiece.Color;
                    tile.transform.localPosition =
                        new Vector3(i + .5f, -j - .5f, _pieceDepth / 2 + .001f);
                }
            }
        }
    }
}
