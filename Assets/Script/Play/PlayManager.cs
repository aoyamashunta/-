using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayManager : MonoBehaviour
{
    // G�}�l�[�W���[
    public GameManager gMana;

    // �v���C���[
    private GameObject Player = default;
    private Player_Life _playerControll = default;

    // �{�X
    private GameObject Boss = default;
    private BossControll _bossControll = default;

    // Start is called before the first frame update
    void Start()
    {
        // ����������
        // �v���C���[
        Player = GameObject.FindGameObjectWithTag("Player");
        _playerControll = Player.GetComponent<Player_Life>();

        // �{�X
        //Boss = GameObject.FindGameObjectWithTag("Boss");
        //_bossControll = Boss.GetComponent<BossControll>();

    }

    // Update is called once per frame
    void Update()
    {
        // �V�[���J��(�N���A)
        if (_bossControll != null && _bossControll.IsChange_Scene || Input.GetKey(KeyCode.Return))
        {
            gMana.ChangeScene2("Clear");
            Player = null;
            _playerControll = null;
            Boss = null;
            _bossControll = null;
        }

        // �V�[���J��(�Q�[���I�[�o�[)
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
