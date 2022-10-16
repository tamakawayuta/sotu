using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Neurasthenia
{
    public class CardSystems : MonoBehaviour
    {
        [SerializeField]
        private Sprite backImage;
        [SerializeField]
        private Sprite[] frontImages;

        [SerializeField]
        private GameObject hpGauge;
        [SerializeField]
        private GameObject counter;
        [SerializeField]
        private GameObject panel;

        private List<GameObject> cards = new List<GameObject>();
        private Sprite[] selectImages;

        private List<GameObject> clickCards = new List<GameObject>();

        private void Awake()
        {
            for (var i = 0;i < this.gameObject.transform.childCount; i++)
            {
                cards.Add(this.gameObject.transform.GetChild(i).gameObject);
            }

            foreach(var card in cards)
            {
                card.GetComponent<Image>().sprite = backImage;
            }

            ShuffleCardImages(frontImages);
            SelectCardImages();
            ShuffleCardImages(selectImages);

        }

        private void ShuffleCardImages(Sprite[] images)
        {
            for (var i = images.Length - 1; i > 0; --i)
            {
                var j = Random.Range(0, i + 1);
                var tmp = images[i];
                images[i] = images[j];
                images[j] = tmp;
            }
        }

        private void SelectCardImages()
        {
            selectImages = new Sprite[cards.Count];

            for (int i = 0; i < cards.Count/2; i++)
            {
                selectImages[i] = frontImages[i];
                selectImages[i + cards.Count / 2] = frontImages[i];
            }
        }

        public void CheckAnswers()
        {
            panel.GetComponent<PanelSystems>().switchPanelActive();

            if (this.clickCards[0].GetComponent<Image>().sprite == this.clickCards[1].GetComponent<Image>().sprite)
            {
                this.clickCards.Clear();
                counter.GetComponent<CounterSystems>().UpdateCount();
                hpGauge.GetComponent<HpSystems>().HealedHp();
                panel.GetComponent<PanelSystems>().switchPanelActive();
                if (counter.GetComponent<CounterSystems>().CheckCount())
                {
                    Invoke("ResetCards", 0.3f);
                }
            }
            else
            {
                Invoke("RemoveCards", 0.7f);
                hpGauge.GetComponent<HpSystems>().DamagedHp();
            }
        }

        public Sprite GetCardImages(int cardNum)
        {
            return this.selectImages[cardNum];
        }

        public void SetClickCards(GameObject clicked)
        {
            clickCards.Add(clicked);
        }

        public int GetClickCardsCount()
        {
            return this.clickCards.Count;
        }

        private void RemoveCards()
        {
            foreach (var card in clickCards)
            {
                card.GetComponent<Image>().sprite = backImage;
                card.GetComponent<Button>().enabled = true;
            }
            this.clickCards.Clear();
            panel.GetComponent<PanelSystems>().switchPanelActive();
        }

        private void ResetCards()
        {
            foreach (var card in cards)
            {
                card.GetComponent<Image>().sprite = backImage;
                card.GetComponent<Button>().enabled = true;
            }

            ShuffleCardImages(frontImages);
            SelectCardImages();
            ShuffleCardImages(selectImages);
        }
    }
}