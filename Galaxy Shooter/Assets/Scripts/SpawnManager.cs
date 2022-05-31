using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyShip;

    [SerializeField]
    private Player _player;

    [SerializeField]
    private GameObject[] powerUps;

    public void initializeComponents()
    {
       Player playerClone = Instantiate(_player, new Vector3(0, -1.75f, 0), Quaternion.identity);
        StartCoroutine(enemySpawn(playerClone));
        StartCoroutine(powerUpSpawn(playerClone));
    }

    private IEnumerator enemySpawn(Player playerClone)
    {
        while (playerClone.lifeCount > 0)
        {
                float randoXPosition = UnityEngine.Random.Range(-7.44f, 7.44f);
                transform.position = new Vector3(randoXPosition, 6.1f, 0);
                Instantiate(_enemyShip, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(2f);
            
        }
    }

    private IEnumerator powerUpSpawn(Player playerClone)
    {
        while (playerClone.lifeCount > 0)
        {
                float randoXPosition = UnityEngine.Random.Range(-7.44f, 7.44f);
                int randonPowerUp = UnityEngine.Random.Range(0, 3);
                transform.position = new Vector3(randoXPosition, 6.1f, 0);
                Instantiate(powerUps[randonPowerUp], transform.position, Quaternion.identity);
                yield return new WaitForSeconds(6f);
        }
    }
}
