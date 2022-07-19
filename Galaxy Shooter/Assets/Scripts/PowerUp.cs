using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;

    [SerializeField]
    private int powerUpId = 0;

    [SerializeField]
    private AudioClip _audioClip;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }

    private void movement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            selectPowerUp(player);
            AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position);
            Destroy(this.gameObject);
        }
    }

    private void selectPowerUp(Player player)
    {
        if(powerUpId == 0)
        {
            player.turnTripleShotPowerUpOn();
        }else if(powerUpId == 1)
        {
            player.turnSpeedPowerUpOn();
        }
        else if(powerUpId == 2)
        {
            player.turnShieldPowerUpOn();
        }

    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
