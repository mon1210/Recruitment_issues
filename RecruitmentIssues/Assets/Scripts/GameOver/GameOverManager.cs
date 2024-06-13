using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    // ガイドテキスト取得
    [SerializeField] private Text text;

    // フェード速度   (大きいほど遅い)
    const float FADE_DURATION = 1.0f;

    void Start()
    {
        text = GetComponent<Text>();

        StartCoroutine(fadeText());
    }

    public void OnTitle(InputAction.CallbackContext context)
    {
        // Spaceキー or pad下ボタン を押したら
        if (context.phase == InputActionPhase.Performed)
        {
            SceneManager.LoadScene("TitleScene");
        }
    }

    // テキストのフェードアウト処理
    private IEnumerator fadeText()
    {
        while (true)
        {
            // sinを使用することで値を周期的に変化させる
            // 1を足して2で割ることで[-1～1]の周期を[0～1]に変換する
            float Alpha = (Mathf.Sin(Time.time * Mathf.PI / FADE_DURATION) + 1.0f) / 2.0f;

            Color NewColor = text.color;
            NewColor.a = Alpha;
            text.color = NewColor;

            // 次のフレームまで待つ
            yield return null;
        }
    }
}
