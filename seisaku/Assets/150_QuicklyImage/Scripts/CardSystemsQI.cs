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

        private List<GameObject> selectButtons = new List<GameObject>();
        private List<Sprite> useSprites = new List<Sprite>();

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
    }
}