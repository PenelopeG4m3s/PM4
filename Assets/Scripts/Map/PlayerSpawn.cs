using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    void Awake()
    {
        GameManager.instance.playerSpawnPoints.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        GameManager.instance.playerSpawnPoints.Remove(this);
    }
}
