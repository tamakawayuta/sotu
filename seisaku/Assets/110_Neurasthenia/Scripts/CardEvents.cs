using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �J�[�h�I�u�W�F�N�g������OnClick�p�̊֐��錾
/// </summary>

namespace Neurasthenia
{
    public class CardEvents : MonoBehaviour
    {
        public void OnClickCard( int cardNum)
        {
            // ���̃J�[�h���đI���ł��Ȃ��悤�ɂ���
            this.gameObject.GetComponent<Button>().enabled = false;

            // ���̃J�[�h�Ɋ��蓖�Ă�ꂽ�X�v���C�g��ǂݍ���ŕ\������
            this.gameObject.GetComponent<Image>().sprite = transform.parent.gameObject.GetComponent<CardSystems>().GetCardImages(cardNum);

            // �e�Ɏ������I�����ꂽ���Ƃ�ʒm����
            transform.parent.gameObject.GetComponent<CardSystems>().SetClickCards(this.gameObject);

            // 2���I�����ꂽ�Ȃ�
            // �܂�A�e�̑I�����ꂽ�J�[�h�I�u�W�F�N�g���Ǘ����郊�X�g�̑傫����2�ɂȂ�����
            if (transform.parent.gameObject.GetComponent<CardSystems>().GetClickCardsCount() == 2)
            {
                // �e�ɓ������킹���˗�����
                transform.parent.gameObject.GetComponent<CardSystems>().CheckAnswers();
            }
        }

        public async void TurnBack(Sprite backSprite)
        {
            // �󂯎�����J�[�h�I�u�W�F�N�g�̑傫�����擾
            var scaleX = this.gameObject.GetComponent<RectTransform>().localScale.x;
            var scaleY = this.gameObject.GetComponent<RectTransform>().localScale.y;

            // X�����炵�Ă߂��銴���ɂ���
            while (scaleX > 0f)
            {
                this.gameObject.GetComponent<RectTransform>().localScale = new Vector3(scaleX, scaleY, 1f);
                scaleX -= 0.05f;
                await Task.Delay(30);
            }

            // �Ή�����\�̉摜���Z�b�g
            this.gameObject.GetComponent<Image>().sprite = backSprite;

            // X�����ɖ߂�
            while (scaleX < 1f)
            {
                this.gameObject.GetComponent<RectTransform>().localScale = new Vector3(scaleX, scaleY, 1f);
                scaleX += 0.05f;
                await Task.Delay(30);
            }
        }
    }
}