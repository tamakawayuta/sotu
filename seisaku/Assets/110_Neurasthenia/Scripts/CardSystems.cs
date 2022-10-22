using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using SoundManager;

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
        [SerializeField]
        private GameObject soundManager;

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

            this.clickCards.Clear();
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
            if (this.clickCards[0].GetComponent<Image>().sprite == this.clickCards[1].GetComponent<Image>().sprite)
            {
                this.clickCards.Clear();
                soundManager.GetComponent<SoundSystems>().PlaySE(2);
                counter.GetComponent<CounterSystems>().UpdateCount();
                //hpGauge.GetComponent<HpSystems>().HealedHp();
                if (counter.GetComponent<CounterSystems>().CheckCount())
                {
                    ResetCards();
                }
            }
            else
            {
                RemoveCards();
                panel.GetComponent<PanelSystems>().switchPanelActive();
                soundManager.GetComponent<SoundSystems>().PlaySE(3);
                hpGauge.GetComponent<HpSystems>().DamagedHp();
            }
        }

        public Sprite GetCardImages(int cardNum)
        {
            return this.selectImages[cardNum];
        }

        public void SetClickCards(GameObject clicked)
        {
            soundManager.GetComponent<SoundSystems>().PlaySE(0);
            clickCards.Add(clicked);
        }

        public int GetClickCardsCount()
        {
            return this.clickCards.Count;
        }

        private async void RemoveCards()
        {
            await Task.Delay(1000);

            while (true)
            {
                if (Time.timeScale == 1)
                {
                    break;
                }
                await Task.Delay(2000);
            }

            foreach (var card in clickCards)
            {
                card.GetComponent<Image>().sprite = backImage;
                card.GetComponent<Button>().enabled = true;
            }
            this.clickCards.Clear();
            panel.GetComponent<PanelSystems>().switchPanelActive();
        }

        private async void ResetCards()
        {
            await Task.Delay(700);
            soundManager.GetComponent<SoundSystems>().PlaySE(1);
            foreach (var card in cards)
            {
                card.GetComponent<Image>().sprite = backImage;
                card.GetComponent<Button>().enabled = true;
            }

            hpGauge.GetComponent<HpSystems>().HealedHp();
            hpGauge.GetComponent<HpSystems>().HealedHp();

            ShuffleCardImages(frontImages);
            SelectCardImages();
            ShuffleCardImages(selectImages);
        }
    }
}