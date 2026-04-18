using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    void Awake()
    {
        GameManager.instance.enemySpawnPoints.Add(this);
    }

    void OnDestroy()
    {
        GameManager.instance.enemySpawnPoints.Remove(this);
    }
}
