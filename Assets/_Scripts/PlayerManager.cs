using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerInputManager))]
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [SerializeField]private int maxPlayers;
    
    public static int playerCount;
    public static PlayerInput[] Players { get; private set; }

    private static PlayerInputManager _playerInputManager;
    //used to view players in the editor
    public PlayerInput[] playersDisplay;
    
    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);

        Players = new PlayerInput[maxPlayers];
        _playerInputManager = GetComponent<PlayerInputManager>();
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        playersDisplay = Players;
        if (playerCount == maxPlayers)
            SceneManager.LoadScene("SampleScene");
    }

    //Event that gets called when input is detected
    public void OnPlayerJoined(PlayerInput playerInput)
    {
        int index = NextPlayerSlot();
        if (Contains(playerInput) ||  index < 0)
            Destroy(playerInput.gameObject);
        else
        {
            DontDestroyOnLoad(playerInput.gameObject);
            Players[index] = playerInput;
            playerInput.gameObject.name = "Player" + (index + 1);
            playerCount++;
        }
    }

    //Event that gets called when player leaves
    public void OnPlayerLeft(PlayerInput playerInput)
    {
        int index = GetIndex(playerInput);
        Destroy(playerInput.gameObject);
        if (index >= 0)
        {
            Players[index] = null;
            playerCount--;
        }
    }

    //Sets "EnableJoining" on the manager to true, allowing new inputs to join
    public static void SetJoinable(bool enabled)
    {
        if (enabled)
            PlayerInputManager.instance.EnableJoining();
        else
            PlayerInputManager.instance.DisableJoining();
    }

    //Gets the index of player in the player array, returns -1 if not there
    public static int GetIndex(PlayerInput _player)
    {
        if (Players.Length <= 0 || _player == null)
            return -1;
        for(int i = 0; i < Players.Length; i++)
            if (Players[i] == _player)
                return i;
        return -1;
    }

    //returns true if player is already connected to player manager, false otherwise
    public static bool Contains(PlayerInput _player)
    {
        if (Players.Length <= 0)
            return false;
        foreach (PlayerInput player in Players)
            if (player == _player)
                return true;
        return false;
    }
    
    //returns the next empty slot index in the array (so can leave and rejoin),
    //returns -1 if no slots are available
    public static int NextPlayerSlot()
    {
        for (int i = 0; i < Players.Length; i++)
            if (Players[i] == null)
                return i;
        return -1;
    }
}
