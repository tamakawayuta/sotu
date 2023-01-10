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
    }
}