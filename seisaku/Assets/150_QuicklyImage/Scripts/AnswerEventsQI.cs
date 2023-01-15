using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �����̉摜��\������I�u�W�F�N�g�̊Ǘ�
/// </summary>

namespace QuicklyImage
{
    public class AnswerEventsQI : MonoBehaviour
    {
        // �����̉摜��\������
        public void SetAnswerSprite(Sprite sprite)
        {
            this.gameObject.GetComponent<Image>().color = Color.white;
            this.gameObject.GetComponent<Image>().sprite = sprite;
        }

        // �J�[�h�𗠕Ԃ�
        public void SetSpriteBlack()
        {
            this.gameObject.GetComponent<Image>().sprite = null;
            this.gameObject.GetComponent<Image>().color = Color.black;
        }
    }
}