using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using SoundManager;

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

        public async void OnClickField(int index)
        {
            // �Ⴄ�摜���m���N���b�N���Ă��������Ȃ�
            if (selectedSprite != null &&
                selectedSprite != this.gameObject.GetComponent<Image>().sprite)
            {
                return;
            }

            GameObject.Find("SoundManager").GetComponent<SoundSystems>().PlaySE(0);

            // �I�����ꂽ��͍đI���ł��Ȃ��悤�ɂ���
            this.gameObject.GetComponent<Button>().enabled = false;

            // �I�����ꂽ�摜���L�^����
            selectedSprite = this.gameObject.GetComponent<Image>().sprite;

            // �t�B�[���h���X�V����
            GameObject.Find("GameDirector").GetComponent<GameMainGB>().UpdateGameState(index,this.gameObject.GetComponent<Image>().sprite);

            var alpha = this.gameObject.GetComponent<Image>().color.a;
            while (alpha > 0f)
            {
                alpha -= 0.1f;
                this.gameObject.GetComponent<Image>().color = new Color(255f, 255f, 255f, alpha);
                await Task.Delay(100);
            }
        }
    }
}