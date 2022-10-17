using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Neurasthenia
{
    public class CardEvents : MonoBehaviour
    {
        public void OnClickCard(int cardNum)
        {
            this.gameObject.GetComponent<Button>().enabled = false;
            this.gameObject.GetComponent<Image>().sprite = transform.parent.gameObject.GetComponent<CardSystems>().GetCardImages(cardNum);
            transform.parent.gameObject.GetComponent<CardSystems>().SetClickCards(this.gameObject);

            if (transform.parent.gameObject.GetComponent<CardSystems>().GetClickCardsCount() == 2)
            {
                transform.parent.gameObject.GetComponent<CardSystems>().CheckAnswers();
            }
        }
    }
}