using UnityEngine;

public class FightRoundKeeper : MonoBehaviour {
    private int player1Wins;
    private int player2Wins;
    private void Awake() {
        GameManagement.Instance.SpawnFighters();
    }

    public bool IsGameOver() {
        return player1Wins == 3 || player2Wins == 3;
    }

    public void ResetFighters() {
        //IncrementWinner();
    }
}
