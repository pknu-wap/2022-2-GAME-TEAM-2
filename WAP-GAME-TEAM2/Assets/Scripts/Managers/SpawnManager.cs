using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    #region Singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public Dictionary<string, (Vector2, Vector2)> sceneTileDictionary;
    public string sceneName;

    public Vector2 spawnPoint;

    public AIController spawnTarget;
    public int chaserNumber;

    public bool chase;

    private bool chaseTimeOver;
    public float chaseTime;
    private float interTime;

    // Start is called before the first frame update
    void Start()
    {
        sceneTileDictionary = new Dictionary<string, (Vector2, Vector2)>();
        sceneTileDictionaryInit();

        interTime = 0f;
    }

    private void Update()
    {
        if (!chase)
            return;

        if (chaseTime <= interTime)
        {
            chaseTimeOver = true;
            return;
        }

        interTime += Time.deltaTime;
    }

    private void sceneTileDictionaryInit()
    {
        // Map size
        sceneTileDictionary.Add("1Floor", (new Vector2(-10.5f, -13.5f), new Vector2(12.5f, 8.5f)));
        sceneTileDictionary.Add("TeacherOffice", (new Vector2(-6.5f, -4.5f), new Vector2(12.5f, 6.5f)));
        sceneTileDictionary.Add("PrincipalOffice", (new Vector2(-6.5f, -4.5f), new Vector2(-0.5f, 5.5f)));

        sceneTileDictionary.Add("3Floor", (new Vector2(-13.5f, -6.5f), new Vector2(13.5f, 8.5f)));
        sceneTileDictionary.Add("ToiletF(3F)", (new Vector2(-5.5f, -4.5f), new Vector2(5.5f, 0.5f)));
        sceneTileDictionary.Add("ComputerRoom", (new Vector2(-7.5f, -4.5f), new Vector2(10.5f, 6.5f)));
        sceneTileDictionary.Add("2-1", (new Vector2(-7.5f, -4.5f), new Vector2(10.5f, 6.5f)));
        sceneTileDictionary.Add("2-2", (new Vector2(-7.5f, -8.5f), new Vector2(4.5f, 1.5f)));
        sceneTileDictionary.Add("BroadcastRoom", (new Vector2(-4.5f, -0.5f), new Vector2(4.5f, 4.5f)));
        sceneTileDictionary.Add("BroadcastR2", (new Vector2(-3.5f, 6.5f), new Vector2(4.5f, 11.5f)));

        sceneTileDictionary.Add("Library", (new Vector2(-7.5f, 11.5f), new Vector2(6.5f, 6.5f)));
        sceneTileDictionary.Add("FurnishingRoom", (new Vector2(-4.5f, -3.5f), new Vector2(5.5f, 1.5f)));

    }

    public void StartSpawnCoroutine()
    {
        StartCoroutine(SpawnCoroutine());
    }

    // 씬 이동마다 호출
    IEnumerator SpawnCoroutine()
    {
        if (!chase)
            yield break;

        if (chaseTimeOver)
        {
            int rand = Random.Range(0, 3);
            if (rand == 0)
            {
                chase = false;
                yield break;
            }
        }

        GameObject spawnObject = GameObject.Find("Chasers");
        spawnTarget = spawnObject.transform.Find("Chaser" + chaserNumber.ToString()).gameObject.GetComponent<AIController>();

        spawnTarget.bottomLeft = sceneTileDictionary[sceneName].Item1;
        spawnTarget.topRight = sceneTileDictionary[sceneName].Item2;
        spawnTarget.transform.position = spawnPoint;

        yield return new WaitForSeconds(2f);

        spawnTarget.gameObject.SetActive(true);
        spawnTarget.chase = true;
    }

    public void StartChase(AIController _chaser)
    {
        chase = true;
        spawnTarget = _chaser;
        spawnTarget.bottomLeft = sceneTileDictionary[sceneName].Item1;
        spawnTarget.topRight = sceneTileDictionary[sceneName].Item2;
    }
}
