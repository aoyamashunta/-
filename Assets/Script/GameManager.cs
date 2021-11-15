using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // タイトルマネージャー(フラグ使うために読んでる)
    public TitleManager titleM;

    //　音
    public AudioSource[] BGM;
    public AudioSource[] SE;

    private void Start()
    {
        // FPSの固定
        // フルスクリーン
        Screen.SetResolution(1920, 1080, false);
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        ChangeScene();
    }

    // シーンの切り替え
    void ChangeScene()
    {
        //タイトル
        if (SceneManager.GetActiveScene().name == "Title" && !titleM.isMenuFlag)
        {
            if (Input.GetKey(KeyCode.Return))
            {
                SceneManager.LoadScene("Play");
            }
        }

        //プレイ
        if (SceneManager.GetActiveScene().name == "Play")
        {

            if (Input.GetKey(KeyCode.Return))
            {
                SceneManager.LoadScene("Clear");
            }

            if (Input.GetKey(KeyCode.Backspace))
            {
                SceneManager.LoadScene("Over");
            }
        }

        if (SceneManager.GetActiveScene().name == "Clear" || SceneManager.GetActiveScene().name == "Over")
        {
            if (Input.GetKey(KeyCode.Return))
            {
                SceneManager.LoadScene("Title");
            }
        }
    }

    // シーンのリセット
    public void SceneReset()
    {
        string activeSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(activeSceneName);
    }
}
