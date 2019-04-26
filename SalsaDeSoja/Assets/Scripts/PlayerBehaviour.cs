using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour {

    public enum State {
        moving,
        scare,
        game_over,
        win
    };

    State playerState;
    public Rigidbody2D rb;
    public float velocity;
    public float scareTime;
    public int citizenScore;
    public Text finalScore;
    public Text currentScore;
    public GameObject gameOver;
    public GameObject win;
    public int limitScore = 10000; 

    private int score;
    private int citizenScared;
    private bool isScaring;
    private float time;
    private Vector3 direction;
    private IEnumerator myroutine;


    void Start() {
        playerState = State.moving;
        rb = GetComponent<Rigidbody2D>();
        isScaring = false;
        score = 0;
        direction = Vector2.up;
        time = 0; 
    }

    private GameObject ciudadanoElegido; 

    void Update() {
        GetComponent<Animator>().SetFloat("x", rb.velocity.x);
        GetComponent<Animator>().SetFloat("y", rb.velocity.y);

        currentScore.text = "Score: " + score;
        print("SCORE: " + score);
        Behaviour();

        Ray2D playerRay = new Ray2D(transform.position, direction);
        RaycastHit2D hit = Physics2D.Raycast(playerRay.origin, playerRay.direction, 2000f);
        //Debug.DrawRay(playerRay.origin, playerRay.direction, Color.green, 2000f);

        if (hit.collider != null && hit.collider.tag.Equals("Citizen")) {
            print("ha chocado");
            print("Raycast ha colisionado con un ciudadano");
            if (hit.distance < 2) {
                //print("Esta a rango");
                if (Input.GetButton("Scare")) {
                    playerState = State.scare;
                    //print("DEBUG: Está asustando");
                    hit.collider.GetComponent<Citizen>().SetState(Citizen.State.scared);
                    GetComponent<Animator>().SetBool("Scaring", true);
                    ciudadanoElegido = hit.collider.gameObject;
                }
            }
        }
        else {
            //print("No está chocando con nada");
        }
    }

    private void Behaviour() {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.DownArrow)) {
            GetComponent<Animator>().SetBool("isWalking", true);
        }
        else {
            GetComponent<Animator>().SetBool("isWalking", false);
        }



        switch (playerState) {
            case State.moving:
                Moving();
                break;
            case State.scare:
                isScaring = true;
                time += Time.deltaTime;
                Scare();
                
                break;
            case State.game_over:
                GameOver();
                break;
            case State.win:
                Win();
                break;
        }
    }

    private void Moving() {
        float moveHorizontal = Input.GetAxis("Horizontal");

        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector2(moveHorizontal, moveVertical);

        if (movement.x < 0f) {
            direction = Vector2.left;
            
        }
        else if (movement.x > 0f) {
            direction = Vector2.right;

        }

        if (movement.y < 0f) {
            direction = Vector2.down;

        }
        else if (movement.y > 0f) {
            direction = Vector2.up;
            
        }

        

        //if (GameObject.FindGameObjectWithTag("Police")) {
        //    velocity += 3;
        //}
        if (!isScaring)
            rb.velocity = movement * velocity;
        else
            rb.transform.position = rb.transform.position;

        if (score > limitScore)  // Set Limit Score
            playerState = State.win;
    }

    public int GetCitizensScared() {
        return citizenScared;
    }

    IEnumerator ExecuteAfterTime(float time) {
        yield return new WaitForSeconds(time);

        AddPoints();
        isScaring = false;
        playerState = State.moving;
        //TO DO: Aqui es cuando termina de asustar
        ciudadanoElegido.GetComponent<Citizen>().SetState(Citizen.State.run_away);

    }

    private void Scare() {
        // Aqui es cuando está asustado el ciudadano
        GetComponent<Animator>().SetTrigger("Scaring2");

        if (time >= scareTime) {
            citizenScared++;
            score += citizenScore;
            playerState = State.moving;
            isScaring = false;
            Debug.Log(score);
            Debug.Log(citizenScared);
            time = 0;
            ciudadanoElegido.GetComponent<Citizen>().SetState(Citizen.State.run_away);
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().IncreaseScaredCitizens();
        }
    }

    private void AddPoints() {
        citizenScared++;
        score += citizenScore;
        Debug.Log(citizenScore);
    }

    private void GameOver() {
        Time.timeScale = 0.0f;
        gameOver.SetActive(true);
        finalScore.text = "Final Score " + score;
    }

    private void Win() {
        Time.timeScale = 0.0f;
        win.SetActive(true);
        finalScore.text = "Final Score " + score;
    }

    public void SetState(State newState) {
        playerState = newState;
    }
}
