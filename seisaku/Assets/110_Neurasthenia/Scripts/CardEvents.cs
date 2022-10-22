using UnityEngine;
using UnityEngine.UI;

namespace Neurasthenia
{
    /// <summary>
    /// カードのOnClick用の関数
    /// </summary>
    
    [DisallowMultipleComponent]
    public sealed class CardEvents : MonoBehaviour
    {
        public void OnClickCard(int cardNum)
        {
            //選択したカードを押せなくする
            //同じカードを2回選ぶ事の防止
            this.gameObject.GetComponent<Button>().enabled = false;
            //カードを表にする
            this.gameObject.GetComponent<Image>().sprite = transform.parent.gameObject.GetComponent<CardSystems>().GetCardImages(cardNum);
            //選択したカードを通知して保持する
            transform.parent.gameObject.GetComponent<CardSystems>().SetClickCards(this.gameObject);

            //2枚選択したら同じカードか確認する
            if (transform.parent.gameObject.GetComponent<CardSystems>().GetClickCardsCount() == 2)
            {
                transform.parent.gameObject.GetComponent<CardSystems>().CheckAnswers();
            }
        }
    }
}