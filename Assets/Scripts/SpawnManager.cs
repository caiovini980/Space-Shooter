using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    //[SerializeField] private GameObject _tripleShotPrefab;
    //[SerializeField] private GameObject _speedBoostPrefab;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject[] _powerups;
    private IEnumerator _enemyCoroutine;
    private IEnumerator _powerupCoroutine;
    //private IEnumerator _speedBoostCoroutine;
    private bool _stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        _enemyCoroutine = SpawnEnemyRoutine(3.0f);
        StartCoroutine(_enemyCoroutine);

        _powerupCoroutine = SpawnPowerupRoutine(Random.Range(3.0f, 7.0f));
        StartCoroutine(_powerupCoroutine);

        /*_speedBoostCoroutine = SpawnSpeedBoostRoutine(Random.Range(10.0f, 15.0f));
        StartCoroutine(_speedBoostCoroutine);*/
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnEnemyRoutine(float _spawnTime)
    {
        while(_stopSpawning == false)
        {
            Vector3 positionToSpawn = new Vector3(Random.Range(-9.3f, 9.3f), 7.2f, 0);

            GameObject newEnemy = Instantiate(_enemyPrefab, positionToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_spawnTime);
        }
    }

    IEnumerator SpawnPowerupRoutine(float _powerupSpawnTime)
    {
        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(_powerupSpawnTime);

            int randomPowerup = Random.Range(0, 3);

            Vector3 positionToSpawn = new Vector3(Random.Range(-9.3f, 9.3f), 7.2f, 0);
            GameObject newPowerUp = Instantiate(_powerups[randomPowerup], positionToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }

    /*IEnumerator SpawnSpeedBoostRoutine(float _speedBoostSpawnTime)
    {
        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(_speedBoostSpawnTime);
            Vector3 positionToSpawn = new Vector3(Random.Range(-9.3f, 9.3f), 7.2f, 0);
            GameObject newPowerUp = Instantiate(_powerups[1], positionToSpawn, Quaternion.identity);

            yield return new WaitForSeconds(5.0f);
        }
    }*/

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
