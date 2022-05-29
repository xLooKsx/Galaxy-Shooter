using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private float _speed = 7f;

    [SerializeField]
    private GameObject destructionAnimation;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(-5.04f, 6.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            subtractPlayerLife(other);
            selfDestruction();
        }
        else if (other.tag == "Laser")
        {
            destroyLaser(other);
            selfDestruction();
        }
    }

    private void subtractPlayerLife(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        player.lifeCount--;
    }

    private void destroyLaser(Collider2D other)
    {
        if (transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
        }
        Destroy(other.gameObject);
    }

    private void selfDestruction()
    {
        Instantiate(destructionAnimation, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    private void move()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        checkMovement();
    }

    private void checkMovement()
    {
        if (transform.position.y < -5.97)
        {
            float randoXPosition = UnityEngine.Random.Range(-7.44f, 7.44f);
            transform.position = new Vector3(randoXPosition, 6.1f, 0);
        }
    }
}
