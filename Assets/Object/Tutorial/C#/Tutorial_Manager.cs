using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Tutorial_Manager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] GameObject Player_Prefab = null;

    [Header("Object")]
    [SerializeField] GameObject Ball_Prefab = null;
    [SerializeField] GameObject Niddle_prefab1 = null;
    [SerializeField] GameObject Niddle_prefab2 = null;
    [SerializeField] GameObject Niddle_prefab3 = null;

    [Header("Camera")]
    [SerializeField] GameObject VCamera = null;
    CameraControll cameraControll = default;

    [Header("UI")]
    [SerializeField] GameObject UI = null;

    [Header("TimeLine")]
    [SerializeField] PlayableDirector StartingCutScene = null;
    GameState state = GameState.None;
    

    private void Awake()
    {
        Ball_Prefab.SetActive(false);
        Niddle_prefab1.SetActive(false);
        Niddle_prefab2.SetActive(false);
        Niddle_prefab3.SetActive(false);
        UI.SetActive(false);

        cameraControll = VCamera.GetComponent<CameraControll>();
        cameraControll.enabled = false;
    }

    void Update()
    {
        switch (state)
        { 
            //�Đ�
            case GameState.None:
                if (StartingCutScene)
                {
                    StartingCutScene.Play();
                }
                state = GameState.Opening;
                break;

            //�Đ��I��
            case GameState.Opening:
                if(StartingCutScene && StartingCutScene.state != PlayState.Playing)
                {
                    StartingCutScene.gameObject.SetActive(false);
                    Instantiate(Player_Prefab, new Vector3(0.1262228f, 0.5f, -29.80061f), Player_Prefab.transform.rotation);
                    ActiveOn();
                    state = GameState.InGame;
                }
                else if (!StartingCutScene)
                {
                    Instantiate(Player_Prefab, new Vector3(0.1262228f, 0.5f, -29.80061f), Player_Prefab.transform.rotation);
                    ActiveOn();
                    state = GameState.InGame;
                }
                break;
        }
    }


    //Object�\��
    private void ActiveOn()
    {
        Ball_Prefab.SetActive(true);
        Niddle_prefab1.SetActive(true);
        Niddle_prefab2.SetActive(true);
        Niddle_prefab3.SetActive(true);
        UI.SetActive(true);

        cameraControll.enabled = true;
    }
}

public enum GameState 
{
    //�Q�[���N��
    None,
    //�Đ���
    Opening,
    //�Q�[����
    InGame,
}

