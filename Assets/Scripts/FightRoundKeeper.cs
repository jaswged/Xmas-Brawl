using UnityEngine;
using UnityEngine.InputSystem;

public class FightRoundKeeper : MonoBehaviour {
    private int player1Wins;
    private int player2Wins;

    [SerializeField] private Transform Player1SpawnPoint;
    [SerializeField] private Transform Player2SpawnPoint;
    
    private void Awake() {
        SpawnFighters();
    }

    public bool IsGameOver() {
        return player1Wins == 3 || player2Wins == 3;
    }

    public void ResetFighters() {
        //IncrementWinner();
        SpawnFighters();
    }
    
    public void SpawnFighters() {
        var player1Char = Instantiate(GameManagement.Instance.GetPlayer1Char(), Player1SpawnPoint);
        player1Char.GetComponent<Enemy>().enabled = false;

        var player2Char = Instantiate(GameManagement.Instance.GetPlayer2Char(), Player2SpawnPoint);
        player2Char.GetComponent<FightController>().enabled = false;
        player2Char.GetComponent<PlayerInput>().enabled = false;
        player2Char.tag = "Enemy";
    }
}
