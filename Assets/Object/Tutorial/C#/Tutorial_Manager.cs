using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Prime31.TransitionKit;

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

    GameObject[] tagObject = null;
    public GameManager gMana;
   
    

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
            //再生
            case GameState.None:
                if (StartingCutScene)
                {
                    StartingCutScene.Play();
                }
                state = GameState.Opening;
                break;

            //再生終了
            case GameState.Opening:
                if(StartingCutScene && StartingCutScene.state != PlayState.Playing)
                {
                    StartingCutScene.gameObject.SetActive(false);
                    Instantiate(Player_Prefab, new Vector3(0.1262228f, 0.5f, -26.80061f), Player_Prefab.transform.rotation);
                    ActiveOn();
                    state = GameState.InGame;
                }
                else if (!StartingCutScene)
                {
                    Instantiate(Player_Prefab, new Vector3(0.1262228f, 0.5f, -26.80061f), Player_Prefab.transform.rotation);
                    ActiveOn();
                    state = GameState.InGame;
                }
                break;
        }

        tagObject = GameObject.FindGameObjectsWithTag("Gate");

        // シーン移動
        // 中身があるときのみ
        if (tagObject.Length == 1)
        {
            if (tagObject[0].GetComponent<GateColision>().isGateOpen)
            {
                gMana.ChangeScene2("Play");
                //var fishEye = new FishEyeTransition()
                //{
                //    duration = 2.0f,
                //    size = 0.2f,
                //    zoom = 100.0f,
                //    colorSeparation = 0.1f
                //};
                //TransitionKit.instance.transitionWithDelegate(fishEye);
            }
        }
    }


    //Object表示
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
    //ゲーム起動
    None,
    //再生中
    Opening,
    //ゲーム中
    InGame,
}

