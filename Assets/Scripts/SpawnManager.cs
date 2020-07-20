using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;
    private IEnumerator _coroutine;
    private bool _stopSpawning = false;
    // Start is called before the first frame update
    void Start()
    {
        _coroutine = SpawnRoutine(3f);
        StartCoroutine(_coroutine);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // spawn game objects every 5 seconds
    // create a coroutine of type IEnumerator -- Yield Events
    // while loop

    IEnumerator SpawnRoutine(float _spawnTime)
    {
        //while loop
            // instantiate enemy prefab
            // yield wait for 5 seconds
        while(_stopSpawning == false)
        {
            Vector3 positionToSpawn = new Vector3(Random.Range(-9.3f, 9.3f), 7.2f, 0);

            GameObject newEnemy = Instantiate(_enemyPrefab, positionToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_spawnTime);
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
