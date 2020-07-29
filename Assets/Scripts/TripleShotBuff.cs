using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShotBuff : MonoBehaviour
{
    [SerializeField] private float _buffSpeed = 3.0f;
    [SerializeField] private int _powerUpID; // 0 = triple shots; 1 = speed; 2 = shields
    private AudioManager _audioManager;

    // Start is called before the first frame update
    void Start()
    {
        _audioManager = GameObject.Find("Audio_Manager").GetComponent<AudioManager>();

        if(_audioManager == null)
        {
            Debug.LogError("AudioManager is NULL");
        }
        transform.position = new Vector3(Random.Range(-9.3f, 9.3f), 7.2f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, -1, 0) * _buffSpeed * Time.deltaTime);
        
        if (transform.position.y <= -5.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();

            if (player != null)
            {
                switch (_powerUpID)
                {
                    case 0:
                        player.ActiveTripleShot();
                        _audioManager.PlayPowerUpAudio();
                        Destroy(this.gameObject);
                        break;

                    case 1:
                        player.ActiveSpeedBoost();
                        _audioManager.PlayPowerUpAudio();
                        Destroy(this.gameObject);
                        break;

                    case 2:
                        player.ActiveShield();
                        _audioManager.PlayPowerUpAudio();
                        Destroy(this.gameObject);
                        break;

                    default:
                        Debug.Log("Default value");
                        break;
                }
            }
        }
    }
}
