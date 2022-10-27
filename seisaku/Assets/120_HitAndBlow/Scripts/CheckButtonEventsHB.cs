using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PublicUI;

namespace HitAndBlow
{
    public class CheckButtonEventsHB : MonoBehaviour
    {
        [SerializeField]
        private GameObject images;
        [SerializeField]
        private GameObject texts;
        [SerializeField]
        private GameObject answerCard;
        [SerializeField]
        private GameObject showHintText;
        [SerializeField]
        private GameObject endUI;

        private int count = 0;

        private void Awake()
        {
            this.gameObject.GetComponent<Image>().color = Color.gray;
            this.gameObject.GetComponent<Button>().enabled = false;
        }

        private void Update()
        {
            if (images.GetComponent<RecordImageSystemsHB>().GetIsImageSetFull())
            {
                this.gameObject.GetComponent<Image>().color = Color.white;
                this.gameObject.GetComponent<Button>().enabled = true;
            }
            else
            {
                this.gameObject.GetComponent<Image>().color = Color.gray;
                this.gameObject.GetComponent<Button>().enabled = false;
            }
        }

        public void OnClickCheck()
        {
            var hit = 0;
            var blow = 0;

            hit = answerCard.GetComponent<CardSystemsHB>().CheckHit(images.GetComponent<RecordImageSystemsHB>().GetSelectedSprites());
            blow = answerCard.GetComponent<CardSystemsHB>().CheckBlow(images.GetComponent<RecordImageSystemsHB>().GetSelectedSprites());
            count++;

            if (hit == 4)
            {
                answerCard.GetComponent<CardSystemsHB>().AppearAnswer();
                showHintText.GetComponent<ShowHintSystemHB>().ShowText("4ヒット");
            }
            else if (count == 8)
            {
                endUI.GetComponent<GameOverSystems>().AppearUIOnlyText("がんばったね!!");
            }
            else
            {
                var realBlow = blow - hit;
                if (realBlow < 0)
                {
                    realBlow = 0;
                }

                showHintText.GetComponent<ShowHintSystemHB>().ShowText(hit + "ヒット " + realBlow + "ブロー");
            }

            texts.GetComponent<RecordTextSystemsHB>().DrawText(hit, blow);
            images.GetComponent<RecordImageSystemsHB>().SetImageNow();
        }
    }
}