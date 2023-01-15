using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// プレイヤーにゲーム進行を示す
/// </summary>

namespace QuicklyImage
{
    public class GuideSystemQI : MonoBehaviour
    {
        private void Awake()
        {
            // ゲームの開始を伝える
            SetText("せーの..!");
        }

        // 受け取ったテキストをそのまま表示する
        public void SetText(string text)
        {
            this.gameObject.GetComponentInChildren<Text>().text = text;
        }
    }
}