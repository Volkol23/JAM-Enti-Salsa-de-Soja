using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police_VisionRange : MonoBehaviour {

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            print("Colisionando con player");
        }
    }
}
