using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorSceneTrigger : MonoBehaviour
{
    public string sceneName;
    public bool isCorrectDoor = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (isCorrectDoor)
            {
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                Debug.Log("Wrong door!");
            }
        }
    }
}