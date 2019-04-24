using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : MonoBehaviour {

    enum State {
        chasing,
        arresting,
        going_away
    }

    public GameObject player;
    public float velocity = 20.0f;
    public GameObject visionado; 

    private State state; 

	void Start () {
        state = State.chasing; 
	}
	
	void Update () {
        CheckBehaviourPolice();
        visionado.transform.LookAt(player.transform);
	}

    void CheckBehaviourPolice() {
        switch (state) {
            case State.chasing:
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, velocity * Time.deltaTime);
                break;
            case State.arresting:
                //TO DO ARRESTING
                break;
            case State.going_away:
                //TO DO GOING AWAY
                break; 
        }
    }
}
