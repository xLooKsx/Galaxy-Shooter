using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private float _coolDownTime = 0.25f;

    [SerializeField]
    private GameObject _explosion;

    private float _nextShotIn = 0;
    public bool canUseTripleShot = false;
    public int lifeCount = 3;
    void Start()
    {
        transform.position = new Vector3(0, -1.75f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement();
        restrictPlayerMovementOnTheEdges();
        shotLaser();
        checkLife();
    }

    private void checkLife()
    {
        if(lifeCount == 0)
        {
            Instantiate(_explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    private void shotLaser()
    {
        bool canShot = checkIfCanShot();

        if (canUseTripleShot && canShot)
        {
            normalShot();
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else if (canShot)
        {
            normalShot();
        }

    }

    private bool checkIfCanShot()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse1)) && isCoolDownClear())
        {
            return true;
        }
        return false;
    }

    private void normalShot()
    {

        Vector3 laserPosition = transform.position + new Vector3(0, 0.96f, 0);
        Instantiate(_laserPrefab, laserPosition, Quaternion.identity);

    }

    private bool isCoolDownClear()
    {
        if (Time.time >= _nextShotIn)
        {
            _nextShotIn = Time.time + _coolDownTime;
            return true;
        }
        return false;
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

    private void playerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(getMovementValue(horizontalInput), getMovementValue(verticalInput), 0));
    }

    private float getMovementValue(float input)
    {
        return _speed * input * Time.deltaTime;
    }

    public void turnTripleShotPowerUpOn()
    {
        canUseTripleShot = true;
        StartCoroutine(tripleShotPowerUpOff());
    }

    public void turnSpeedPowerUpOn()
    {
        _speed *= 2f;
        StartCoroutine(speedPowerUpOff());
    }

    private IEnumerator tripleShotPowerUpOff()
    {
        yield return new WaitForSeconds(5f);
        canUseTripleShot = false;
    }

    private IEnumerator speedPowerUpOff()
    {
        yield return new WaitForSeconds(5f);
        canUseTripleShot = false;
        _speed = 5.0f;
    }
}
