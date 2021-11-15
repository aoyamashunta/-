using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // �^�C�g���}�l�[�W���[(�t���O�g�����߂ɓǂ�ł�)
    public TitleManager titleM;

    //�@��
    public AudioSource[] BGM;
    public AudioSource[] SE;

    private void Start()
    {
        // FPS�̌Œ�
        // �t���X�N���[��
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

    // �V�[���̐؂�ւ�
    void ChangeScene()
    {
        //�^�C�g��
        if (SceneManager.GetActiveScene().name == "Title" && !titleM.isMenuFlag)
        {
            if (Input.GetKey(KeyCode.Return))
            {
                SceneManager.LoadScene("Play");
            }
        }

        //�v���C
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

    // �V�[���̃��Z�b�g
    public void SceneReset()
    {
        string activeSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(activeSceneName);
    }
}
