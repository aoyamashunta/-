using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // �^�C�g���}�l�[�W���[(�t���O�g�����߂ɓǂ�ł�)
    public TitleManager titleM;

    //�@��
    public AudioSource[] BGM;
    public AudioSource[] SE;

    private GameObject Player = default;
    private Player_Life _playerControll = default;

    private GameObject Boss = default;
    private BossControll _bossControll = default;


    private void Start()
    {
        // FPS�̌Œ�
        // �t���X�N���[��
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

    // �V�[���̐؂�ւ�
    void ChangeScene()
    {
        //�^�C�g��
        if (SceneManager.GetActiveScene().name == "Title" && !titleM.isMenuFlag)
        {
            if (Input.GetKey(KeyCode.Return) || Input.GetKeyDown("joystick button 0"))
            {
                SceneManager.LoadScene("Play");
            }
        }

        //�v���C
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

    // �V�[���̃��Z�b�g
    public void SceneReset()
    {
        string activeSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(activeSceneName);
    }

    // ���̍Đ�
    public void StartSound(AudioSource audio)
    {
        audio.Play();
    }
}
