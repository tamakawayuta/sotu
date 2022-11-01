using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using PublicUI;

namespace HitAndBlow
{
    public class CardSystemsHB : MonoBehaviour
    {
        [SerializeField]
        private Sprite backImage;
        [SerializeField]
        private Sprite[] frontImages;
        [SerializeField]
        private GameObject endUI;

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

            /*for (var i = 0; i < cards.Count; i++)
            {
                cards[i].GetComponent<Image>().sprite = selectImages[i];
            }*/
        }

        /*private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                AppearAnswer();
            }
        }*/

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
                var randomIndex = Random.Range(0, frontImages.Length);
                selectImages[i] = frontImages[randomIndex];
            }
        }

        public async void AppearAnswer()
        {
            for (var i = 0; i < selectImages.Length; i++)
            {
                AppearAnimation(cards[i], i);
            }

            await Task.Delay(3800);
            endUI.GetComponent<GameOverSystems>().AppearUIOnlyText("‚¨‚ß‚Å‚Æ‚¤!!");
        }

        private async void AppearAnimation(GameObject card,int index)
        {
            var scaleX = card.GetComponent<RectTransform>().localScale.x;
            var scaleY = card.GetComponent<RectTransform>().localScale.y;
            while (scaleX > 0f)
            {
                card.GetComponent<RectTransform>().localScale = new Vector3(scaleX,scaleY,1f);
                scaleX -= 0.05f;
                await Task.Delay(30);
            }

            card.GetComponent<Image>().sprite = selectImages[index];

            while (scaleX < 1f)
            {
                card.GetComponent<RectTransform>().localScale = new Vector3(scaleX, scaleY, 1f);
                scaleX += 0.05f;
                await Task.Delay(30);
            }
        }

        public int CheckHit(Sprite[] answer)
        {
            if (answer.Length != 4)
            {
                Debug.LogError("");
            }

            var hitAmount = 0;

            for (var i = 0; i < 4; i++)
            {
                if (selectImages[i] == answer[i])
                {
                    hitAmount++;
                }
            }

            return hitAmount;
        }

        public int CheckBlow(Sprite[] answer)
        {
            if (answer.Length != 4)
            {
                Debug.LogError("");
            }

            var blowAmount = 0;
            bool[] didAddAmount = { false, false, false, false };

            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    if (didAddAmount[j])
                    {
                        continue;
                    }

                    if (answer[i] == selectImages[j])
                    {
                        blowAmount++;
                        didAddAmount[j] = true;
                    }
                }
            }

            return blowAmount;
        }
    }
}