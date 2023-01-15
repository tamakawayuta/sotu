using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// “š‚¦‚ÌŠÇ—
/// </summary>

namespace Flash
{
    public class AnswerSystemFL : MonoBehaviour
    {
        // “š‚¦‚Ì‰æ‘œ‚ğ•\¦‚·‚é
        public async Task SetSprites(List<Sprite> sprites, int delayTime)
        {
            this.gameObject.SetActive(true);

            foreach (var sprite in sprites)
            {
                this.gameObject.GetComponent<Image>().sprite = sprite;
                await Task.Delay(delayTime);
            }
        }
    }
}