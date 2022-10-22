using UnityEngine;
using UnityEngine.UI;

namespace Neurasthenia
{
    /// <summary>
    /// �J�[�h��OnClick�p�̊֐�
    /// </summary>
    
    [DisallowMultipleComponent]
    public sealed class CardEvents : MonoBehaviour
    {
        public void OnClickCard(int cardNum)
        {
            //�I�������J�[�h�������Ȃ�����
            //�����J�[�h��2��I�Ԏ��̖h�~
            this.gameObject.GetComponent<Button>().enabled = false;
            //�J�[�h��\�ɂ���
            this.gameObject.GetComponent<Image>().sprite = transform.parent.gameObject.GetComponent<CardSystems>().GetCardImages(cardNum);
            //�I�������J�[�h��ʒm���ĕێ�����
            transform.parent.gameObject.GetComponent<CardSystems>().SetClickCards(this.gameObject);

            //2���I�������瓯���J�[�h���m�F����
            if (transform.parent.gameObject.GetComponent<CardSystems>().GetClickCardsCount() == 2)
            {
                transform.parent.gameObject.GetComponent<CardSystems>().CheckAnswers();
            }
        }
    }
}