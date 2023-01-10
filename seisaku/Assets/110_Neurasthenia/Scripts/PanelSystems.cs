using UnityEngine;

/// <summary>
/// 不正解のとき、カードの選択を制限するための透明なパネル
/// </summary>

namespace Neurasthenia
{
    public class PanelSystems : MonoBehaviour
    {
        private void Awake()
        {
            // 初期化
            this.gameObject.SetActive(false);
        }

        // パネルのセットアクティブを切り替える
        public void switchPanelActive()
        {
            this.gameObject.SetActive(!this.gameObject.activeSelf);
        }
    }
}