using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 選択肢を示すオブジェクトの管理
/// </summary>

namespace QuicklyImage
{
    public class SelectButtonEventsQI : MonoBehaviour
    {
        private static bool didClick = false;

        // OnClick処理
        public void OnClickSelect()
        {
            if (didClick)
            {
                return;
            }

            didClick = true;

            // クリックしたオブジェクトが持つ画像を通知
            var director = GameObject.Find("GameDirector");
            director.GetComponent<CardSystemsQI>().CheckAnswer(this.gameObject.GetComponent<Image>().sprite);
        }

        // 選択肢となる画像を設定する
        public void SetSprite(Sprite sprite)
        {
            didClick = false;

            this.gameObject.GetComponent<Image>().color = Color.white;
            this.gameObject.GetComponent<Image>().sprite = sprite;
            this.gameObject.GetComponent<Button>().enabled = true;
        }
    }
}