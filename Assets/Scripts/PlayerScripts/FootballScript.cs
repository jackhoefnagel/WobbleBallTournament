using UnityEngine;
using System.Collections;

public class FootballScript : MonoBehaviour {

    void OnTriggerEnter(Collider coll){
        if(coll.CompareTag("GoalTrigger")){

            if(LevelManager.instance.goalsActive == true){
                int activePlayerID = coll.GetComponent<GoalScript>().activePlayerID;
                if(activePlayerID != 0){
                    LevelManager.instance.GoalScored(activePlayerID);
                }
            }
        }
    }
}
