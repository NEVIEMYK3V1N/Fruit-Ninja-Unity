using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject[] objsToSpawn;
    public GameObject bomb;
    public Transform[] spawnPoints;
    public float minWait = 0.3f;
    public float maxWait = 1f;    
    public float destroyTime = 5f;
    public float minForce = 12f;
    public float maxForce = 14f;
    public float bombSpawnPercentage = 10;
    public float minTorque = 1f;
    public float maxTorque = 2f;

    public float bonusMinWait = 0.1f;
    public float bonusMaxWait = 0.3f;
    public double duration = 15.0;

    private bool isBonus = false;
    private double bonusStartTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnStuff());
    }

    private IEnumerator SpawnStuff()
    {
        while(true)
        {
            GameObject obj = null;
            Transform t = null;

            if (!isBonus)
            {
                yield return new WaitForSeconds(Random.Range(minWait, maxWait));

                t = spawnPoints[Random.Range(0, spawnPoints.Length)];

                float p = Random.Range(0, 100);
                if (p < bombSpawnPercentage)
                {
                    obj = bomb;
                }
                else
                {
                    obj = objsToSpawn[Random.Range(0, objsToSpawn.Length)];
                }
            }
            else
            {
                yield return new WaitForSeconds(Random.Range(bonusMinWait, bonusMaxWait));

                t = spawnPoints[Random.Range(0, spawnPoints.Length)];

                obj = objsToSpawn[Random.Range(0, objsToSpawn.Length)];
            }
            
            GameObject fruit = Instantiate(obj, t.position, obj.transform.rotation);

            fruit.GetComponent<Rigidbody2D>().AddForce(t.transform.up * Random.Range(minForce, maxForce), 
                ForceMode2D.Impulse);

            fruit.GetComponent<Rigidbody2D>().AddTorque(Random.Range(minTorque, maxTorque),
                ForceMode2D.Impulse);

            // Debug.Log("Fruit gets spawned");

            Destroy(fruit, destroyTime);

            if (isBonus && Time.timeAsDouble - bonusStartTime > duration)
            {
                isBonus = false;
                FindObjectOfType<GameManager>().updateScoreBefore();
                Debug.Log("Bonus ends");
            }

            if (!isBonus && FindObjectOfType<GameManager>().checkForBonus())
            {
                isBonus = true;
                bonusStartTime = Time.timeAsDouble;
                Debug.Log("Triggered Bonus");
            }
        }
    }

    /*private IEnumerator BonusFruitBlossom(float startTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(bonusMinWait, bonusMaxWait));

            Transform t = spawnPoints[Random.Range(0, spawnPoints.Length)];

            GameObject obj = null;
            obj = objsToSpawn[Random.Range(0, objsToSpawn.Length)];

            GameObject fruit = Instantiate(obj, t.position, obj.transform.rotation);

            fruit.GetComponent<Rigidbody2D>().AddForce(t.transform.up * Random.Range(minForce, maxForce),
                ForceMode2D.Impulse);

            fruit.GetComponent<Rigidbody2D>().AddTorque(Random.Range(minTorque, maxTorque),
                ForceMode2D.Impulse);

            // Debug.Log("Fruit gets spawned");

            Destroy(fruit, destroyTime);

            int score = FindObjectOfType<GameManager>().getScore();
            if (Time.time - startTime > duration)
            {
                break;
            }
        }
        StopCoroutine(BonusFruitBlossom(Time.time));
        StartCoroutine(SpawnStuff());
    }*/




}
