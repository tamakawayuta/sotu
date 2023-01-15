using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 選択肢を示すオブジェクトの管理
/// </summary>

namespace QuicklyImage
{
    public class SelectButtonEventsQI : MonoBehaviour
    {
        // OnClick処理
        public void OnClickSelect()
        {
            // クリックしたオブジェクトが持つ画像を通知
            var director = GameObject.Find("GameDirector");
            director.GetComponent<CardSystemsQI>().CheckAnswer(this.gameObject.GetComponent<Image>().sprite);
        }

        // カードを裏返す処理
        public void SetSpriteToBlack()
        {
            this.gameObject.GetComponent<Button>().enabled = false;
            this.gameObject.GetComponent<Image>().sprite = null;
            this.gameObject.GetComponent<Image>().color = Color.black;
        }

        // 選択肢となる画像を設定する
        public void SetSprite(Sprite sprite)
        {
            this.gameObject.GetComponent<Image>().color = Color.white;
            this.gameObject.GetComponent<Image>().sprite = sprite;
            this.gameObject.GetComponent<Button>().enabled = true;
        }
    }
}