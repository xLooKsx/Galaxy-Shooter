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

    [SerializeField]
    private bool _isPlayerOne = false;

    [SerializeField]
    private bool _isPlayerTwo = false;

    private int _hitCount = 0;
    private int _lastDamageSpriteId;
    private AudioSource _audioSoruce;
    private float _nextShotIn = 0;
    private GameManager _gameManager;   
    private UIManager _uiManager;

    public int lifeCount = 3;
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        _uiManager.updateLife(lifeCount);
        _audioSoruce = GetComponent<AudioSource>();
        _hitCount = 0;

    }

    // Update is called once per frame
    void Update()
    {
            playerMovement();
            shotLaser();
    }

    public void damageTaken()
    {
        if(_hitCount < 2 && !_canUseShield)
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
            _hitCount ++;
            _uiManager.updateLife(lifeCount);
            if (lifeCount == 0)
            {
                _gameManager.gameOver();            
                Instantiate(_explosion, transform.position, Quaternion.identity);                
                Destroy(this.gameObject);
            }
        }
        
    }

    private void getRandonSpriteDamage()
    {
       int currentId = UnityEngine.Random.Range(0, 2);
        currentId = currentId == _lastDamageSpriteId ? currentId == 1? 0: 1: 0;
           
        _damageExplosion[currentId].SetActive(true);
        _lastDamageSpriteId = currentId;

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
        if (Input.GetKeyDown(KeyCode.Space) && _isPlayerOne && isCoolDownClear())
        {
            return true;
        }else if (Input.GetKeyDown(KeyCode.Mouse1) && _isPlayerTwo && isCoolDownClear())
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
        if (_isPlayerOne)
        {
            getPlayerOneMovement();
        } else
        {
            getPlayerTwoMovement();
        }
        restrictPlayerMovementOnTheEdges();
    }

    private void getPlayerOneMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
        }
    }

    private void getPlayerTwoMovement()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
        } 
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
        }
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
