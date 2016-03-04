using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour {
    public int health;
	// Use this for initialization
	void Start () {
	}

    // Update is called once per frame
     public int GetHealth()
    {
        return health;
    }

    public void SetHealth(int newHealth) {
        health = newHealth;
    }

}
