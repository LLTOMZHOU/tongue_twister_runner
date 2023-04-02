using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    public float levelLength = 30.0f;
    public ObstacleSpawner mObSpawn;
    public BGControl mBGControl;
    public float initGlobalSpeed = 2.0f;
    public float globalSpeedMin = 2.0f;
    public float globalSpeedMax = 5.0f;
    public float globalSpeedIncreaseRate = 0.05f;
    public float rankMin = 0.0f;
    public float rankMax = 10.0f;
    public float randIncreaseRate = 0.5f;
    public int sceneIdxMax = 0;
    public float transitTime = 5.0f;
    public float timer { get; private set; }
    public float globalSpeed { get; private set; }
    public float rank { get; private set; }
    public int sceneIdx { get; private set; }
    bool inTransit = true;
    // Start is called before the first frame update
    private void Awake()
    {
        timer = 0.0f;
        globalSpeed = initGlobalSpeed;
        rank = 0.0f;
        sceneIdx = 0;
    }
    void Start()
    {
        StartCoroutine(Transit());
    }

    // Update is called once per frame
    void Update()
    {
        if (!inTransit)
        {
            timer += Time.deltaTime;
            globalSpeed = Mathf.Min(globalSpeedIncreaseRate * Time.deltaTime + globalSpeed, globalSpeedMax);
            rank = Mathf.Min(randIncreaseRate * Time.deltaTime + rank, rankMax);
            if (timer > levelLength)
            {
                timer = 0.0f;
                NextScene();
            }
        }
    }

    void NextScene()
    {
        rank = Mathf.Max(rankMin, rank - 1);
        globalSpeed = Mathf.Max(globalSpeedMin, globalSpeed - 1);
        mObSpawn.StopSpawn();
        inTransit = true;
        sceneIdx++;
        if (sceneIdx >= sceneIdxMax)
        {
            //End the Game
        }
        else
        {
            StartCoroutine(Transit());
        }
    }

    IEnumerator Transit()
    {
        yield return new WaitForSeconds(transitTime);
        mObSpawn.StartSpawn();
        inTransit = false;
    }
}
