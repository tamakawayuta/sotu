using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 答えの管理
/// </summary>

namespace Flash
{
    public class AnswerSystemFL : MonoBehaviour
    {
        // 答えの画像を表示する
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