using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    void Awake()
    {
        GameManager.instance.playerSpawnPoints.Add(this);
    }

    void OnDestroy()
    {
        GameManager.instance.playerSpawnPoints.Remove(this);
    }
}
