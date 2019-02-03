﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tagText;
        public GameObject prefabObject;
        public int maimumSize;
    }

    #region Singleton Instance
    public static ObjectPooler Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion
    [SerializeField] private List<Pool> poolsofObjects;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
	// Use this for initialization
	void Start ()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        foreach (Pool pool in poolsofObjects)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.maimumSize; i++)
            {
                GameObject obj = Instantiate(pool.prefabObject);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tagText, objectPool);
        }
	}

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }



        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
