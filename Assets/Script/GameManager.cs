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

    private GameObject Player = default;
    private Player_Life _playerControll = default;

    private GameObject Boss = default;
    private BossControll _bossControll = default;


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
            if(Player == null && _playerControll == null){
                Player = GameObject.FindGameObjectWithTag("Player");
                _playerControll = Player.GetComponent<Player_Life>();
            }
            else if(Boss == null && _bossControll == null){
                Boss = GameObject.FindGameObjectWithTag("Boss");
                _bossControll = Boss.GetComponent<BossControll>();
            }


            if (_bossControll != null && _bossControll.IsChange_Scene)
            {
                SceneManager.LoadScene("Clear");
            }

            if (_playerControll != null && _playerControll.IsDead)
            {
                SceneManager.LoadScene("Over");
            }
        }

        if (SceneManager.GetActiveScene().name == "Clear" || SceneManager.GetActiveScene().name == "Over")
        {
            Player = null;
            _playerControll = null;
            Boss = null;
            _bossControll = null;

            if (Input.GetKey(KeyCode.Return) || Input.GetKeyDown("joystick button 0"))
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
