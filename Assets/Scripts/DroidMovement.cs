using UnityEngine;
using System.Collections;

public class DroidMovement : MonoBehaviour
{
    public float speed;
    private bool stopMovement;
    private int stopPosition;
    private Rigidbody rb;
    private int randomSign;
    private int targetPosition;
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        randomSign = RandomSign();
        targetPosition = 6 * randomSign;
        rb.velocity = transform.forward * speed;
        stopPosition = Random.Range(8, 13);
    }

    public void SetStopMovement(bool set)
    {
        stopMovement = set;
    }

    void FixedUpdate() {
        if (!stopMovement)
        {
            if ((int)transform.position.z == stopPosition)
            {
                rb.velocity = new Vector3((speed * -1) * Mathf.Sign(targetPosition), 0.0f, 0.0f);//move randomly to the left or right

                if ((int)transform.position.x == targetPosition) //check whether it's hit the target position
                {
                    targetPosition = targetPosition * -1; //move in the opposite direction
                }
            }
        }
        else { 
            rb.velocity = new Vector3(0, 0, 0);


        }


    }
    int RandomSign() {
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
