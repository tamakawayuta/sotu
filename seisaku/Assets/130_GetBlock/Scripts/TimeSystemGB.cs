using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 制限時間の管理
/// </summary>

namespace GetBlock
{
    public class TimeSystemGB : MonoBehaviour
    {
        private void Awake()
        {
            // 初期化
            this.gameObject.GetComponent<Image>().fillAmount = 0f;
        }

        // カウントダウンを行う
        public async Task StartCountdown()
        {
            // ゲージをフルにする
            this.gameObject.GetComponent<Image>().fillAmount = 1f;

            // 空になるまで徐々にゲージを減らす
            while (this.gameObject.GetComponent<Image>().fillAmount > 0)
            {
                this.gameObject.GetComponent<Image>().fillAmount -= 0.05f;
                await Task.Delay(100);
            }
        }
    }
}