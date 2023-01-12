using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ヒットとブローの数を強調する
/// </summary>

namespace HitAndBlow
{
    public class ShowHintSystemHB : MonoBehaviour
    {
        private void Awake()
        {
            // 初期化
            this.gameObject.SetActive(false);
        }

        // ヒットとブローの数を示す
        public async void ShowText(string text)
        {
            this.gameObject.SetActive(true);
            this.gameObject.GetComponentInChildren<Text>().text = text;
            await Task.Delay(1500);
            this.gameObject.SetActive(false);
        }
    }
}