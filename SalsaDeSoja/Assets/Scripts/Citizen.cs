using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen : MonoBehaviour {

    public enum State {
        moving,
        scared,
        run_away
    }
    
    public enum Direction {
        vertical_up, 
        vertical_down,
        horizontal_left, 
        horizontal_right
    }

    State citizenState;
    Direction citizenDirection;
    public Rigidbody2D rb;
    public GameObject player; 
    public float velocity = 5.0f;

    private GameController gameController; 

	void Start () {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        citizenState = State.moving;
	}
	
	void Update () {
        CheckCitizenBehaviour();	
	}

    private void CheckCitizenBehaviour() {
        switch (citizenState) {
            case State.moving:
                switch (citizenDirection) {
                    case Direction.horizontal_left:
                        rb.velocity = Vector2.right * velocity * Time.deltaTime;
                        break;
                    case Direction.horizontal_right:
                        rb.velocity = Vector2.left * velocity * Time.deltaTime;
                        break;
                    case Direction.vertical_up:
                        rb.velocity = Vector2.down * velocity * Time.deltaTime;
                        break;
                    case Direction.vertical_down:
                        rb.velocity = Vector2.up * velocity * Time.deltaTime;
                        break;
                    default:
                        print("ERROR: No se ha asignado ninguna dirección para el ciudadano.");
                        break;
                }
                break;
            case State.scared:
                rb.velocity = Vector2.zero; 
                break;
            case State.run_away:
                Vector2 playerPos = player.transform.position * -1; 
                rb.velocity = playerPos * velocity * Time.deltaTime;
                break;
        }
    }

    public void SetDirection(Direction newDirection) {
        citizenDirection = newDirection;
    }

    public void SetState(State newState) {
        citizenState = newState; 
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Bound") {
            Destroy(this.gameObject);
            gameController.DecreaseCitizens();
        }
    }
}
