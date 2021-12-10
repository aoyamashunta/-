using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Playables;

public class PlayManager : MonoBehaviour
{
    // G�}�l�[�W���[
    public GameManager gMana;

    // �v���C���[
    private GameObject Player = default;
    private Player_Life _playerControll = default;

    [Header("Timeline")]
    private PlayableDirector PlayerDeadScene = null;
    private GameObject PlayerDead = null;

    float player_flame = 0;


    // �{�X
    private GameObject Boss = default;
    private BossControll _bossControll = default;

    [Header("TimeLine")]
    PlayableDirector BossDead = null;
    GameObject BossManager  = null;

    GameObject Dice = null;


    private GameObject GameProcess = default;
    BossAppearance bossAppearance = default;

    private GameObject PlayerManager = default;
    P_Player_Manager p_Player_Manager = default;

    bool GameClear = false;
    bool GameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        PlayerDead = GameObject.FindGameObjectWithTag("PlayerDeadCut");
        PlayerDeadScene = PlayerDead.GetComponent<PlayableDirector>();
        PlayerDead.SetActive(false);


        PlayerManager = GameObject.FindGameObjectWithTag("PlayerManager");
        p_Player_Manager = PlayerManager.GetComponent<P_Player_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (p_Player_Manager.IsOn && GameProcess == null && Player == null)
        {
            GameProcess = GameObject.FindGameObjectWithTag("GameProcess");
            bossAppearance = GameProcess.GetComponent<BossAppearance>();

            Player = GameObject.FindGameObjectWithTag("Player");
            _playerControll = Player.GetComponent<Player_Life>();

            Debug.Log("Process,Player�擾");
        }

        if (p_Player_Manager.IsOn && bossAppearance.IsOn && Boss == null && _bossControll == null)
        {
            Boss = GameObject.FindGameObjectWithTag("Boss");
            _bossControll = Boss.GetComponent<BossControll>();
            //Debug.Log("�{�X�擾");

            BossManager = GameObject.FindGameObjectWithTag("BossDeadCut");
            BossDead = BossManager.GetComponent<PlayableDirector>();

            Dice = GameObject.FindGameObjectWithTag("Dice_Center");

            BossManager.SetActive(false);
        }

        // �V�[���J��(�N���A)
        if (_bossControll != null && _bossControll.IsDead)
        {
            GameClear = true;
        }

        if (GameClear)
        {
            _bossControll.Dead_Process();
            Boss.SetActive(false);
            Player.SetActive(false);
            BossManager.SetActive(true);
            BossManager.transform.position = new Vector3(0, 0, 3);
            Dice.SetActive(false);
            BossDead.Play();


            if(BossDead.time >= BossDead.duration){

                gMana.ChangeScene2("Clear");
                Time.timeScale = 1.0f;
                Player = null;
                _playerControll = null;
                Boss = null;
                _bossControll = null;
                GameClear = false;
            }
        }


        // �V�[���J��(�Q�[���I�[�o�[)
        if (_playerControll != null && _playerControll.IsDead)
        {
            GameOver = true;
        }

        if (GameOver)
        {
            Player.SetActive(false);
            PlayerDead.SetActive(true);
            PlayerDeadScene.transform.position = new Vector3(0, 5.5f, -48f);
            PlayerDeadScene.Play();

            if(PlayerDeadScene.time >= PlayerDeadScene.duration){
                gMana.ChangeScene2("Over");
                Player = null;
                _playerControll = null;
                Boss = null;
                _bossControll = null;
                GameOver = false;
            }
        }
    }
}
