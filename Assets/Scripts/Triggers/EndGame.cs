using UnityEngine;

public class EndGame : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.SetBestTime(SceneManager.GetTimer());
            GameManager.instance.LoadLevel("MainMenu");
        }
    }
}
