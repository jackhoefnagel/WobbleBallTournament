using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

    public static LevelManager instance;

    [Header("Gameplay Variables")]
    public List<int> playerScores;
    public bool goalsActive = false;
    public bool levelHasStarted = false;

    [Header("Level Objects")]
    public GameObject playerPrefab;
    public List<GameObject> spawnedPlayerList;
    public List<Transform> playerStartLocations;
    public Transform footballStartLocation;

    public GameObject footballObject;

    public List<GameObject> goalsList;
    public List<GameObject> goalsTriggerList;
    public List<int> currentlyActiveGoalsPlayerIDs;

    private float goalsSwitchTimeInterval = 5.0f;
    public float goalsSwitchTime = 5.0f;


    void Awake() {
        instance = this;
    }       

    void Update(){
        if(levelHasStarted){
            UpdateGoalSwitchTime();
        }
    }

    public void InitLevel(){

        spawnedPlayerList = new List<GameObject>();
    
    }

    public void StartLevel(){

        Time.timeScale = 1.0f;

        goalsActive = true;

        GUIManager.instance.ShowScoreMessage(0, false);

        ResetFootball();

        InstantiatePlayers();

        SwitchActiveGoals();

    }

    public void ResetLevel(){
        Time.timeScale = 1.0f;

        goalsActive = true;

        GUIManager.instance.ShowScoreMessage(0, false);

        ResetFootball();

        ResetPlayers();

        SwitchActiveGoals();
    }

    private void ResetFootball(){
        footballObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        footballObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        footballObject.transform.position = footballStartLocation.position;

    }

    private void InstantiatePlayers(){
        for (int i = 0; i < GameManager.instance.playersActivated.Count; i++)
        {
            if(GameManager.instance.playersActivated[i]){
                GameObject newPlayer = (GameObject)Instantiate(playerPrefab, playerStartLocations[i].position, Quaternion.identity);
                WobbleMovement newPlayerScript = newPlayer.GetComponent<WobbleMovement>();
                spawnedPlayerList.Add(newPlayer);

                newPlayerScript.playerID = i+1;
                newPlayerScript.InitPlayer();
            }
        }
    }

    public void ResetPlayers(){
        for (int i = 0; i < spawnedPlayerList.Count; i++)
        {
            WobbleMovement playerScript = spawnedPlayerList[i].GetComponent<WobbleMovement>();
            playerScript.ResetPlayer();
            spawnedPlayerList[playerScript.playerID-1].transform.position = playerStartLocations[playerScript.playerID-1].position;
        }

    }

    private void UpdateGoalSwitchTime(){
        goalsSwitchTime -= Time.deltaTime;
        if(goalsSwitchTime < 0.0f){
            //SwitchActiveGoal();
            goalsSwitchTime = goalsSwitchTimeInterval;
        }
    }


    public void SwitchActiveGoals(){

        for (int i = 0; i < 4; i++)
        {
            if(GameManager.instance.playersActivated[i] == true){
                ActivateGoal(goalsList[i], i, true);
                goalsTriggerList[i].GetComponent<GoalScript>().activePlayerID = i+1;
            }
        }
    }


    private void ActivateGoal(GameObject goal, int playerID, bool activate){

        if(activate){
            Color currentColor = goal.GetComponent<Renderer>().material.color;
            Color newColor = GameManager.instance.playerColors[playerID];
            newColor.a = currentColor.a;
            goal.GetComponent<Renderer>().material.color = newColor;

        }
        else {
            Color currentColor = goal.GetComponent<Renderer>().material.color;
            Color newColor = Color.white;
            newColor.a = currentColor.a;
            goal.GetComponent<Renderer>().material.color = newColor;
        }
    }

    public void GoalScored(int playerID)
    {
        playerScores[playerID-1]++;
        goalsActive = false;

        GUIManager.instance.ShowScoreMessage(playerID, true);


        StartCoroutine("WaitAndReset");
        //Time.timeScale = 0.5f;
    }

    private IEnumerator WaitAndReset(){
        yield return new WaitForSeconds(3.0f);
        ResetLevel();
    }
}
