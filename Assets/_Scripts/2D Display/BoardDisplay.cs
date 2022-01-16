using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardDisplay : MonoBehaviour
{
    public GameObject tilePrefab;
    
    private uint _boardSize;
    private GameObject[,] _displayBoard;
    
    
    
    // Start is called before the first frame update
    public void Setup(Vector2 offset, uint size)
    {
        _boardSize = size;
        _displayBoard = new GameObject[_boardSize, _boardSize];
        
        for (int i = 0; i < _boardSize; i++)
            for (int j = 0; j < _boardSize; j++)
            {
                _displayBoard[i, j] = Instantiate(tilePrefab, transform);
                _displayBoard[i, j].transform.position = new Vector2(i, j);
            }
        
        transform.position =  new Vector2(-_boardSize/2, -_boardSize/2);
    }

    public void Show()
    {
        for (int i = 0; i < _boardSize; i++)
            for (int j = 0; j < _boardSize; j++)
                _displayBoard[i, j].SetActive(true);
    }
    
    public void Hide()
    {
        for (int i = 0; i < _boardSize; i++)
            for (int j = 0; j < _boardSize; j++)
                _displayBoard[i, j].SetActive(false);
    }

}
