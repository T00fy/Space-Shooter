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
    private HealthController healthControllerFirstObj;
    private DamageController damageControllerFirstObj;
    private HealthController healthControllerSecondObj;
    private DamageController damageControllerSecondObj;

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
        switch (other.tag)
        {
            case "Boundary":
                return;
            case "Enemy":
                return;
        }
        healthControllerFirstObj = gameObject.GetComponent<HealthController>();
        damageControllerFirstObj = gameObject.GetComponent<DamageController>();
        healthControllerSecondObj = other.gameObject.GetComponent<HealthController>();
        damageControllerSecondObj = other.gameObject.GetComponent<DamageController>();
        int healthFirstObj = healthControllerFirstObj.GetHealth();
        int damageDealtFirstObj = damageControllerFirstObj.GetDamage();
        int healthSecondObj = healthControllerSecondObj.GetHealth();
        int damageDealtSecondObj = damageControllerSecondObj.GetDamage();
        if (damageDealtSecondObj < healthFirstObj)
        {
            healthControllerFirstObj.SetHealth(healthFirstObj - damageDealtSecondObj);
            Debug.Log(healthControllerFirstObj.GetHealth());
        }
        else {
            DestroyFirstObject();
        }
        if (damageDealtFirstObj < healthSecondObj) {
            healthControllerSecondObj.SetHealth(healthSecondObj - damageDealtFirstObj);
        }
        else {
            DestroySecondObject(other);
        }

    }

    private void DestroySecondObject(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gc.GameOver();
        }

        gc.AddScore(scoreValue);

        if (fragmentable)
        {
            BreakIntoFragments(other);
        }
        else { //the bolt or the player
            Destroy(other.gameObject);
        }
    }

    private void DestroyFirstObject()
    {
        if (CompareTag("Player"))
        {
            Instantiate(playerExplosion, transform.position, transform.rotation);
            gc.GameOver();
        }


        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }
        gc.AddScore(scoreValue);
        Destroy(gameObject);
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
