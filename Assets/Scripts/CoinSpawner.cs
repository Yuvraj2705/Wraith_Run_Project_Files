using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [Header("Gizmo Settings")]
    [SerializeField] Vector3 center;
    [SerializeField] Vector3 size;

    [Header("Spawn Settings")]
    [SerializeField] Rigidbody2D Coin;
    [SerializeField] float StartAt;
    [SerializeField] public float timeBetweenSpawn;
    [SerializeField] float LevelTwoStartAt = 45;
    [SerializeField] float LevelThreeStartAt = 120;
    float SpawnTime;

    [Header("Coin Settings")]
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
        Gizmos.color = Color.green;
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
        var EnemyInstance = Instantiate(Coin,randomPosition + new Vector3(center.x,center.y,0) ,Quaternion.identity);
        EnemyInstance.AddForce(Vector2.left * ForceApplied);
        Destroy(EnemyInstance.gameObject,DestroyInstaceAt);
    }

    void SettingsUpdater()
    {
        if(LevelOneCheck && Time.timeSinceLevelLoad >= LevelTwoStartAt)
        {
            timeBetweenSpawn = 7;
            ForceApplied = 600;
            DestroyInstaceAt = 2f;
            LevelTwoCheck = true;
            LevelOneCheck = false;
            GameObject.FindObjectOfType<BackgroundScroll>().scrollSpeed = 2f;
        }

        if(LevelTwoCheck && Time.timeSinceLevelLoad >= LevelThreeStartAt)
        {
            timeBetweenSpawn = 5f;
            ForceApplied = 800;
            DestroyInstaceAt = 1.5f;
            LevelTwoCheck = false;
            GameObject.FindObjectOfType<BackgroundScroll>().scrollSpeed = 2.5f;
        }
    }
}
