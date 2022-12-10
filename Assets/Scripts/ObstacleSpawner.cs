using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private Obstacle[] obstaclePrefabs;
    [SerializeField] private Transform obstacleParent;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnInterval;
    [SerializeField] private float spawnDelay;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnRandomObstacle), spawnInterval, spawnDelay);
    }

    private void SpawnRandomObstacle()
    {
        Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)], spawnPoint.position, Quaternion.identity)
            .transform.parent = obstacleParent;
    }
}
