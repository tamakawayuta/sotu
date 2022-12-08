using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace QuicklyImage
{
    public class CardSystemsQI : MonoBehaviour
    {
        [SerializeField]
        private Sprite[] sprites;
        [SerializeField]
        private GameObject select;
        [SerializeField]
        private GameObject answerObject;
        [SerializeField]
        private GameObject time;
        [SerializeField]
        private GameObject guide;
        [SerializeField]
        private GameObject score;

        private List<GameObject> selectButtons = new List<GameObject>();
        private List<Sprite> useSprites = new List<Sprite>();
        private int delayTime = 100;

        private async void Awake()
        {
            for (var i = 0; i < select.transform.childCount; i++)
            {
                this.selectButtons.Add(this.select.transform.GetChild(i).gameObject);
            }


            foreach (var button in selectButtons)
            {
                button.GetComponent<SelectButtonEventsQI>().SetSpriteToBlack();
            }

            this.answerObject.GetComponent<AnswerEventsQI>().SetSpriteBlack();

            foreach (var sprite in sprites)
            {
                this.useSprites.Add(sprite);
            }

            ShuffleSprites(useSprites);

            await Task.Delay(2000);

            for (var i = 0; i < selectButtons.Count; i++)
            {
                selectButtons[i].GetComponent<SelectButtonEventsQI>().SetSprite(useSprites[i]);
            }

            SetAnswer();

            this.time.GetComponent<TimeSystemsQI>().StartCountdown(delayTime);
        }

        private async void ReShuffle()
        {
            ShuffleSprites(useSprites);
            await Task.Delay(1000);
            this.guide.GetComponent<GuideSystemQI>().SetText("ÇπÅ[ÇÃ..!");
            await Task.Delay(1000);

            for (var i = 0; i < selectButtons.Count; i++)
            {
                selectButtons[i].GetComponent<SelectButtonEventsQI>().SetSprite(useSprites[i]);
            }

            SetAnswer();
            this.time.GetComponent<TimeSystemsQI>().StartCountdown(delayTime);
        }

        private void ShuffleSprites(List<Sprite> sprites)
        {
            for (var i = sprites.Count - 1; i > 0; --i)
            {
                var j = Random.Range(0, i + 1);
                var tmp = sprites[i];
                sprites[i] = sprites[j];
                sprites[j] = tmp;
            }
        }

        private void SetAnswer()
        {
            var index = Random.Range(0, 4);
            this.answerObject.GetComponent<AnswerEventsQI>().SetAnswerSprite(this.useSprites[index]);

            this.guide.GetComponent<GuideSystemQI>().SetText("Ç±ÇÍÇÇ¶ÇÁÇ◊ÅI");
        }

        private void UpdateDelayTime()
        {
            if (this.delayTime < 10)
            {
                return;
            }

            this.delayTime -= 3;
        }

        public void CheckAnswer(Sprite sprite)
        {
            this.time.GetComponent<Image>().enabled = false;

            if (sprite == this.answerObject.GetComponent<Image>().sprite)
            {
                this.guide.GetComponent<GuideSystemQI>().SetText("Ç¢Ç¢ÇÀÅI");
                UpdateDelayTime();

                float bonusValue = this.time.GetComponent<Image>().fillAmount;
                bonusValue *= 100;
                int newScore = (int)bonusValue;
                Debug.Log(newScore);

                this.score.GetComponent<ScoreSystemsQI>().AddScore(newScore);
            }
            else
            {
                Debug.Log("False");
            }

            foreach (var button in selectButtons)
            {
                button.GetComponent<SelectButtonEventsQI>().SetSpriteToBlack();
            }
            this.answerObject.GetComponent<AnswerEventsQI>().SetSpriteBlack();

            ReShuffle();
        }
    }
}