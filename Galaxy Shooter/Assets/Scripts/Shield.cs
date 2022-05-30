using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;

    [SerializeField]
    private GameObject _shieldAnimation;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(_shieldAnimation, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }

    private void movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(getMovementValue(horizontalInput), getMovementValue(verticalInput), 0));
        restrictPlayerMovementOnTheEdges();
    }

    private float getMovementValue(float input)
    {
        return _speed * input * Time.deltaTime;
    }

    private void restrictPlayerMovementOnTheEdges()
    {
        restrictPlayerMovementOnX();
        restrictPlayerMovementOnY();
    }
    private void restrictPlayerMovementOnY()
    {
        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.15)
        {
            transform.position = new Vector3(transform.position.x, -4.14f, 0);
        }
    }

    private void restrictPlayerMovementOnX()
    {
        if (transform.position.x > 8.68)
        {
            transform.position = new Vector3(-8.63f, transform.position.y, 0);
        }
        else if (transform.position.x < -8.64f)
        {
            transform.position = new Vector3(8.67f, transform.position.y, 0);
        }
    }
}
