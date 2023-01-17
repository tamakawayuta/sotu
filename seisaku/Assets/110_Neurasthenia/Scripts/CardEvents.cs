using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// カードオブジェクトが持つOnClick用の関数宣言
/// </summary>

namespace Neurasthenia
{
    public class CardEvents : MonoBehaviour
    {
        public void OnClickCard( int cardNum)
        {
            // このカードを再選択できないようにする
            this.gameObject.GetComponent<Button>().enabled = false;

            // このカードに割り当てられたスプライトを読み込んで表示する
            this.gameObject.GetComponent<Image>().sprite = transform.parent.gameObject.GetComponent<CardSystems>().GetCardImages(cardNum);

            // 親に自分が選択されたことを通知する
            transform.parent.gameObject.GetComponent<CardSystems>().SetClickCards(this.gameObject);

            // 2枚選択されたなら
            // つまり、親の選択されたカードオブジェクトを管理するリストの大きさが2になったら
            if (transform.parent.gameObject.GetComponent<CardSystems>().GetClickCardsCount() == 2)
            {
                // 親に答え合わせを依頼する
                transform.parent.gameObject.GetComponent<CardSystems>().CheckAnswers();
            }
        }

        public async void TurnBack(Sprite backSprite)
        {
            // 受け取ったカードオブジェクトの大きさを取得
            var scaleX = this.gameObject.GetComponent<RectTransform>().localScale.x;
            var scaleY = this.gameObject.GetComponent<RectTransform>().localScale.y;

            // Xを減らしてめくる感じにする
            while (scaleX > 0f)
            {
                this.gameObject.GetComponent<RectTransform>().localScale = new Vector3(scaleX, scaleY, 1f);
                scaleX -= 0.05f;
                await Task.Delay(30);
            }

            // 対応する表の画像をセット
            this.gameObject.GetComponent<Image>().sprite = backSprite;

            // Xを元に戻す
            while (scaleX < 1f)
            {
                this.gameObject.GetComponent<RectTransform>().localScale = new Vector3(scaleX, scaleY, 1f);
                scaleX += 0.05f;
                await Task.Delay(30);
            }
        }
    }
}