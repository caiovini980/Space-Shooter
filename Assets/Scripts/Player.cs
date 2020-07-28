using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _shield;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleShotPrefab;
    [SerializeField] private float _fireRate = 0.3f;
    [SerializeField] private int _maxHealth = 3;
    [SerializeField] private float _speed = 5.5f;
    [SerializeField] private int _score;
     
    private SpawnManager _spawnManager;
    private UIManager _uiManager;
    private float _canFire = 0.0f;
    private float _speedMultiplier = 2.0f;
    //private float _buffDuration = 5.0f;
    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive = false;

    public bool isShieldActive = false;

    //variable reference to the shield visualizer
     


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>(); //find the gameObject and get the component
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("Spawn manager is NULL");
        }

        if (_uiManager == null)
        {
            Debug.LogError("UI Manager is NULL");
        }

        _shield.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }

         if(Input.GetKeyDown(KeyCode.F) && _maxHealth > 0) 
        {
            this.Damage();
        }

    }
    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(1, 0, 0) * horizontalInput * _speed * Time.deltaTime);
        transform.Translate(new Vector3(0, 1, 0) * verticalInput * _speed * Time.deltaTime);

        /*if (_isSpeedBoostActive == true)
        {
            transform.Translate(new Vector3(1, 0, 0) * horizontalInput * _speedBoost * Time.deltaTime);
            transform.Translate(new Vector3(0, 1, 0) * verticalInput * _speedBoost * Time.deltaTime);
        }*/
/*
        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }
*/
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x >= 11.2f)
        {
            transform.position = new Vector3(-11.2f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.2f)
        {
            transform.position = new Vector3(11.2f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            _canFire = Time.time + _fireRate;

            if (_isTripleShotActive == true)
            {
                Instantiate(_tripleShotPrefab, new Vector3(transform.position.x - 0.59f, transform.position.y + 0.4f, 0), Quaternion.identity);
               // _buffDuration = _buffDuration - 1.0f;

               // if (_buffDuration <= 0)
                //{
                //    _isTripleShotActive = false;
                //}
            }
            else
            {
                Instantiate(_laserPrefab, new Vector3(transform.position.x, transform.position.y + 0.7f, 0), Quaternion.identity);
            }
        }
    }

    public void ActiveTripleShot()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine(5.0f));
    }

    IEnumerator TripleShotPowerDownRoutine(float _buffTime)
    {
        yield return new WaitForSeconds(_buffTime);
        _isTripleShotActive = false;
    }

    public void ActiveSpeedBoost()
    {
        _isSpeedBoostActive = true;
        StartCoroutine(SpeedBoostDownCoroutine(5.0f));
        _speed = _speed * _speedMultiplier;
    }

    IEnumerator SpeedBoostDownCoroutine(float _buffTime)
    {
        yield return new WaitForSeconds(_buffTime);
        _isSpeedBoostActive = false;
        _speed = _speed / _speedMultiplier;
    }

    public void ActiveShield()
    {
        isShieldActive = true;
        _shield.SetActive(true);
        // enable shield
    }

    public void Damage()
    {
        if (isShieldActive == true)
        {
            isShieldActive = false;
            _shield.SetActive(false);
            // disable shield
            return;
            
        }
        else
        {
            _maxHealth = _maxHealth - 1;
            _uiManager.UpdateLives(_maxHealth);
        }

        //if shield is active
        //do nothing "return;"
        //deactivate shields

        if (_maxHealth <= 0)
        {
            Destroy(this.gameObject);
            _spawnManager.OnPlayerDeath();
        }
    }

    public void AddScore(int points)
    {
        _score = _score + points;
        _uiManager.UpdateScore(_score);
    }
    //method to add 1 to the score
    //communicate to the ui to add 1 to the score

}
