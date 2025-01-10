using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeScene(int scene)
    {
        Debug.Log("${SceneManager.GetActiveScene().buildIndex}");
        if (scene != SceneManager.GetActiveScene().buildIndex)
        {
            SceneManager.LoadScene(scene);
        }

    }
}