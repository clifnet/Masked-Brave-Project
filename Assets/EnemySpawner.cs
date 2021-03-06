﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform enemyContainer;
    public EnemySpawn[] enemies;
    public List<EnemySpawn> soloEnemies = new List<EnemySpawn>();
    public Vector2 rand = new Vector2(0, 2);
    public CharacterObject playerChar;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }
    private void FixedUpdate()
    {
        if (playerChar!=null)
        {
            this.transform.position = playerChar.transform.position;
        }
    }
    public void SetPlayer()
    {
        playerChar = GameEngine.gameEngine.mainCharacter;
        AddEnemiesToList();
    }
    private void AddEnemiesToList()
    {
        foreach (Transform transform in enemyContainer)
        {
            EnemySpawn enemySpawn = transform.GetComponent<EnemySpawn>();
            if (enemySpawn != null)
                soloEnemies.Add(enemySpawn);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemySpawn E = collision.gameObject.GetComponent<EnemySpawn>();
            if (soloEnemies.Contains(E))
            {
                SpawnEnemy(E);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemySpawn E = collision.gameObject.GetComponent<EnemySpawn>();
            if (soloEnemies.Contains(E))
            {
                DeSpawnEnemy(E);
            }
        }
    }
    public void SpawnEnemy(EnemySpawn enemy)
    {
        enemy.Spawn();
    }
    public void DeSpawnEnemy(EnemySpawn enemy)
    {
        enemy.DeSpawn();
    }
}
