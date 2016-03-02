using UnityEngine;

public class EasyFadeIn : MonoBehaviour
{
    private AudioSource audios;
    public int approxSecondsToFade = 10;
    void Start() {
        audios = GetComponent<AudioSource>();

    }
    void FixedUpdate()
    {
        
        if (audios.volume < 1)
        {
            audios.volume = audios.volume + (Time.deltaTime / (approxSecondsToFade + 1));
        }
        else
        {
            Destroy(this);
        }
    }
}