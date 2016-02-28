using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;
    private GameController gc;

    void Start() {
        GameObject gcObject = GameObject.FindWithTag("GameController");
        if (gcObject != null)
        {
            gc = gcObject.GetComponent<GameController>();
        }//get the reference for gamecontroller 
        else {
            Debug.Log("Can't find gamecontroller");
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy")) {
            return;
        }

        if (explosion != null) {
            Instantiate(explosion, transform.position, transform.rotation);
        } //can leave explosion empty in inspector
        

        if (other.CompareTag("Player")) {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gc.GameOver();
        }
        gc.AddScore(scoreValue);
        Destroy(other.gameObject); //the bolt or the player
        Destroy(gameObject); //the asteroid itself
    }
}
