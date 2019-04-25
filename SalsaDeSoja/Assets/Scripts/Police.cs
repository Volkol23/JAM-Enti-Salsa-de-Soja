﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : MonoBehaviour {

    // Enums
    public enum State {
        chasing,
        arresting,
        going_away
    }
    // Outlets
    public GameObject vision;
    public GameObject pointToEscape; 

    // Public variables
    public float velocity = 20.0f;
    public float timePlayerInsideVision = 2.0f;

    // Private variables
    private State state;
    private float counterArresting = 0.0f;
    private GameObject player;

    void Start() {
        state = State.chasing;
        player = GameObject.FindGameObjectWithTag("Player");
        pointToEscape = GameObject.Find("Up_Left");
    }

    void Update() {
        CheckBehaviourPolice();
        vision.transform.LookAt(player.transform);
    }

    // Private methods
    private void CheckBehaviourPolice() {
        switch (state) {
            case State.chasing:
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, velocity * Time.deltaTime);
                break;
            case State.arresting:
                counterArresting += Time.deltaTime;
                if (counterArresting >= timePlayerInsideVision) {
                    //TO DO: SET PLAYER STATE GAME_OVER
                    print("Player: GAME_OVER");
                } else {
                    transform.position = Vector2.MoveTowards(transform.position, player.transform.position, velocity * Time.deltaTime);
                }
                break;
            case State.going_away:
                //TO DO: GOING AWAY
                transform.position = Vector2.MoveTowards(transform.position, pointToEscape.transform.position, velocity * Time.deltaTime);
                break;
        }
    }

    // Public methods
    public void SetState(State newState) {
        state = newState;
    }
}
