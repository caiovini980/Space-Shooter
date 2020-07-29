using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    //set speed
    [SerializeField] private float _asteroidSpeed = 2.0f;
    [SerializeField] private GameObject _explosionPrefab;
    private SpawnManager _spawnManager;
    private AudioManager _audioManager;
    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _audioManager = GameObject.Find("Audio_Manager").GetComponent<AudioManager>();

        if(_player == null)
        {
            Debug.LogError("Player is NULL");
        }
        
        if(_spawnManager == null)
        {
            Debug.LogError("SpawnManager is NULL");
        }
        
        if(_audioManager == null)
        {
            Debug.LogError("AudioManager is NULL");
        }
        float randomX = Random.Range(-9.3f, 9.3f);
        transform.position = new Vector3(randomX, 7.2f, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //rotate the gameObject
        transform.Rotate(new Vector3(0, 0, 1) * _asteroidSpeed * Time.deltaTime);
        transform.Translate(new Vector3(0, -1, 0) * _asteroidSpeed * Time.deltaTime);

        if (transform.position.y <= -5.5f)
        {
            float randomX = Random.Range(-9.3f, 9.3f);
            transform.position = new Vector3(randomX, 7.2f, 0);
        }
        
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        if(other.gameObject.CompareTag("Laser"))
        {
            if (_player != null)
            {
                Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                _audioManager.PlayExplosionAudio();
                _spawnManager.StartSpawning();
                Destroy(this.gameObject, 0.18f);
            }
        }
    }
}
