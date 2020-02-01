using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class GameManager : MonoBehaviour
{
    // TODO: structにする

    // プレー時間
    [SerializeField]
    private float PlayTime = 20f; 
    // 開始アニメーション(「スタート！」等のテキスト表示含むの所要時間)
    [SerializeField]
    private float StartWait = 3f; 
    // 終了アニメーション(「できたぁぁぁぁ！！」等のテキスト表示含むの所要時間)
    [SerializeField]
    private float FinishWait = 3f; 

    
    [SerializeField]
    private Transform CameraStartPoint;
    [SerializeField]
    private Transform CameraPlayPoint;

    [SerializeField]
    private Text MessageText;

    [SerializeField]
    private Transform CakeSpawnPoint;
    [SerializeField]
    private GameObject CakePrefab;
    private CakeManager CakeManager;

    // 修復したりするために操作できるオブジェクト達。
    [SerializeField]
    private Transform[] ToolSpawnPoints;
    [SerializeField]
    private GameObject[] ToolPrefabs;
    private ToolManager ToolManager;
    
    private ToolManager[] ToolManagers;

    private WaitForSeconds _StartWait;
    private WaitForSeconds _FinishWait;
    private WaitForSeconds _PlayTime;

    private void Start()
    {
        _StartWait = new WaitForSeconds(StartWait);
        _FinishWait = new WaitForSeconds(FinishWait);
        _PlayTime = new WaitForSeconds(PlayTime);

        CakePrefab.AddComponent<CakeManager>();

        // for (int i = 0; i < ToolPrefabs.Length; i++)
        // {
        //     ToolPrefabs[i].AddComponent
        //     toolManager
        // }
        CakeManager.Create(CakeSpawnPoint);
        
        SpawnCakeAndTools();

        StartCoroutine(GameLoop());
    }

    private void SpawnCakeAndTools()
    // 修復に使うオブジェクトを全て生成する
    {
        CakeManager.Create(CakeSpawnPoint);

        for (int i = 0; i < ToolManagers.Length; i++)
        {
            ToolManagers[i].Create(ToolSpawnPoints[i]);
        }
    }

    private IEnumerator GameLoop()
    {
        ResetCakeAndTools();
        DisableControls();
        CameraRigControl.Reset(CameraStartPoint);

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
        CameraRigControl.Move(CameraStartPoint);

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
        CakeManager.Reset(CakeSpawnPoint);

        for (int i = 0; i < ToolManagers.Length; i++)
        {
            ToolManagers[i].Reset(ToolSpawnPoints[i]);
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