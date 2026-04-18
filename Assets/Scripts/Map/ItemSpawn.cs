using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    void Awake()
    {
        GameManager.instance.itemSpawnPoints.Add(this);
    }

    void OnDestroy()
    {
        GameManager.instance.itemSpawnPoints.Remove(this);
    }
}
