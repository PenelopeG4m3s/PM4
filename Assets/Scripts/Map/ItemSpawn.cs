using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    void Awake()
    {
        GameManager.instance.itemSpawnPoints.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        GameManager.instance.itemSpawnPoints.Remove(this);
    }
}
