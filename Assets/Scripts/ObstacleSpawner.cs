using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public LevelControl levelControl;
    public float scaleUp = 1.5f;
    [Serializable]
    public struct SceneList
    {
        [SerializeField]
        public List<SpawnInfo> hitables;
        [SerializeField]
        public List<SpawnInfo> nonHitables;
    }


    [Serializable]
    public struct SpawnInfo
    {
        public GameObject prefab;
        public Vector3 spawnPosUp;
        public Vector3 spawnPosDown;
        public bool movable;
    }
    public List<SceneList> sceneLists;
    public float minTime = 0.5f;
    public float timeRange = 3.0f;
    public int waitCountMax = 4;
    public float extraProb = 0.2f;
    public float extraSpeedMax = 2.0f;
    float timer = 0.0f;
    bool isSpawning = false;
    int waitCounter = 0;
    System.Random rand;
    // Start is called before the first frame update
    void Start()
    {
        rand = new System.Random();
        waitCounter = waitCountMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpawning)
        {
            timer += Time.deltaTime;
            if (timer > minTime + timeRange * (levelControl.rankMax - levelControl.rank) / levelControl.rankMax)
            {
                timer = 0.0f;
                Spawn(minTime + timeRange * (levelControl.rankMax - levelControl.rank) / levelControl.rankMax, Mathf.Max(levelControl.rank / levelControl.rankMax * 0.4f, 0.5f));
            }
        }
    }

    public void StartSpawn()
    {
        isSpawning = true;
        waitCounter = waitCountMax;
    }

    public void StopSpawn()
    {
        isSpawning = false;
        timer = 0.0f;
    }

    void Spawn(float timeRange, float prob)
    {
        
        int type = rand.Next(3);
        int isHit = rand.Next(2);
        int hitIdx = rand.Next(sceneLists[levelControl.sceneIdx].hitables.Count);
        int nonHitIdx = rand.Next(sceneLists[levelControl.sceneIdx].nonHitables.Count);
        Obstacle ob = null;
        Obstacle ob2 = null;
        if (type == 2)
        {
            waitCounter = 0;
        }
        else
        {
           if (waitCounter >= waitCountMax)
            {
                type = 2;
                waitCounter = 0;
            }
            else
            {
                waitCounter++;
            }
        }
        bool movable = false;
        bool movable2 = false;
        switch (type)
        {
            case 0:
                if (isHit == 0)
                {
                    ob = Instantiate(sceneLists[levelControl.sceneIdx].hitables[hitIdx].prefab, sceneLists[levelControl.sceneIdx].hitables[hitIdx].spawnPosUp, Quaternion.identity).GetComponent<Obstacle>();
                    movable = sceneLists[levelControl.sceneIdx].hitables[hitIdx].movable;
                }
                else
                {
                    ob = Instantiate(sceneLists[levelControl.sceneIdx].nonHitables[nonHitIdx].prefab, sceneLists[levelControl.sceneIdx].nonHitables[nonHitIdx].spawnPosUp, Quaternion.identity).GetComponent<Obstacle>();
                    movable = sceneLists[levelControl.sceneIdx].nonHitables[nonHitIdx].movable;
                }
                break;
            case 1:
                if (isHit == 0)
                {
                    ob = Instantiate(sceneLists[levelControl.sceneIdx].hitables[hitIdx].prefab, sceneLists[levelControl.sceneIdx].hitables[hitIdx].spawnPosDown, Quaternion.identity).GetComponent<Obstacle>();
                    movable = sceneLists[levelControl.sceneIdx].hitables[hitIdx].movable;
                }
                else
                {
                    ob = Instantiate(sceneLists[levelControl.sceneIdx].nonHitables[nonHitIdx].prefab, sceneLists[levelControl.sceneIdx].nonHitables[nonHitIdx].spawnPosDown, Quaternion.identity).GetComponent<Obstacle>();
                    movable = sceneLists[levelControl.sceneIdx].nonHitables[nonHitIdx].movable;
                }
                break;
            case 2:
                if (isHit == 0)
                {
                    ob = Instantiate(sceneLists[levelControl.sceneIdx].hitables[hitIdx].prefab, sceneLists[levelControl.sceneIdx].hitables[hitIdx].spawnPosUp, Quaternion.identity).GetComponent<Obstacle>();
                    movable = sceneLists[levelControl.sceneIdx].hitables[hitIdx].movable;
                    ob2 = Instantiate(sceneLists[levelControl.sceneIdx].nonHitables[nonHitIdx].prefab, sceneLists[levelControl.sceneIdx].nonHitables[nonHitIdx].spawnPosDown, Quaternion.identity).GetComponent<Obstacle>();
                    movable2 = sceneLists[levelControl.sceneIdx].nonHitables[nonHitIdx].movable;
                }
                else
                {
                    ob = Instantiate(sceneLists[levelControl.sceneIdx].nonHitables[nonHitIdx].prefab, sceneLists[levelControl.sceneIdx].nonHitables[nonHitIdx].spawnPosUp, Quaternion.identity).GetComponent<Obstacle>();
                    movable = sceneLists[levelControl.sceneIdx].nonHitables[nonHitIdx].movable;
                    ob2 = Instantiate(sceneLists[levelControl.sceneIdx].hitables[hitIdx].prefab, sceneLists[levelControl.sceneIdx].hitables[hitIdx].spawnPosDown, Quaternion.identity).GetComponent<Obstacle>();
                    movable2 = sceneLists[levelControl.sceneIdx].hitables[hitIdx].movable;
                }
                break;
            case 3:
                break;
        }
        
        // scale up
        if (ob != null)
        {
            ob.transform.localScale = new Vector3(ob.transform.localScale.x * scaleUp, ob.transform.localScale.y * scaleUp, ob.transform.localScale.z * scaleUp);
        }
        if (ob2 != null)
        {
            ob2.transform.localScale = new Vector3(ob2.transform.localScale.x * scaleUp, ob2.transform.localScale.y * scaleUp, ob2.transform.localScale.z * scaleUp);
        }
        if (movable)
        {
            ob.SetMoveSpeed(levelControl.globalSpeed + (float)(rand.NextDouble()) * extraSpeedMax);
            if (ob2 != null && movable2)
            {
                ob2.SetMoveSpeed(levelControl.globalSpeed + (float)(rand.NextDouble() * extraSpeedMax));
            }
        }
        float sample = (float)rand.NextDouble();
        if (sample <= prob)
        {
            float waitTime = (float)rand.NextDouble() * timeRange;
            StartCoroutine(WaitAndSpawn(waitTime, timeRange - waitTime, 0));
        }
        
    }

    IEnumerator WaitAndSpawn(float waitTime, float timeRange, float prob)
    {
        yield return new WaitForSeconds(waitTime);
        Spawn(timeRange, prob);
    }
}
