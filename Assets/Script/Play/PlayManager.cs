using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayManager : MonoBehaviour
{
    // Gマネージャー
    public GameManager gMana;

    // プレイヤー
    private GameObject Player = default;
    private Player_Life _playerControll = default;

    // ボス
    private GameObject Boss = default;
    private BossControll _bossControll = default;

    // Start is called before the first frame update
    void Start()
    {
        // 初期化処理
        // プレイヤー
        Player = GameObject.FindGameObjectWithTag("Player");
        _playerControll = Player.GetComponent<Player_Life>();

        // ボス
        //Boss = GameObject.FindGameObjectWithTag("Boss");
        //_bossControll = Boss.GetComponent<BossControll>();

    }

    // Update is called once per frame
    void Update()
    {
        // シーン遷移(クリア)
        if (_bossControll != null && _bossControll.IsChange_Scene || Input.GetKey(KeyCode.Return))
        {
            gMana.ChangeScene2("Clear");
            Player = null;
            _playerControll = null;
            Boss = null;
            _bossControll = null;
        }

        // シーン遷移(ゲームオーバー)
        if (_playerControll != null && _playerControll.IsDead ||  Input.GetKey(KeyCode.Return))
        {
            gMana.ChangeScene2("Over");
            Player = null;
            _playerControll = null;
            Boss = null;
            _bossControll = null;
        }

    }
}
