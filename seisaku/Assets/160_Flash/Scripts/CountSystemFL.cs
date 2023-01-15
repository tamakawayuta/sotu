using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 正解数の管理
/// </summary>

namespace Flash
{
    public class CountSystemFL : MonoBehaviour
    {
        // 正解数を記録する変数
        private int count = 0;

        // 正解数をカウントする
        public void AddCount()
        {
            this.count++;
            UpdateCountText();
        }

        // 正解数のゲッター
        public int GetCount()
        {
            return this.count;
        }

        // 正解数をテキストに反映する
        private void UpdateCountText()
        {
            this.gameObject.GetComponent<Text>().text = this.count.ToString();
        }
    }
}