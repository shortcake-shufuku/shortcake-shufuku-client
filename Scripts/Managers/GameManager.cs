using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // プレー時間
    public int NumSecondsToFix = 20; 
    // 開始アニメーション(「スタート！」等のテキスト表示含むの所要時間)
    public int StartWait = 3; 
    // 終了アニメーション(「できたぁぁぁぁ！！」等のテキスト表示含むの所要時間)
    public int FinishWait = 3; 

    private void Start()
    {
        SpawnAllTools();
        StartCoroutine(GameLoop());
    }

    private void SpawnAllTools()
    // 修復に使うオブジェクトを全て生成する
    {
    }

    private IEnumerator GameLoop()
    {

    }
}