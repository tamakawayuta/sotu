using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PublicUI;

namespace Flash
{
    public class CardSystemsFL : MonoBehaviour
    {
        [SerializeField]
        private Sprite[] sprites;
        [SerializeField]
        private GameObject answer;
        [SerializeField]
        private GameObject button;
        [SerializeField]
        private GameObject count;
        [SerializeField]
        private GameObject clearUI;

        private List<Sprite> useSprites;
        private List<Sprite> answers;

        private int answerAmount = 0;
        private int selectSpriteAmount = 2;
        private int delayTime = 2000;

        private async void Awake()
        {
            button.SetActive(false);

            ShuffleSprites(this.sprites);
            SelectSprites(selectSpriteAmount);

            await this.answer.GetComponent<AnswerSystemFL>().SetSprites(this.useSprites,2000);
            this.answers = new List<Sprite>(useSprites);

            ShuffleSprites(this.sprites);
            this.answer.SetActive(false);
            this.button.GetComponent<SelectSystemFL>().SetButtonSprites(this.sprites);
        }

        private void ShuffleSprites(Sprite[] sprites)
        {
            for (var i = sprites.Length - 1; i > 0; --i)
            {
                var j = Random.Range(0, i + 1);
                var tmp = sprites[i];
                sprites[i] = sprites[j];
                sprites[j] = tmp;
            }
        }

        private void SelectSprites(int selectAmount)
        {
            useSprites = new List<Sprite>();
            
            for (var i = 0; i < selectAmount; i++)
            {
                useSprites.Add(this.sprites[i]);
            }
        }

        private async void ResetSprites()
        {
            this.answerAmount++;

            ShuffleSprites(this.sprites);
            UpdateSelectSpriteAmount();
            SelectSprites(selectSpriteAmount);

            await this.answer.GetComponent<AnswerSystemFL>().SetSprites(this.useSprites, 2000);
            this.answers = new List<Sprite>(useSprites);

            ShuffleSprites(this.sprites);
            this.answer.SetActive(false);
            this.button.GetComponent<SelectSystemFL>().SetButtonSprites(this.sprites);
        }

        private void UpdateSelectSpriteAmount()
        {
            if (this.selectSpriteAmount > this.sprites.Length - 1)
            {
                UpdateDelayTime();
                return;
            }

            if (this.answerAmount % 3 == 0)
            {
                this.selectSpriteAmount++;
            }
        }

        private void UpdateDelayTime()
        {
            if (this.delayTime < 0)
            {
                return;
            }

            this.delayTime -= 20;
        }

        public void CheckAnswer(Sprite sprite)
        {
            if (this.answers.Contains(sprite))
            {
                this.answers.Remove(sprite);
                this.count.GetComponent<CountSystemFL>().AddCount();

                if (this.answers.Count == 0)
                {
                    this.button.SetActive(false);
                    ResetSprites();
                }
            }
            else
            {
                this.clearUI.GetComponent<GameOverSystems>().AppearGameOverUI(this.count.GetComponent<CountSystemFL>().GetCount());
            }
        }
    }
}