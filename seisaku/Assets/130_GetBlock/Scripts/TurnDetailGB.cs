using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// どちらの番かを示すオブジェクトの管理
/// </summary>

namespace GetBlock
{
    public class TurnDetailGB : MonoBehaviour
    {
        private void Awake()
        {
            // 初期化
            ShowPlayer1();
        }

        // プレイヤー1の番にする
        public void ShowPlayer1()
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }

        // プレイヤー2の番にする
        public void ShowPlayer2()
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
}