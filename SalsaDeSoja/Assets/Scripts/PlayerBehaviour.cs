using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour {

    enum State
    {
        moving,
        scare,
        game_over
    };

    State playerState;
    public Rigidbody2D rb;
    public float velocity;
    public Rigidbody2D citizen;
    public float scareTime;
    public int citizenScore;

    private int score;
    private int citizenScared;
    private bool isScaring;
    private float time;
    private Vector3 direction;


    void Start () {
        playerState = State.moving;
        rb = GetComponent<Rigidbody2D>();
        citizen = GetComponent<Rigidbody2D>();
        isScaring = false;
        score = 0;
        direction = Vector2.up;
    }

    void Update () {
        Behaviour();

        Ray2D playerRay = new Ray2D(transform.position, direction);
        RaycastHit2D hit = Physics2D.Raycast(playerRay.origin, playerRay.direction, 2000f);
        Debug.DrawRay(playerRay.origin, playerRay.direction, Color.green, 2000f);

        if (hit.collider != null && hit.collider.tag.Equals("Citizen"))
        {
            print("ha chocado");
                print("Raycast ha colisionado con un ciudadano");
                if (hit.distance < 2)
                {
                    print("Esta a rango");
                    if (Input.GetButton("Scare"))

                        playerState = State.scare;
                }
        }
    }

    private void Behaviour()
    {
        switch (playerState)
        {
            case State.moving:
                Moving();
                break;
            case State.scare:
                isScaring = true;
                Scare();
                break;
            case State.game_over:
                GameOver();
                break;
        }
    }

    private void Moving()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector2(moveHorizontal, moveVertical);

        if (movement.x < 0f)
        {
            direction = Vector2.left;
        }
        else if (movement.x > 0f)
        {
            direction = Vector2.right;
        }

        if (movement.y < 0f)
        {
            direction = Vector2.down;
        }
        else if (movement.y > 0f)
        {
            direction = Vector2.up;
        }
        //if (GameObject.FindGameObjectWithTag("Police"))
        //{
        //    velocity += 3;
        //}
        if (!isScaring)
            rb.velocity = movement * velocity;
        else
            rb.transform.position = rb.transform.position;
    }

    public int GetCitizensScared()
    {
        return citizenScared;
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        AddPoints();
        isScaring = false;
        playerState = State.moving;
    }

    private void Scare()
    {
        StartCoroutine(ExecuteAfterTime(scareTime));
    }

    private void AddPoints()
    {
        citizenScared++;
        score += citizenScore;
        Debug.Log(citizenScore);
    }

    private void GameOver()
    {
        Time.timeScale = 0.0f;
        

    }
}
