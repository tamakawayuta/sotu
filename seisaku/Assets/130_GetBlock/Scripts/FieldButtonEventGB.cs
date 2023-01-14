using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �t�B�[���h�̋��OnClick�̒�`
/// </summary>

namespace GetBlock
{
    public class FieldButtonEventGB : MonoBehaviour
    {
        // �I�����ꂽ�摜�̊Ǘ�
        public static Sprite selectedSprite;

        private void Awake()
        {
            // ������
            selectedSprite = null;
        }

        public void OnClickField(int index)
        {
            // �Ⴄ�摜���m���N���b�N���Ă��������Ȃ�
            if (selectedSprite != null &&
                selectedSprite != this.gameObject.GetComponent<Image>().sprite)
            {
                return;
            }

            // �I�����ꂽ��͍đI���ł��Ȃ��悤�ɂ���
            this.gameObject.GetComponent<Image>().color = Color.black;
            this.gameObject.GetComponent<Button>().enabled = false;

            // �I�����ꂽ�摜���L�^����
            selectedSprite = this.gameObject.GetComponent<Image>().sprite;

            // �t�B�[���h���X�V����
            GameObject.Find("GameDirector").GetComponent<GameMainGB>().UpdateGameState(index,this.gameObject.GetComponent<Image>().sprite);
        }
    }
}