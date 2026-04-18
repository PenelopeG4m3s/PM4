using UnityEngine;

public class AudioManage : MonoBehaviour
{
    public GameObject playAudioPrefab;

    public void PlayAudio( AudioClip sound )
    {
        PlayAudio playAudio = SpawnAudio( playAudioPrefab );
        playAudio.PlaySound( sound );
    }

    public PlayAudio SpawnAudio( GameObject prefab )
    {
        GameObject tempAudio = Instantiate<GameObject>(prefab, Vector3.zero, Quaternion.identity);
        GameManager.instance.SetParent(tempAudio);
        return tempAudio.GetComponent<PlayAudio>();
    }
}
