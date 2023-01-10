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
    }
}