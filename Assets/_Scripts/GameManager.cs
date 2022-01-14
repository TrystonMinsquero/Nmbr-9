using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public GameObject playerPrefab;
    public uint boardSize;
    public uint numPlayers;
    
    private Player[] _players;
    private PlayerDisplay[] _displays;
    public Card[] availablePieces;
    private Deck _deck;
    private int _turnNumber;

    private void Awake()
    {
        if (instance)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        SetupGame();
        BeginTurn();
    }

    private void Update()
    {
        foreach (Player game in _players)
            if (game.ActiveGamePiece != null)
                return;
        BeginTurn();
    }

    private void SetupGame()
    {
        _deck = new Deck(availablePieces);
        _players = new Player[numPlayers];
        _displays = new PlayerDisplay[numPlayers];
        for(int i = 0; i < numPlayers; i++)
        {
            _players[i] = new Player(boardSize);
            GameObject player = Instantiate(playerPrefab);
            player.name = "Player " + (i + 1);
            player.GetComponent<PlayerController>().Set(ref _players[i]);
            // Displays
            _displays[i] = player.GetComponent<PlayerDisplay>();    
        }
    }

    private void BeginTurn()
    {
        Card newCard = _deck.DrawCard();
        if (newCard == null)
        {
            EndGame();
            return;
        }

        GamePiece newPiece = newCard.GamePiece(++_turnNumber);
        for (int i = 0; i < numPlayers; i++)
        {
            _players[i].StartTurn(newPiece);
            _displays[i].SpawnPiece(_players[i]);
        }
    }

    private void EndGame()
    {
        for (int i = 0; i < _players.Length; i++)
        {
            Debug.Log($"{_displays[i].name}'s total score was {_players[i].CalculateScore()}");
            Destroy(_displays[i].gameObject);
        }
        SetupGame();
    }
    
    
    public static Vector2Int ConvertToIndex(Vector2Int boardPos)
    {
        Vector2Int index = new Vector2Int();
        index.x = (int)GameManager.instance.boardSize - boardPos.y;
        index.y = boardPos.x;

        return index;
    }
    
    public static Vector2Int ConvertToPosition(Vector2Int index)
    {
        Vector2Int position = new Vector2Int();
        position.x = (int)GameManager.instance.boardSize - index.y;
        position.y = index.x;

        return index;
    }
}
