using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    float wait = 3f;

    void Update()
    {
        if (wait > 0)
        {
            wait -= Time.deltaTime;
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("BarScene");
        }
    }
}
