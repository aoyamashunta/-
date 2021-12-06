using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;


public class P_Player_Manager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] GameObject Player_Prefab = null;
    Player_Life player_Life = default;

    [Header("Object")]
    [SerializeField] GameObject GameProgress = null;

    [Header("Camera")]
    [SerializeField] GameObject VCamera = null;
    CameraControll cameraControll = default;

    [Header("UI")]
    [SerializeField] Slider UI = null;

    [Header("TimeLine")]
    [SerializeField] PlayableDirector StartingCutScene = null;
    p_GameState state = p_GameState.None;
    
    public bool IsOn = false;

    private void Awake()
    {
        GameProgress.SetActive(false);

        cameraControll = VCamera.GetComponent<CameraControll>();
        cameraControll.enabled = false;

        UI.value = 1;
    }

    private void Start()
    {
        player_Life = Player_Prefab.GetComponent<Player_Life>();
        player_Life._slider = UI;
    }

    void Update()
    {
        switch (state)
        { 
            //再生
            case p_GameState.None:
                if (StartingCutScene)
                {
                    StartingCutScene.Play();
                }
                state = p_GameState.Opening;
                break;

            //再生終了
            case p_GameState.Opening:
                if(StartingCutScene && StartingCutScene.state != PlayState.Playing)
                {
                    StartingCutScene.gameObject.SetActive(false);
                    Instantiate(Player_Prefab, new Vector3(0.1262228f, 0.5f, -48.73f), Player_Prefab.transform.rotation);
                    ActiveOn();
                    state = p_GameState.InGame;
                }
                else if (!StartingCutScene)
                {
                    Instantiate(Player_Prefab, new Vector3(0.1262228f, 0.5f, -48.73f), Player_Prefab.transform.rotation);
                    ActiveOn();
                    state = p_GameState.InGame;
                }
                break;
        }
    }


    //Object表示
    private void ActiveOn()
    {
        GameProgress.SetActive(true);

        cameraControll.enabled = true;

        IsOn = true;
    }
}

public enum p_GameState 
{
    //ゲーム起動
    None,
    //再生中
    Opening,
    //ゲーム中
    InGame,
}