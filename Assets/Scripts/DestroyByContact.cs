using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{

    public GameObject explosion;
    public GameObject playerExplosion;
    public GameObject[] splitAsteroids;
    public GameObject[] powerUps;
    public bool fragmentable;
    public int scoreValue;
    private GameController gc;
    private HealthController healthControllerFirstObj;
    private DamageController damageControllerFirstObj;
    private HealthController healthControllerBolt;
    private DamageController damageControllerBolt;
    private bool dropPowerUp;

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
        if (powerUps.Length > 0) {
            dropPowerUp = GetRandomBoolean();
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
            case "Powerup":
                return;
        }
        Debug.Log("Other: " + other.tag);
        Debug.Log("GO: " + gameObject.tag);
        healthControllerFirstObj = gameObject.GetComponent<HealthController>();
        damageControllerFirstObj = gameObject.GetComponent<DamageController>();
        healthControllerBolt = other.gameObject.GetComponent<HealthController>();
        damageControllerBolt = other.gameObject.GetComponent<DamageController>();
        int healthFirstObj = healthControllerFirstObj.GetHealth();
        int damageDealtFirstObj = damageControllerFirstObj.GetDamage();
        int healthBolt = healthControllerBolt.GetHealth();
        int damageDealtBolt = damageControllerBolt.GetDamage();
        if (damageDealtFirstObj < healthBolt)
        {
            healthControllerBolt.SetHealth(healthBolt - damageDealtFirstObj);
        }
        else {
            DestroyBolt(other);
        }
        if (damageDealtBolt < healthFirstObj)
        {
            healthControllerFirstObj.SetHealth(healthFirstObj - damageDealtBolt);
            
        }
        else {
            DestroyEnemy(other);
        }


    }

    private void DestroyBolt(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gc.GameOver();
        }



        Debug.Log("Ges");
            Destroy(other.gameObject);
    }

    private void DestroyEnemy(Collider other)
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
        if (gameObject.CompareTag("Enemy") && dropPowerUp) {
            CreatePowerUp();
        }
        for (int i = 0; i < other.transform.childCount; i++)
        {
            if (other.transform.GetChild(i).CompareTag("PlayerWeapon"))
            {
                gc.AddScore(scoreValue);
                break;
            }
        }
        if (fragmentable)
        {
            BreakIntoFragments(other);
        }

        Destroy(gameObject);
    }

    void CreatePowerUp()
    {
        Instantiate(powerUps[Random.Range(0, powerUps.Length)], transform.position, transform.rotation * Random.rotation);
    }

    void BreakIntoFragments(Collider other)
    {
        for (int i = 0; i < splitAsteroids.Length; i++)
        {
            Rigidbody fragmentRb = splitAsteroids[i].GetComponent<Rigidbody>();
            fragmentRb.constraints = RigidbodyConstraints.FreezePositionY;
            splitAsteroids[i].tag = "Enemy";
        }
        int swap = 1;
        for (int i = 0; i < splitAsteroids.Length; i++)
        {
            swap = swap * -1;
            Instantiate(splitAsteroids[i], other.transform.position + new Vector3(swap * 1f,0,0), Quaternion.AngleAxis(90, swap * transform.right) * Random.rotation); //Random.rotation
        }

    }

    bool GetRandomBoolean()
    {
    if (Random.value <= 0.15)
    {
        return true;
    }
    return false;
    }

    int RandomSign()
    {
        int randomSign = Random.Range(0, 2);
        if (randomSign == 0)
        {
            return 1;
        }
        else {
            return -1;
        }
    }
}
