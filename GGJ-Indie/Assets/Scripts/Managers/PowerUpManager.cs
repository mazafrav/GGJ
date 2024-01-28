using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    private GameManager gm;
    private bool spawned = false;
    public bool hasKilled = false;
    [SerializeField] private List<Transform> spawnPointsGO;

    Player player;

    [SerializeField] private GameObject[] powerUpsGO;
    private List<GameObject> powerUpSpawned;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        powerUpSpawned = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm != null)
        {
            if (gm.GetProgression() == 17 && !spawned)
            {
                SpawnPowerUps();
            }
            else if (gm.GetProgression() == 30 && !spawned)
            {
                SpawnPowerUps();
            }
            else if (gm.GetProgression() == 60 && !spawned)
            {
                SpawnPowerUps();

            }

        }
    }

    IEnumerator SpawnReset()
    {
        yield return new WaitUntil(() => hasKilled == true);
        spawned = false;
    }

    List<Vector3> getFurthestSpawnPoints(List<Transform> spawnPoints)
    {
        List<Vector3> furthestPoints = new List<Vector3>();
        Transform currentPoint= null;
        int index = -1;
        float maxDistance = 0;
        float currentDistance = 0;

        for (int i = 0; i < spawnPoints.Count; i++)
        {
            currentDistance = Vector3.Distance(spawnPoints[i].position, player.transform.position);
            if (currentDistance > maxDistance)
            {
                maxDistance = currentDistance;
                currentPoint = spawnPoints[i];
                index = i;
            }
        }
        furthestPoints.Add(currentPoint.position);
        spawnPoints.RemoveAt(index);

        currentDistance = 0;
        maxDistance = 0;

        foreach (Transform t in spawnPoints)
        {
            if (t != null)
            {
                currentDistance = Vector3.Distance(t.position, player.transform.position);
                if (currentDistance > maxDistance)
                {
                    maxDistance = currentDistance;
                    currentPoint = t;
                }
            }
        }
        furthestPoints.Add(currentPoint.position);
        return furthestPoints;
    }

    List<int> getRandomPowerUps()
    {
        List<int> result = new List<int>();
        int random1 = Random.Range(0, 3);
        int random2 = -1;
        int randomAux;
        switch(random1)
        {
            case 0:
                random2 = Random.Range(1, 3);
                break;
            case 1:
                randomAux= Random.Range(1, 3);
                if (randomAux == 0)
                {
                    random2 = 0;
                }
                else
                {
                    random2 = 2;
                }
                break;
            case 2:
                random2 = Random.Range(0, 2);
                break;
        }
        result.Add(random1);
        result.Add(random2);
        return result;
    }

    void SpawnPowerUps()
    {
        List<Vector3> spawnPoints = getFurthestSpawnPoints(spawnPointsGO);

        List<int> powerUps = getRandomPowerUps();
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            GameObject go = Instantiate(powerUpsGO[powerUps[i]], spawnPoints[i], Quaternion.identity);
            powerUpSpawned.Add(go);
        }
        spawned = true;
        hasKilled = false;
        StartCoroutine(SpawnReset());
    }

    public void PickPowerUp()
    {
        foreach (GameObject go in powerUpSpawned)
        {
            if (go != null)
            {
                Destroy(go.gameObject);
            }
        }
        powerUpSpawned.Clear();
    }
}
