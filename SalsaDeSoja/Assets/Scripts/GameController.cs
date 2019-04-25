﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    // Enums
    public enum Direction {
        vertical_up,
        vertical_down,
        horizontal_left,
        horizontal_right
    }

    // Public variables
    public int maxCitizens;
    public float timeToSpawnCitizen;

    // Outlets
    //Generators <!> Hay que mejorar esta parte, mejor hacerlo con un array/lista.
    public GameObject left_up_generator;
    public GameObject left_down_generator;

    public GameObject right_up_generator;
    public GameObject right_down_generator;

    public GameObject up_left_generator;
    public GameObject up_right_generator;

    public GameObject down_left_generator;
    public GameObject down_right_generator;

    public GameObject police;
    public GameObject citizen;

    // Private variables
    private int citizens = 0;
    private float counterToSpawnCitizen;

    private int scaredCitizens = 0; //Usar el método IncrementScaredCitizens para incrementar por cada ciudadano asustado desde player

    void Update() {
        if (citizens < maxCitizens) {
            counterToSpawnCitizen += Time.deltaTime;
            if (counterToSpawnCitizen >= timeToSpawnCitizen) {
                GenerateCitizen();
                counterToSpawnCitizen = 0.0f;
            }
        }

        // debug

        if (Input.GetKeyDown(KeyCode.KeypadEnter)) {
            IncreaseScaredCitizens();
        }


        if (scaredCitizens == 10) {
            GeneratePolice();
            scaredCitizens = 0;
        }
    }

    // Private methods
    private void GeneratePolice() {
        Direction randomDir = (Direction)Mathf.Floor(Random.Range(0, 4));
        int randomPosGenerator = Random.Range(0, 2);

        switch (randomDir) {
            case Direction.horizontal_left:
                if (randomPosGenerator == 1) {
                    Instantiate(police, left_up_generator.transform.position, left_up_generator.transform.rotation);
                } else {
                    Instantiate(police, left_down_generator.transform.position, left_down_generator.transform.rotation);
                }
                break;
            case Direction.horizontal_right:
                if (randomPosGenerator == 1) {
                    Instantiate(police, right_up_generator.transform.position, right_up_generator.transform.rotation);
                } else {
                    Instantiate(police, right_down_generator.transform.position, right_down_generator.transform.rotation);
                }

                break;
            case Direction.vertical_down:
                if (randomPosGenerator == 1) {
                    Instantiate(police, down_left_generator.transform.position, down_left_generator.transform.rotation);
                } else {
                    Instantiate(police, down_right_generator.transform.position, down_right_generator.transform.rotation);
                }
                break;
            case Direction.vertical_up:
                if (randomPosGenerator == 1) {
                    Instantiate(police, up_left_generator.transform.position, up_left_generator.transform.rotation);
                } else {
                    Instantiate(police, up_right_generator.transform.position, up_right_generator.transform.rotation);
                }
                break;
        }
    }

    private void GenerateCitizen() {
        Direction randomDir = (Direction)Mathf.Floor(Random.Range(0, 4));
        int randomPosGenerator = Random.Range(0, 2);

        GameObject newCitizen;

        switch (randomDir) {
            case Direction.horizontal_left:
                if (randomPosGenerator == 1) {
                    newCitizen = Instantiate(citizen, left_up_generator.transform.position, left_up_generator.transform.rotation);
                } else {
                    newCitizen = Instantiate(citizen, left_down_generator.transform.position, left_down_generator.transform.rotation);
                }
                newCitizen.GetComponent<Citizen>().SetDirection(Citizen.Direction.horizontal_left);

                break;
            case Direction.horizontal_right:
                if (randomPosGenerator == 1) {
                    newCitizen = Instantiate(citizen, right_up_generator.transform.position, right_up_generator.transform.rotation);
                } else {
                    newCitizen = Instantiate(citizen, right_down_generator.transform.position, right_down_generator.transform.rotation);
                }
                newCitizen.GetComponent<Citizen>().SetDirection(Citizen.Direction.horizontal_right);

                break;
            case Direction.vertical_down:
                if (randomPosGenerator == 1) {
                    newCitizen = Instantiate(citizen, down_left_generator.transform.position, down_left_generator.transform.rotation);
                } else {
                    newCitizen = Instantiate(citizen, down_right_generator.transform.position, down_right_generator.transform.rotation);
                }
                newCitizen.GetComponent<Citizen>().SetDirection(Citizen.Direction.vertical_down);

                break;
            case Direction.vertical_up:
                if (randomPosGenerator == 1) {
                    newCitizen = Instantiate(citizen, up_left_generator.transform.position, up_left_generator.transform.rotation);
                } else {
                    newCitizen = Instantiate(citizen, up_right_generator.transform.position, up_right_generator.transform.rotation);
                }
                newCitizen.GetComponent<Citizen>().SetDirection(Citizen.Direction.vertical_up);

                break;
        }
        citizens++;
    }

    // Public methods
    public void DecreaseCitizens() {
        citizens--;
    }

    public void IncreaseScaredCitizens() {
        scaredCitizens++;
    }
}
