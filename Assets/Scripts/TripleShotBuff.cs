using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShotBuff : MonoBehaviour
{
    [SerializeField] private float _buffSpeed = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Random.Range(-9.3f, 9.3f), 7.2f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, -1, 0) * _buffSpeed * Time.deltaTime);
        
        if (transform.position.y <= -5.5f)
        {
            transform.position = new Vector3(Random.Range(-9.3f, 9.3f), 7.2f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();

            if (player != null)
            {
                player.ActiveTripleShot();
                Destroy(gameObject);
            }
        }
    }
}
