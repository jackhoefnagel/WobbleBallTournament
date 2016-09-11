using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GUIManager : MonoBehaviour {
    
    public static GUIManager instance;
    void Awake(){
        instance = this;
    }

    public Text timeText;

    public GameObject startGamePanel;

    public GameObject scorePanel;
    public List<Text> scoreTexts;

    public Text countdownText;
    public GameObject countDownTimerPanel;

    public GameObject scoreMessage;

    public List<Image> playerButtonImages;

    public void InitGUI(){
        scorePanel.SetActive(false);
        countDownTimerPanel.SetActive(false);
   }

    public void StartGUI(){
        scorePanel.SetActive(true);
        ActivateStartGamePanel(false);
    }

    void Update(){
        UpdateTimeText();
        UpdateScoreTexts();
        UpdateCountdownText();
    }

    void UpdateTimeText(){
        timeText.text = "Next Goal: " + LevelManager.instance.goalsSwitchTime.ToString("N") + "s";
    }

    void UpdateScoreTexts(){        
        for (int i = 0; i < scoreTexts.Count; i++) {
            scoreTexts[i].text = LevelManager.instance.playerScores[i].ToString();
        }
    }

    void UpdateCountdownText(){
        countdownText.text = "Game Starts in: "+GameManager.instance.playerActivationCountdownTimer.ToString("N1");
    }

    public void ActivateStartGamePanel(bool activate){
        startGamePanel.SetActive(activate);
    }

    public void ActivatePlayerButton(int playerID, bool activate){

        if(activate){
            playerButtonImages[playerID].color = GameManager.instance.playerColors[playerID];
        }
        else
        {
            playerButtonImages[playerID].color = Color.white;
        }
    }

    public void ShowStartCountdownTimer(bool active){
        countDownTimerPanel.SetActive(active);
    }

    public void ShowScoreMessage(int playerID, bool active){
        //scoreMessage.SetActive(active);
        scoreMessage.GetComponent<Animator>().SetBool("showMessage", active);

        if(active){
            Text scoreText = scoreMessage.GetComponent<Text>();
            scoreText.text = "Player "+playerID.ToString() + " scored!";
            scoreText.color = GameManager.instance.playerColors[playerID-1];
        }
    }
}
