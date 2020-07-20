using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 5.5f;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleShotPrefab;
    [SerializeField] private float _fireRate = 0.3f;
    [SerializeField] private float _maxHealth = 3f;
    private SpawnManager _spawnManager;
    private float _canFire = 0.0f;
    private float _buffDuration = 5.0f;

    private bool _isTripleShotActive = false;


    // variable for isTripleShotActive

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>(); //find the gameObject and get the component
    
        if (_spawnManager == null)
        {
            Debug.LogError("Spawn manager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }

    }
    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(1, 0, 0) * horizontalInput * _speed * Time.deltaTime);
        transform.Translate(new Vector3(0, 1, 0) * verticalInput * _speed * Time.deltaTime);
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
                _buffDuration = _buffDuration - 1.0f;

                if (_buffDuration <= 0)
                {
                    _isTripleShotActive = false;
                }
            }
            else
            {
                Instantiate(_laserPrefab, new Vector3(transform.position.x, transform.position.y + 0.7f, 0), Quaternion.identity);
            }
            
            // if space key pressed
            // if tripleshotActive is true
                //fire 3 lasers for 5 seconds (_tripleShotPrefab)
            // Instantiate triple shot prefab
            //else fire one shot
        }
    }

    public void ActiveTripleShot()
    {
        _isTripleShotActive = true;
    }

    public void Damage()
    {
        _maxHealth = _maxHealth - 1;

        //check if dead
        //destroy player

        // communicate to the spawn manager that player is dead

        if (_maxHealth <= 0)
        {
            Destroy(this.gameObject);
            _spawnManager.OnPlayerDeath();
        }
    }
}
