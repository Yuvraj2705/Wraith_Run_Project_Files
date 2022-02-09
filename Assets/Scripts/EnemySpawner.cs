using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Gizmo Settings")]
    [SerializeField] Vector3 center;
    [SerializeField] Vector3 size;

    [Header("Spawn Settings")]
    [SerializeField] Rigidbody2D Enemy;
    [SerializeField] float StartAt;
    [SerializeField] public float timeBetweenSpawn;
    [SerializeField] float LevelTwoStartAt = 45;
    [SerializeField] float LevelThreeStartAt = 120;
    float SpawnTime;

    [Header("Enemy Settings")]
    [SerializeField] public float ForceApplied;
    [SerializeField] public float DestroyInstaceAt;

    private bool LevelOneCheck = true;
    private bool LevelTwoCheck = false;

    void Awake()
    {
        SpawnTime = StartAt;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(center, size);
    }

    void Update()
    {
        if(Time.timeSinceLevelLoad > SpawnTime)
        {
            SpawnEnemy();
            SpawnTime = Time.timeSinceLevelLoad + timeBetweenSpawn;   
        }

        SettingsUpdater();
    }

    void SpawnEnemy()
    {
        Vector3 randomPosition = new Vector3(transform.position.x, Random.Range(size.y/2,-size.y/2), 0);
        var EnemyInstance = Instantiate(Enemy,randomPosition + new Vector3(0,center.y,0) ,Quaternion.identity);
        EnemyInstance.AddForce(Vector2.left * ForceApplied);
        Destroy(EnemyInstance.gameObject,DestroyInstaceAt);
    }

    void SettingsUpdater()
    {
        if(LevelOneCheck && Time.timeSinceLevelLoad >= LevelTwoStartAt)
        {
            timeBetweenSpawn = 1;
            ForceApplied = 600;
            DestroyInstaceAt = 2f;
            LevelTwoCheck = true;
            LevelOneCheck = false;
            GameObject.FindObjectOfType<BackgroundScroll>().scrollSpeed = 2f;
        }

        if(LevelTwoCheck && Time.timeSinceLevelLoad >= LevelThreeStartAt)
        {
            timeBetweenSpawn = 0.8f;
            ForceApplied = 800;
            DestroyInstaceAt = 1.5f;
            LevelTwoCheck = false;
            GameObject.FindObjectOfType<BackgroundScroll>().scrollSpeed = 2.5f;
        }
    }
}
