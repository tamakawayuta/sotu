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
        private bool isTimeUp = true;

        private void Awake()
        {
            // 初期化
            this.gameObject.GetComponent<Image>().fillAmount = 0f;
        }

        // カウントダウンの処理
        public async void StartCountdown(int delayTime)
        {
            this.gameObject.GetComponent<Image>().fillAmount = 1f;
            this.isTimeUp = true;

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

            // つまり、制限時間ないに選択肢を選ばなかったときゲーム終了時のウィンドウを表示
            if (this.isTimeUp)
            {
                GameObject.Find("GameDirector").GetComponent<CardSystemsQI>().AppearClearUI();
            }
        }

        public void SetIsTimeUp()
        {
            this.isTimeUp = false;
        }
    }
}