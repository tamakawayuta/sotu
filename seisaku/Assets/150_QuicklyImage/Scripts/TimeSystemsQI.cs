using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 制限時間の管理
/// </summary>

namespace QuicklyImage
{
    public class TimeSystemsQI : MonoBehaviour
    {
        private void Awake()
        {
            // 初期化
            this.gameObject.GetComponent<Image>().fillAmount = 0f;
        }

        // カウントダウンの処理
        public async void StartCountdown(int delayTime)
        {
            this.gameObject.GetComponent<Image>().enabled = true;
            this.gameObject.GetComponent<Image>().fillAmount = 1f;

            while (this.gameObject.GetComponent<Image>().fillAmount > 0f)
            {
                // ポーズ中なら待機
                while (Time.timeScale == 0)
                {
                    await Task.Delay(1000);
                }

                this.gameObject.GetComponent<Image>().fillAmount -= 0.05f;

                await Task.Delay(delayTime);
            }

            // このオブジェクトの画像がアクティブならゲーム終了時のウィンドウを表示
            // つまり、制限時間ないに選択肢を選ばなかったとき
            if (this.gameObject.GetComponent<Image>().enabled)
            {
                GameObject.Find("GameDirector").GetComponent<CardSystemsQI>().AppearClearUI();
            }
        }
    }
}