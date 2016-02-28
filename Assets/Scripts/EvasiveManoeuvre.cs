using UnityEngine;
using System.Collections;

public class EvasiveManoeuvre : MonoBehaviour {

    public float dodge;
    public float smoothing;
    public float tilt;
    public Vector2 startWait;
    public Vector2 manoeuvreDuration;
    public Vector2 manoeuvreWait;
    public Boundary boundary;

    private float currentSpeed;
    private float targetManoeuvre;
    private Rigidbody rb;
    void Start () {
        rb = GetComponent<Rigidbody>();
        currentSpeed = rb.velocity.z;
        StartCoroutine(Evade());
    }

    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y)); //compacting down two int into a vector2
        while (true) {
            targetManoeuvre = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x); //orient target manoeuvre towards the opposite side of the screen
            yield return new WaitForSeconds(Random.Range(manoeuvreDuration.x, manoeuvreDuration.y)); //how long its going to do the manoeuvre for
            targetManoeuvre = 0;
            yield return new WaitForSeconds(Random.Range(manoeuvreWait.x, manoeuvreWait.y)); //wait random time before doing another manoeuvre

        }
    }

    void FixedUpdate () {
        float newManeuver = Mathf.MoveTowards(rb.velocity.x, targetManoeuvre, Time.deltaTime * smoothing); //move towards the manoeuvre never exceeding speed set in smoothing (how quickly the enemy moves into the dodge)
        rb.velocity = new Vector3(newManeuver, 0.0f, currentSpeed);
        rb.position = new Vector3 //clamp position to scene
        (
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }
}
