using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using SoundManager;

/// <summary>
/// “š‚¦‚ÌŠÇ—
/// </summary>

namespace Flash
{
    public class AnswerSystemFL : MonoBehaviour
    {
        [SerializeField]
        private GameObject sound;

        // “š‚¦‚Ì‰æ‘œ‚ğ•\¦‚·‚é
        public async Task SetSprites(List<Sprite> sprites, int delayTime)
        {
            this.gameObject.SetActive(true);

            foreach (var sprite in sprites)
            {
                while (true)
                {
                    if (Time.timeScale == 1)
                    {
                        break;
                    }
                    await Task.Delay(2000);
                }

                sound.GetComponent<SoundSystems>().PlaySE(0);
                this.gameObject.GetComponent<Image>().sprite = sprite;
                await Task.Delay(delayTime);
            }
        }
    }
}