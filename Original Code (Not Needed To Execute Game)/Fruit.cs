using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject slicedFruitPrefab;
    public float expRadius = 2f;
    public float expFmin = 300f;
    public float expFmax = 600f;
    public float destroyTime = 5f;
    public int points = 3;

    public void CreateSlicedFruit()
    {
        GameObject inst = (GameObject)Instantiate(slicedFruitPrefab, transform.position, transform.rotation);

        Rigidbody[] rbsOnSliced = inst.transform.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rb in rbsOnSliced)
        {
            rb.transform.rotation = Random.rotation;
            float expForce = Random.Range(expFmin, expFmax);
            rb.AddExplosionForce(expForce, transform.position, expRadius);
        }

        FindObjectOfType<GameManager>().IncreaseScore(points);

        Destroy(gameObject);
        Destroy(inst.gameObject, destroyTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Blade b = collision.GetComponent<Blade>();

        if (!b)
        {
            return;
        } 
        else
        {
            CreateSlicedFruit();
        }
    }
}
