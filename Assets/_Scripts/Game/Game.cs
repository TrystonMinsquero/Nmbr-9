using System;
using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Card[] availablePieces;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private uint boardSize;
    [SerializeField] private uint numPlayers;

    private Player[] _players;
    private PlayerDisplay[] _displays;
    private Deck _deck;
    private int _turnNumber;

    private void Start()
    {
        SetupGame();
        BeginTurn();
    }

    private void Update()
    {
        foreach (Player player in _players)
        {
            if (player.ActiveGamePiece != null)
                return;
        }
        //BeginTurn();
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
            _displays[i].Setup(ref _players[i]);
            
            _displays[i].gameObject.SetActive(false);
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

        StartCoroutine(StartTurnCycle(newPiece));
    }

    IEnumerator StartTurnCycle(GamePiece gamePiece)
    {
        for (int i = 0; i < numPlayers; i++)
        {
            // Start of player i's turn
            
            _displays[i].gameObject.SetActive(true);
            _players[i].StartTurn(gamePiece);
            _displays[i].SpawnPiece(_players[i]);
            while (_players[i].ActiveGamePiece != null)
                yield return null;
            
            // End of player i's Turn
            
            // Debug.Log($"Player {(i + 1)}'s board:");
            // _players[i].DebugDisplay();
            
            _displays[i].gameObject.SetActive(false);
        }
        BeginTurn();
    }

    private void EndGame()
    {
        for (int i = 0; i < _players.Length; i++)
        {
            Debug.Log($"{_displays[i].name}'s total score was {_players[i].CalculateScore()}");
            Destroy(_displays[i].gameObject);
        }
        Start();
    }
    

}
