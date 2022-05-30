using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyShip;

    [SerializeField]
    private GameObject _playerPrefab;

    [SerializeField]
    private GameObject[] powerUps;

    private Player player;

    public void initializeComponents()
    {
        Instantiate(_playerPrefab, new Vector3(0, -1.75f, 0), Quaternion.identity);
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
