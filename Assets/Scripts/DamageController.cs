using UnityEngine;
using System.Collections;

public class DamageController : MonoBehaviour {
    public int damage;
	// Use this for initialization
	void Start () {
	
	}

    public int GetDamage()
    {
        return damage;
    }

    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }
}
