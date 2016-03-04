using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{

    public GameObject explosion;
    public GameObject playerExplosion;
    public GameObject[] splitAsteroids;
    public bool fragmentable;
    public int scoreValue;
    private GameController gc;
    private Mover move;

    void Start()
    {
        GameObject gcObject = GameObject.FindWithTag("GameController");
        if (gcObject != null)
        {
            gc = gcObject.GetComponent<GameController>();
        }//get the reference for gamecontroller 
        else {
            Debug.Log("Can't find gamecontroller");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.tag) {
            case "Boundary":
                return;
            case "Enemy":
                return;
            case "Player":
                Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                gc.GameOver();
                break;
        }
        
        if (explosion != null && !gameObject.CompareTag("Laser")) {
            Instantiate(explosion, transform.position, transform.rotation);
        }

        if (gameObject.CompareTag("Laser")) {
            Destroy(other.gameObject);
            return;
        }

        if (other.CompareTag("Laser")) {
            Destroy(gameObject);
            return;
        }
        gc.AddScore(scoreValue);

        if (fragmentable)
        {
            BreakIntoFragments(other);
        }
        else{
            Destroy(other.gameObject); //the bolt or the player
            Destroy(gameObject);
        }


    }

    void BreakIntoFragments(Collider other)
    {
        for (int i = 0; i < splitAsteroids.Length; i++)
        {
            Rigidbody fragmentRb = splitAsteroids[i].GetComponent<Rigidbody>();
            fragmentRb.constraints = RigidbodyConstraints.FreezePositionY;
            splitAsteroids[i].tag = "Enemy";
        }
        for (int i = 0; i < splitAsteroids.Length; i++)
        {
            Instantiate(splitAsteroids[i], transform.position, transform.rotation * Random.rotation);
        }
        if (other.CompareTag("Bolt"))
        {
            Destroy(other.gameObject);
        }
        if (!gameObject.CompareTag("Laser")) {
            Destroy(gameObject);
        }
        

    }
}
