using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �I�����������I�u�W�F�N�g�̊Ǘ�
/// </summary>

namespace QuicklyImage
{
    public class SelectButtonEventsQI : MonoBehaviour
    {
        // OnClick����
        public void OnClickSelect()
        {
            // �N���b�N�����I�u�W�F�N�g�����摜��ʒm
            var director = GameObject.Find("GameDirector");
            director.GetComponent<CardSystemsQI>().CheckAnswer(this.gameObject.GetComponent<Image>().sprite);
        }

        // �J�[�h�𗠕Ԃ�����
        public void SetSpriteToBlack()
        {
            this.gameObject.GetComponent<Button>().enabled = false;
            this.gameObject.GetComponent<Image>().sprite = null;
            this.gameObject.GetComponent<Image>().color = Color.black;
        }

        // �I�����ƂȂ�摜��ݒ肷��
        public void SetSprite(Sprite sprite)
        {
            this.gameObject.GetComponent<Image>().color = Color.white;
            this.gameObject.GetComponent<Image>().sprite = sprite;
            this.gameObject.GetComponent<Button>().enabled = true;
        }
    }
}