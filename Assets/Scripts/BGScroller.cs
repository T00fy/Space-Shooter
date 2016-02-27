using UnityEngine;
using System.Collections;

public class BGScroller : MonoBehaviour {
    public float scrollSpeed;

    private float tileSizeY;
    private Vector3 startPosition;
	// Use this for initialization
	void Start () {
        startPosition = transform.position;
        tileSizeY = transform.localScale.y;
	}
	
	// Update is called once per frame
	void Update () {
        float newPosition = Mathf.Repeat(Time.time * -scrollSpeed, tileSizeY); //loops one second increments until it reaches the length of the bg scale (30) then resets back to 1
        // will return the remainder to the position
        transform.position = startPosition + Vector3.forward * newPosition;
	}
}
