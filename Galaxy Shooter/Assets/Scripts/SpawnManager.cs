using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyShip;

    [SerializeField]
    private GameObject[] powerUps;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(enemySpawn());
        StartCoroutine(powerUpSpawn());
    }

    private IEnumerator enemySpawn()
    {
        while (true)
        {
            
            float randoXPosition = UnityEngine.Random.Range(-7.44f, 7.44f);
            transform.position = new Vector3(randoXPosition, 6.1f, 0);
            Instantiate(_enemyShip, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(2f);
        }
    }

    private IEnumerator powerUpSpawn()
    {
        while (true)
        {

            float randoXPosition = UnityEngine.Random.Range(-7.44f, 7.44f);
            int randonPowerUp = UnityEngine.Random.Range(0, 3);
            transform.position = new Vector3(randoXPosition, 6.1f, 0);
            Instantiate(powerUps[randonPowerUp], transform.position, Quaternion.identity);
            yield return new WaitForSeconds(6f);
        }
    }
}
