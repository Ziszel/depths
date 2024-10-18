using UnityEngine;

public class SceneManager : MonoBehaviour
{
    private static float _timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
    }

    public static float GetTimer()
    {
        return _timer;
    }
}
