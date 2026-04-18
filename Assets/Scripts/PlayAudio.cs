using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public AudioSource audioSource;

    public void PlaySound( AudioClip sound )
    {
        audioSource.PlayOneShot(sound);
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}