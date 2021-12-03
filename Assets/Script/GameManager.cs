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

    //private GameObject Player = default;
    //private Player_Life _playerControll = default;

    //private GameObject Boss = default;
    //private BossControll _bossControll = default;


    private void Start()
    {
        // FPSの固定
        // フルスクリーン
        Screen.SetResolution(1920, 1080, false);
        Application.targetFrameRate = 60;

        BGM[0].volume = PlayerPrefs.GetFloat("BGMVolume");
        SE[0].volume = PlayerPrefs.GetFloat("SEVolume");
    }

    private void Update()
    {
        BGM[0].volume = PlayerPrefs.GetFloat("BGMVolume");

    }

    //次のシーンへ
    public void ChangeScene2(string nextScene)
    {
        SceneManager.LoadScene(nextScene);
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
