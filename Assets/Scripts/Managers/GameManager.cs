using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    void Awake(){
        instance = this;
    }

    public List<bool> playersActivated;
    public List<Color> playerColors;

    public float playerActivationCountdownTime = 5.0f;
    public float playerActivationCountdownTimer = 5.0f;
    [SerializeField]
    private bool playerActivationCountdownActive = false;

    void Start(){
        InitGame();
    }

    void Update(){
        if(playerActivationCountdownActive){
            playerActivationCountdownTimer -= Time.deltaTime;
            if(playerActivationCountdownTimer < 0){
                StartGame();
                StartPlayerCountdown(false);
            }
        }
    }

    public void InitGame(){
        LevelManager.instance.InitLevel();
        GUIManager.instance.InitGUI();
    }

    public void StartGame(){
        LevelManager.instance.StartLevel();
        GUIManager.instance.StartGUI();
    }

    public void ActivatePlayer(int playerID){
        bool activation = !playersActivated[playerID];
        playersActivated[playerID] = activation;
        GUIManager.instance.ActivatePlayerButton(playerID, activation);

        CheckActivePlayersAndCountdown();
    }

    private void CheckActivePlayersAndCountdown(){
        int activePlayerCount = 0;
        for (int i = 0; i < playersActivated.Count; i++)
        {
            if(playersActivated[i] == true){
                activePlayerCount++;
            }
        }

        if(activePlayerCount > 1){
            StartPlayerCountdown(true);
        }
        else
        {
            StartPlayerCountdown(false);
        }
    }

    public void StartPlayerCountdown(bool active){        
        playerActivationCountdownActive = active;
        GUIManager.instance.ShowStartCountdownTimer(active);

        if(!active){
            playerActivationCountdownTimer = playerActivationCountdownTime;
        }
    }


}
