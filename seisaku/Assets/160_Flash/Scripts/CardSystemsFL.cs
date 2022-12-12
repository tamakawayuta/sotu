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

        private void Awake()
        {
            button.SetActive(false);

            ShuffleSprites(this.sprites);
            SelectSprites(3);

            this.answer.GetComponent<AnswerSystemFL>().SetSprites(this.useSprites,2000);

            Debug.Log("a");
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

        public void SetSelectedObject(Sprite sprite)
        {
            Debug.Log(sprite);
        }
    }
}