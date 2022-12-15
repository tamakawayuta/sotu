using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        private List<Sprite> useSprites;

        private List<Sprite> answers;

        private async void Awake()
        {
            button.SetActive(false);

            ShuffleSprites(this.sprites);
            SelectSprites(3);

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
            ShuffleSprites(this.sprites);
            SelectSprites(3);

            await this.answer.GetComponent<AnswerSystemFL>().SetSprites(this.useSprites, 2000);
            this.answers = new List<Sprite>(useSprites);

            ShuffleSprites(this.sprites);
            this.answer.SetActive(false);
            this.button.GetComponent<SelectSystemFL>().SetButtonSprites(this.sprites);
        }

        public void CheckAnswer(Sprite sprite)
        {
            if (this.answers.Contains(sprite))
            {
                this.answers.Remove(sprite);
                Debug.Log("true");

                if (this.answers.Count == 0)
                {
                    this.button.SetActive(false);
                    ResetSprites();
                }
            }
            else
            {
                Debug.Log("false");
            }
        }
    }
}