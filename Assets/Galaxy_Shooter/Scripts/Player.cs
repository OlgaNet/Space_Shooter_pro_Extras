using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    private float _speedMultiplier = 2;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 4;
    private SpawnManadger _spawnManadger;

    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive = false;
    private bool _isShieldsActive = false;
    public bool isPlayerOne = false;
    public bool isPlayerTwo = false;

    [SerializeField]
    private GameObject _shieldVisualizer;

    [SerializeField]
    private GameObject _leftEngine, _rightEngine;

    [SerializeField]
    private int _score;

    private UIManager _uiManager;
    [SerializeField]
    private GameManager _gameManager;

    [SerializeField]
    private AudioClip _laserSoundClip;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        //transform.position = new Vector3(0, 0, 0);
        //_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _spawnManadger = GameObject.Find("Spawn_Manadger").GetComponent<SpawnManadger>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();

        if (_spawnManadger == null)
        {
            Debug.LogError("The Spawn Manadger is NULL.");
        }

        if (_uiManager == null)
        {
            Debug.LogError("The UI Manadger is NULL.");
        }

        if (_audioSource == null)
        {
            Debug.LogError("AudioSource on the player is NULL.");
        }
        else
        {
            _audioSource.clip = _laserSoundClip;
        }

        if (_gameManager.isCoopMode == false)
        {
            transform.position = new Vector3(0, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if player1
        if (isPlayerOne == true)
        {
            CalculateMovement();
#if UNITY_ANDROID
        if (((Input.GetKeyDown(KeyCode.Space) || CrossPlatformInputManager.GetButtonDown("Fire")) && Time.time > _canFire) && isPlayerOne == true)
        {
            FireLaser();
        }
#elif UNITY_IOS
        if (((Input.GetKeyDown(KeyCode.Space) || CrossPlatformInputManager.GetButtonDown("Fire")) && Time.time > _canFire) && isPlayerOne == true)
        {
            FireLaser();
        }
#else
            //if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0)) && Time.time > _canFire)
            if ((Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire) && isPlayerOne == true)
            {
                FireLaser();
            }
#endif
        }

        //if player2
        if (isPlayerTwo == true)
        {
            CalculateMovementPlayerTwo();
            FireLaserPlayerTwo();
            /*if ((Input.GetKeyDown(KeyCode.Keypad8) && Time.time > _canFire) && isPlayerTwo == true)
            {
                FireLaser();
            }*/
        }

        
    }

    void CalculateMovement()
    {
#if UNITY_ANDROID
        float horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal_Player1");
        float verticalInput = CrossPlatformInputManager.GetAxis("Vertical_Player1");
#elif UNITY_IOS
        float horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal_Player1");
        float verticalInput = CrossPlatformInputManager.GetAxis("Vertical_Player1");
#else
        float horizontalInputPlayer1 = Input.GetAxis("Horizontal");
        float verticalInputPlayer1 = Input.GetAxis("Vertical");
#endif

        Vector3 directionPlayer1 = new Vector3(horizontalInputPlayer1, verticalInputPlayer1, 0);

        transform.Translate(directionPlayer1 * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void CalculateMovementPlayerTwo()
    {
#if UNITY_ANDROID
        float horizontalInputPlayer2 = CrossPlatformInputManager.GetAxis("Horizontal_Player2");
        float verticalInputPlayer2 = CrossPlatformInputManager.GetAxis("Vertical_Player2");
#elif UNITY_IOS
        float horizontalInputPlayer2 = CrossPlatformInputManager.GetAxis("Horizontal_Player2");
        float verticalInputPlayer2 = CrossPlatformInputManager.GetAxis("Vertical_Player2");
#else
        float horizontalInputPlayer2 = Input.GetAxis("Horizontal_Player2");
        float verticalInputPlayer2 = Input.GetAxis("Vertical_Player2");
#endif
        
        Vector3 directionPlayer2 = new Vector3(horizontalInputPlayer2, verticalInputPlayer2, 0);

        transform.Translate(directionPlayer2 * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
         _canFire = Time.time + _fireRate;

        if (_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }

        _audioSource.Play(); //play the laser audio clip
        
    }

    void FireLaserPlayerTwo()
    {
        _canFire = Time.time + _fireRate;

        if (_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }

        _audioSource.Play(); //play the laser audio clip

    }

    public void Damage()
    {
        //if shields is active
        //do nothing...
        //deactivate shields
        //return;
        if (_isShieldsActive == true)
        {
            _isShieldsActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }
        
        _lives --;

        //if lives is 2 enable right engine
        //if lives is 1 enable left engine
        if (_lives == 2)
        {
            _leftEngine.SetActive(true);
        }
        else if (_lives == 1)
        {
            _rightEngine.SetActive(true);
        }

        _uiManager.UpdateLives(_lives);


        if (_lives < 1)
        {
            _spawnManadger.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
        _speed /= _speedMultiplier;
    }

    public void ShieldsActive()
    {
        _isShieldsActive = true;
        _shieldVisualizer.SetActive(true);
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
    //method to add 10 to the score!
    //communicate with the UI to update the score!
}
