using UnityEngine;
using System.Collections;

public class FireLaserBeam : MonoBehaviour
{
    private AudioSource audioSource;
    public GameObject shot;
    public Transform shotSpawn;
    public float shotDuration;
    public float initialDelay;
    public float timeBetweenShots;
    private DroidMovement mover;
    private bool firstShot;
    private CapsuleCollider capsCollider;
    private GameObject beam;
    // Use this for initialization
    void Start()
    {
        mover = GetComponent<DroidMovement>();
        audioSource = GetComponent<AudioSource>();
        firstShot = true;
        StartCoroutine(Fire());
    }
    IEnumerator Fire()
    {

            yield return new WaitForSeconds(initialDelay);//initial delay
        while (true) {
            if (!firstShot) {
                yield return new WaitForSeconds(timeBetweenShots);
            }
            mover.SetStopMovement(true);
            beam = Instantiate(shot, shotSpawn.position, shotSpawn.rotation) as GameObject; //collider not working
            audioSource.Play();     
            yield return new WaitForSeconds(shotDuration);
            Destroy(beam);
            mover.SetStopMovement(false);
            firstShot = false;
            
        }
    }

    void OnDestroy() {
        if (beam != null) {
            Destroy(beam);
        }
        
    }
}
