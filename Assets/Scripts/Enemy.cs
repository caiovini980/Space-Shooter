using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _enemySpeed = 4.0f;
    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        float randomX = Random.Range(-9.3f, 9.3f);
        transform.position = new Vector3(randomX, 7.2f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, -1, 0) * _enemySpeed * Time.deltaTime);

        if (transform.position.y <= -5.5f)
        {
            float randomX = Random.Range(-9.3f, 9.3f);
            transform.position = new Vector3(randomX, 7.2f, 0);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Laser"))
        {
            //add 1 point to the player's score

            if (_player != null)
            {
                _player.AddScore(1);
            }

            Destroy(this.gameObject);
        }

        else if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }

            Destroy(this.gameObject);
        }
    }
}
