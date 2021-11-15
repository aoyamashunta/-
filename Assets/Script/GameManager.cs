using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

        BGM[0].volume = PlayerPrefs.GetFloat("BGMVolume");
    }

    private void Update()
    {
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        ChangeScene();

        BGM[0].volume = PlayerPrefs.GetFloat("BGMVolume");
    }

    // シーンの切り替え
    void ChangeScene()
    {
        //タイトル
        if (SceneManager.GetActiveScene().name == "Title" && !titleM.isMenuFlag)
        {
            if (Input.GetKey(KeyCode.Return) || Input.GetKeyDown("joystick button 0"))
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

    // 音の再生
    public void StartSound(AudioSource audio)
    {
        audio.Play();
    }
}
