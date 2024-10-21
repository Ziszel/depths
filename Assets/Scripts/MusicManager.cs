using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    [Header("Audio Source")]
    private AudioSource _musicSource;

    private float fadeDuration = 3.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _musicSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void TriggerFadeOutMusic()
    {
        StartCoroutine(FadeOutMusic());
    }

    private IEnumerator FadeOutMusic()
    {
        float startVolume = _musicSource.volume;

        // Gradually reduce the volume
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            _musicSource.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
            yield return null;
        }

        // Ensure the volume is set to 0 at the end
        _musicSource.volume = 0;
        _musicSource.Stop(); // Optionally stop the music when it reaches 0 volume
    }
}
