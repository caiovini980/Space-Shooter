using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AudioSource _laserSource;
    [SerializeField] private AudioSource _explosionSource;
    [SerializeField] private AudioSource _powerupSource;

    public void PlayLaserAudio()
    {
        _laserSource.Play();
    }

    public void PlayExplosionAudio()
    {
        _explosionSource.Play();
    }

    public void PlayPowerUpAudio()
    {
        _powerupSource.Play();
    }
}
