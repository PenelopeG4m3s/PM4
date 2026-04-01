using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    void Awake()
    {
        GameManager.instance.enemySpawnPoints.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        GameManager.instance.enemySpawnPoints.Remove(this);
    }
}
