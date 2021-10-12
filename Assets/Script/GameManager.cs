
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private void Start()
    {
        Screen.SetResolution(1920, 1080, false);
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        if(Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        ChangeScene();
    }

    void ChangeScene()
    {
        //タイトル
        if(SceneManager.GetActiveScene().name == "Title")
        {
            if(Input.GetKey(KeyCode.Return)){
                SceneManager.LoadScene("Play");
            }
        }

        //プレイ
        if(SceneManager.GetActiveScene().name == "Play"){

            if(Input.GetKey(KeyCode.Return)){
                    SceneManager.LoadScene("Clear");
            }

            if(Input.GetKey(KeyCode.Backspace)){
                    SceneManager.LoadScene("Over");
            }
        }

        if(SceneManager.GetActiveScene().name == "Clear" || SceneManager.GetActiveScene().name == "Over")
        {
            if(Input.GetKey(KeyCode.Return)){
                SceneManager.LoadScene("Title");
            }
        }
    }
}
