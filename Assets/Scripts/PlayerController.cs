using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary {
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {
    private Rigidbody rb;
    private AudioSource audio;
    
    public float speed;
    public Boundary boundary;
    public float tilt;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    private float nextFire;

    void Update() {
        if (Input.GetButton("Fire1") && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            audio = GetComponent<AudioSource>();
            audio.Play();
        }
        

    }
    
	void FixedUpdate () { //fixed update for physics/movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(moveHorizontal*speed, 0.0f, moveVertical*speed);

        //clamp player object in the game area
        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), 0.0f, Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
            );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt); //banks on z in respect to x

    }
}
