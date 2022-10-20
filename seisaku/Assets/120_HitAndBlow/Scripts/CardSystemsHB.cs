using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HitAndBlow
{
    public class CardSystemsHB : MonoBehaviour
    {
        [SerializeField]
        private Sprite backImage;
        [SerializeField]
        private Sprite[] frontImages;

        private List<GameObject> cards = new List<GameObject>();
        private Sprite[] selectImages;

        private void Awake()
        {
            for (var i = 0; i < this.gameObject.transform.childCount; i++)
            {
                cards.Add(this.gameObject.transform.GetChild(i).gameObject);
            }

            foreach (var card in cards)
            {
                card.GetComponent<Image>().sprite = backImage;
                card.GetComponent<Image>().color = Color.white;
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

            for (int i = 0; i < 4; i++)
            {
                selectImages[i] = frontImages[i];
            }
        }

        private void AppearAnswer()
        {
            for (var i = 0; i < selectImages.Length; i++)
            {
                cards[i].GetComponent<Image>().sprite = selectImages[i];
            }
        }
    }
}