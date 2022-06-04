using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private GameObject _shieldGameObject;

    [SerializeField]
    private GameObject _explosion;

    [SerializeField]
    private float _speed = 5.0f;

    [SerializeField]
    private float _coolDownTime = 0.25f;

    [SerializeField]
    private bool _canUseTripleShot = false;

    [SerializeField]
    private bool _canUseShield = false;

    [SerializeField]
    private GameObject[] _damageExplosion;

    private int hitCount = 0;
    private int lastDamageSpriteId;
    private AudioSource _audioSoruce;
    private float _nextShotIn = 0;
    private GameObject _shield;
    public int lifeCount = 3;
    

    private UIManager uiManager;
    void Start()
    {
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        uiManager.updateLife(lifeCount);
        _audioSoruce = GetComponent<AudioSource>();
        hitCount = 0;

    }

    // Update is called once per frame
    void Update()
    {
            playerMovement();
            shotLaser();
    }

    public void damageTaken()
    {
        if(hitCount < 2)
        {
            getRandonSpriteDamage();
        }

        if (_canUseShield)
        {
            _canUseShield = false;
            _shieldGameObject.SetActive(false);
        }
        else if(lifeCount > 0)
        {
            lifeCount--;
            hitCount ++;
            uiManager.updateLife(lifeCount);
            if (lifeCount == 0)
            {
                uiManager.gameOver();
                Instantiate(_explosion, transform.position, Quaternion.identity);                
                Destroy(this.gameObject);
            }
        }
        
    }

    private void getRandonSpriteDamage()
    {
       int currentId = UnityEngine.Random.Range(0, 2);
        currentId = currentId == lastDamageSpriteId ? currentId == 1? 0: 1: 0;
           
        _damageExplosion[currentId].SetActive(true);
        lastDamageSpriteId = currentId;

    }

    private void shotLaser()
    {
        bool canShot = checkIfCanShot();

        if (_canUseTripleShot && canShot)
        {
            normalShot();
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            _audioSoruce.Play();
        }
        else if (canShot)
        {
            normalShot();
            _audioSoruce.Play();
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
        restrictPlayerMovementOnTheEdges();
    }

    private float getMovementValue(float input)
    {
        return _speed * input * Time.deltaTime;
    }

    public void turnTripleShotPowerUpOn()
    {
        _canUseTripleShot = true;
        StartCoroutine(tripleShotPowerUpOff());
    }

    public void turnSpeedPowerUpOn()
    {
        _speed *= 2f;
        StartCoroutine(speedPowerUpOff());
    }

    public void turnShieldPowerUpOn()
    {
        _canUseShield = true;
        _shieldGameObject.SetActive(true);
        StartCoroutine(shieldPowerUpOff());
    }

    private IEnumerator tripleShotPowerUpOff()
    {
        yield return new WaitForSeconds(5f);
        _canUseTripleShot = false;
    }

    private IEnumerator speedPowerUpOff()
    {
        yield return new WaitForSeconds(5f);
        _canUseTripleShot = false;
        _speed = 5.0f;
    }

    private IEnumerator shieldPowerUpOff()
    {
        yield return new WaitForSeconds(10f);
        _shieldGameObject.SetActive(false);
        _canUseShield = false;
    }
}
