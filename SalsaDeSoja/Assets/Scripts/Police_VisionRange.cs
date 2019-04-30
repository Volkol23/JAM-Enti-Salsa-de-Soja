using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police_VisionRange : MonoBehaviour {

    // Private variables
    private bool canArrest = false;
    private GameObject player;

    // Outlets
    public LayerMask layerMask;

    // Private methods
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update() {
        Vector3 direction = (player.transform.position - transform.position).normalized;

        Debug.DrawRay(transform.position, direction, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1000f, layerMask);

        Debug.Log(hit.collider, hit.collider);
        if (hit.collider != null && hit.collider.tag == "Player") {
            canArrest = true;
        } else {
            canArrest = false;
        }

    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player" && canArrest) {
            GetComponentInParent<Police>().SetState(Police.State.arresting);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            GetComponentInParent<Police>().SetState(Police.State.chasing);
        }
    }
}
