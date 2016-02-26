using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Boundary") {
            return;
        }
        Destroy(other.gameObject); //the bolt or the player
        Destroy(gameObject); //the asteroid itself
    }
}
