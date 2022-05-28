using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveUp();
    }

    private void moveUp()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
    }

    void OnBecameInvisible()
    {
        if (transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
        }
        Destroy(gameObject);
    }
}
