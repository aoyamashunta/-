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

    public GameObject[] BGMgameObjects;
    public GameObject[] SEgameObjects;

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
    }

    private void Update()
    {

      
        BGM[0].volume = PlayerPrefs.GetFloat("BGMVolume");
        
        for (int i = 0; i < BGMgameObjects.Length; i++)
        {
            BGMgameObjects[i].GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("BGMVolume");
        }
        //.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SEVolume");

        for (int i = 0; i <SE.Length; i++)
        {
            if (SE[i])
            {
                SE[i].volume = PlayerPrefs.GetFloat("SEVolume");
            }
        }

        for (int i = 0; i < SEgameObjects.Length; i++)
        {
            if (SEgameObjects[i])
            {
                if(SEgameObjects[i].transform.Find("Dice_Relationship/dice"))
                {
                    SEgameObjects[i].transform.Find("Dice_Relationship/dice").GetComponent<AudioSource>().volume = SE[0].volume;
                }
                else if(SEgameObjects[i].transform.Find("kemurin"))
                {
                    SEgameObjects[i].transform.Find("kemurin").GetComponent<AudioSource>().volume = SE[0].volume;
                }

                else
                {
                    SEgameObjects[i].GetComponent<AudioSource>().volume = SE[0].volume;
                }
            }
        }
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
