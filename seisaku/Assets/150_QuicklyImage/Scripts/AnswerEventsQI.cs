using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 正解の画像を表示するオブジェクトの管理
/// </summary>

namespace QuicklyImage
{
    public class AnswerEventsQI : MonoBehaviour
    {
        // 正解の画像を表示する
        public void SetAnswerSprite(Sprite sprite)
        {
            this.gameObject.GetComponent<Image>().color = Color.white;
            this.gameObject.GetComponent<Image>().sprite = sprite;
        }

        // カードを裏返す
        public void SetSpriteBlack()
        {
            this.gameObject.GetComponent<Image>().sprite = null;
            this.gameObject.GetComponent<Image>().color = Color.black;
        }
    }
}