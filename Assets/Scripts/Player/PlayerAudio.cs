using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] private AudioSource sfxSource;
    
    [Header("Audio clips")]
    [SerializeField] private AudioClip _footstep;
    [SerializeField] private AudioClip _death;

    private void Start()
    {
        sfxSource.clip = _footstep;
    }
    
    public void PlaySfx()
    {
        if (!sfxSource.isPlaying)
        {
            sfxSource.Play();
        }
    }

    public void PlayDeathSound()
    {
        sfxSource.PlayOneShot(_death);
    }
}
