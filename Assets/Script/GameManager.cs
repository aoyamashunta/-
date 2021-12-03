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

    //private GameObject Player = default;
    //private Player_Life _playerControll = default;

    //private GameObject Boss = default;
    //private BossControll _bossControll = default;


    private void Start()
    {
        // FPS�̌Œ�
        // �t���X�N���[��
        Screen.SetResolution(1920, 1080, false);
        Application.targetFrameRate = 60;

        BGM[0].volume = PlayerPrefs.GetFloat("BGMVolume");
        SE[0].volume = PlayerPrefs.GetFloat("SEVolume");
    }

    private void Update()
    {
        BGM[0].volume = PlayerPrefs.GetFloat("BGMVolume");

    }

    //���̃V�[����
    public void ChangeScene2(string nextScene)
    {
        SceneManager.LoadScene(nextScene);
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
