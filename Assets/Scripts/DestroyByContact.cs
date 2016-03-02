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
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy"))
        {
            return;
        }

        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        } //can leave explosion empty in inspector


        if (other.CompareTag("Player"))
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gc.GameOver();
        }
        gc.AddScore(scoreValue);
        if (fragmentable)
        {
            for (int i = 0; i < splitAsteroids.Length; i++)
            {
                Rigidbody fragmentRb = splitAsteroids[i].GetComponent<Rigidbody>();
                fragmentRb.constraints = RigidbodyConstraints.FreezePositionY;
                splitAsteroids[i].tag = "Enemy";
            }
            for (int i = 0; i < splitAsteroids.Length; i++) {
                Instantiate(splitAsteroids[i], transform.position, transform.rotation * Random.rotation);
            }
            if (other.CompareTag("Bolt"))
            {
                Destroy(other.gameObject);
            }
            Destroy(gameObject);
        }
        else {
            Destroy(other.gameObject); //the bolt or the player
            Destroy(gameObject);
        }
    }
}
