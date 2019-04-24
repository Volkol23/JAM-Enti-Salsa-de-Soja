using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public Collider2D citizen;
    private int score;
    private int citizenScared;


    void Start () {
        playerState = State.moving;
        rb = GetComponent<Rigidbody2D>();
        citizen = GetComponent<Collider2D>();
    }

    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up);

    void Update () {
        Behaviour();
        
	}

    private void Behaviour()
    {
        switch (playerState)
        {
            case State.moving:
                Moving();
                break;
            case State.scare:
                Scare();
                break;
            case State.game_over:
                //TO DO RUN AWAY
                break;
        }
    }

    private void Moving()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
        //if (GameObject.FindGameObjectWithTag("Police"))
        //{
        //    velocity += 3;
        //}
        rb.velocity = movement * velocity;

        if (hit.collider != null)
        {
            if (hit.collider == citizen)
            if (hit.distance < 4)
            {
                if (Input.GetButton("Scare"))
                    playerState = State.scare;
            }
        }
    }

    public int GetCitizensScared()
    {
        return citizenScared;
    }

    private void Scare()
    {

    }
}
