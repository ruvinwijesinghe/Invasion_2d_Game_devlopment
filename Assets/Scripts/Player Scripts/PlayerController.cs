using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region variables
    public GameObject centerLaserPrefab, tripleShotLaserPrefab;
    public GameObject shieldsGameObject;

    [SerializeField]
    private GameObject[] _sideThrusters;

    public int lives = 3;
    private int _hitCount;

    public float moveSpeed = 3f;

    private float _horizontalInput, _verticalInput;

    private float _xBound = 8.8f, _yBound = 4.0f;

    private Vector3 _offset = new Vector3(0, 1, 0);

    private AudioSource _audioSource;

    // COOL DOWN SYSTEM
    public float fireRate = 0.25f;

    private float _nextFire = 0f;

    public bool hasTripleShotPowerup = false;
    public bool isSpeedBoostActive = false;
    public bool isShieldPowerupActive = false;

    private UIManager _uiManager;
    private GameManager _gameManager;
    #endregion  

    private void Awake()
    {
        // handle to the UIManager
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        // handle to the GameManager
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        _audioSource = GetComponent<AudioSource>();

    }// Awake

    private void Start()
    {
        //transform.position = new Vector3(0, 0, 0);

        if(_uiManager != null)
        {
            _uiManager.UpdateLives(lives);
        }

        _hitCount = 0;
  
    }//Start
    
    private void Update()
    {
        ProcessInput();
        MovePlayer();

        PlayerBounds();

        PlayerShoot();

    }//Update

    private void ProcessInput()
    {
        // proecess input
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
    }//ProcessInput

    private void MovePlayer()
    {
        // if speed boost enabled
        if(isSpeedBoostActive == true)
        {
            // move player (1.5x the normal speed)                  
            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed * 2.0f * _horizontalInput);
            transform.Translate(Vector3.up * Time.deltaTime * moveSpeed * 2.0f * _verticalInput);
        }
        else
        {
            // move player (normal speed)                  
            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed * _horizontalInput);
            transform.Translate(Vector3.up * Time.deltaTime * moveSpeed * _verticalInput);
        }

    }//MovePlayer

    private void PlayerBounds()
    {
        // player bounds
        if (transform.position.x >= _xBound)
        {
            transform.position = new Vector3(-_xBound, transform.position.y, 0);
        }
        else if (transform.position.x <= -_xBound)
        {
            transform.position = new Vector3(_xBound, transform.position.y, 0);
        }

        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -_yBound)
        {
            transform.position = new Vector3(transform.position.x, -_yBound, 0);
        }
    }//PlayerBounds

    private void PlayerShoot()
    {
        if (Input.GetMouseButtonDown(0))
        {

            // play laser sound
            _audioSource.Play();

            if(Time.time > _nextFire)
            {
                if (hasTripleShotPowerup)
                {
                    Instantiate(tripleShotLaserPrefab, transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(centerLaserPrefab, transform.position + _offset, Quaternion.identity);
                }

                _nextFire = Time.time + fireRate;           
            }
            
        }
    }//PlayerShoot

    public void Damage()
    {
        // if the player has shields
        // do nothing

        if(isShieldPowerupActive == true) // if the player has shiedls
        {
            isShieldPowerupActive = false;
            shieldsGameObject.SetActive(false);
            return;
            //------------------------------------------------------------ will not execute
        }

        _hitCount++;

        if(_hitCount == 1)
        {
            _sideThrusters[0].SetActive(true);

        }else if(_hitCount == 2)
        {
            _sideThrusters[1].SetActive(true);
        }

        // subtract 1 life from the player
        lives--;

        _uiManager.UpdateLives(lives);

        // if lives < 1 (meaning 0)
        // destroy this object
        if(lives < 1)
        {
            lives = 0;

            _gameManager.isGameOver = true;
            _uiManager.ShowTitleScreen();

            Destroy(this.gameObject);
        }
    }// Damage

    public void EnableShields()
    {
        isShieldPowerupActive = true;
        shieldsGameObject.SetActive(true);
    }// EnableShields

    public void SpeedBoostPowerupOn()
    {
        isSpeedBoostActive = true;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }//SpeedBoostPowerupOn

    public void TripleShotPowerOn()
    {
        hasTripleShotPowerup = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }//TripleShotPowerOn

    public IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isSpeedBoostActive = false;
    }//SpeedBoostPowerDownRoutine

    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        hasTripleShotPowerup = false;
    }//TripleShotPowerDownRoutine

}//class
