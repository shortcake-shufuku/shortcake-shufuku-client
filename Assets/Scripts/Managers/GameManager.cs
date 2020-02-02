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

    public CanvasControl CanvasControl;
    
    public float CameraMoveSpeed = 6f;
    public CameraRigControl CameraRigControl;
    public Transform CameraStartPoint;
    public Transform CameraPlayPoint;

    // 壊れたり直したりするケーキ。あとでもしかしたら目的別に生成するかもしれないけど。
    public GameObject CakePrefab;
    public CakeManager CakeManager;
    public Transform CakeSpawnPoint;
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
        
        CanvasControl.Init();

        CanvasControl.MessageText.text = $"ケーキ修復！"; //TODO: もっとマシな開始メッセージ

        SpawnAllTools();

        StartCoroutine(GameLoop());
    }

    private void SpawnAllTools()
    // 修復に使うオブジェクトを全て生成する
    {
        CakeManager.Instance = Instantiate(
            CakePrefab,
            CakeSpawnPoint.position,
            CakeSpawnPoint.rotation
        ) as GameObject;

    }

    private IEnumerator GameLoop()
    {
        ResetCakeAndTools();
        DisableControls();
        CameraRigControl.Init(CameraStartPoint);
        
        CanvasControl.ToggleTimer(false);

        yield return _StartWait;

        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

        // 無限にゲームはループする
        StartCoroutine(GameLoop());
    }

    private IEnumerator RoundStarting()
    {
        Debug.Log("RoundStarting");
        ResetCakeAndTools();
        DisableControls();

        CanvasControl.ToggleTimer(true);
        
        float currentTime = 0;
        
        while (currentTime <= 1 ){
            currentTime += Time.deltaTime * CameraMoveSpeed;
            CameraRigControl.Move(CameraPlayPoint, currentTime);
            yield return new WaitForEndOfFrame();
        }

        CanvasControl.MessageText.text = "始め！"; //TODO: もっとマシな開始メッセージ

        yield return _StartWait;
    }

    private IEnumerator RoundPlaying()
    {
        CanvasControl.MessageText.text = string.Empty;

        EnableControls();

        float timeLeft = PlayTime;
        
        while (timeLeft >= 0f ){
            timeLeft -= Time.deltaTime;
            CanvasControl.UpdateTimer(timeLeft, PlayTime);

            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator RoundEnding()
    {
        DisableControls();

        CanvasControl.MessageText.text = "出来たあああああぁぁあぁ!";

        yield return _FinishWait;
    }

    private void ResetCakeAndTools()
    {
        CakeManager.Reset(CakeSpawnPoint);

        // foreach(ToolManager toolManager in ToolManagers)
        // {
        //     toolManager.Reset();
        // }
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