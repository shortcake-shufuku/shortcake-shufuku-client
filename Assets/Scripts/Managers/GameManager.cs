using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // プレー時間
    public float PlayTime = 20f; 
    // 開始アニメーション(「スタート！」等のテキスト表示含むの所要時間)
    public float StartWait = 3f; 
    // 終了アニメーション(「できたぁぁぁぁ！！」等のテキスト表示含むの所要時間)
    public float FinishWait = 3f; 

    public CameraRigControl CameraRigControl;
    public Text MessageText;

    // 壊れたり直したりするケーキ。あとでもしかしたら目的別に生成するかもしれないけど。
    public GameObject CakePrefab;
    public CakeManager CakeManager;
    // 修復したりするために操作できるオブジェクト達。
    public ToolManager[] ToolManagers;

    private WaitForSeconds _StartWait;
    private WaitForSeconds _FinishWait;
    private WaitForSeconds _PlayTime;


    private void Start()
    {
        _StartWait = new WaitForSeconds(StartWait);
        _FinishWait = new WaitForSeconds(FinishWait);
        _PlayTime = new WaitForSeconds(PlayTime);
        
        SpawnAllTools();

        StartCoroutine(GameLoop());
    }

    private void SpawnAllTools()
    // 修復に使うオブジェクトを全て生成する
    {
        CakeManager.Instance = Instantiate(
            CakePrefab,
            CakeManager.SpawnPoint.position,
            CakeManager.SpawnPoint.rotation
        ) as GameObject;


        foreach (ToolManager toolManager in ToolManagers)
        {
            toolManager.Instance = Instantiate(
                toolManager.Prefab,
                toolManager.SpawnPoint.position,
                toolManager.SpawnPoint.rotation
            ) as GameObject;

            toolManager.Setup();
        }
    }

    private IEnumerator GameLoop()
    {
        ResetCakeAndTools();
        DisableControls();
        CameraRigControl.Reset();

        yield return _StartWait;

        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

        // 無限にゲームはループする
        StartCoroutine(GameLoop());
    }

    private IEnumerator RoundStarting()
    {
        ResetCakeAndTools();
        DisableControls();
        CameraRigControl.Reset();

        MessageText.text = "始め！"; //TODO: もっとマシな開始メッセージ

        yield return _StartWait;
    }

    private IEnumerator RoundPlaying()
    {
        MessageText.text = string.Empty;
        EnableControls();

        while (!PlayFinished())
        {
            yield return _PlayTime;
        }
    }

    private IEnumerator RoundEnding()
    {
        DisableControls();

        MessageText.text = "出来たあああああぁぁあぁ!";

        yield return _FinishWait;
    }

    private bool PlayFinished()
    {
        // TODO: プレイ時間制限前にプレイが終了する条件？
        return true;
    }
    
    private void ResetCakeAndTools()
    {
        CakeManager.Reset();

        foreach(ToolManager toolManager in ToolManagers)
        {
            toolManager.Reset();
        }
    }

    private void EnableControls()
    {
        // TODO
    }

    private void DisableControls()
    {
        // TODO
    }
}