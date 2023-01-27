using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// �K�v
using PublicUI;
using SoundManager;

/// <summary>
/// �����̊m�F������{�^����OnClick
/// </summary>

namespace HitAndBlow
{
    public class CheckButtonEventsHB : MonoBehaviour
    {
        // �K�v�Ȃ��̂̎擾
        [SerializeField]
        private GameObject images;
        [SerializeField]
        private GameObject texts;
        [SerializeField]
        private GameObject answerCard;
        [SerializeField]
        private GameObject showHintText;
        [SerializeField]
        private GameObject detail;
        [SerializeField]
        private GameObject endUI;
        [SerializeField]
        private GameObject sound;

        // �񓚂������̊Ǘ�
        private int count = 0;

        private void Awake()
        {
            // �����Ȃ��悤�ɂ��鏉����
            this.gameObject.GetComponent<Image>().color = Color.gray;
            this.gameObject.GetComponent<Button>().enabled = false;
        }

        private void Update()
        {
            // ������S�ē��͂����牟����悤�ɂ���
            if (images.GetComponent<RecordImageSystemsHB>().GetIsImageSetFull())
            {
                this.gameObject.GetComponent<Image>().color = Color.white;
                this.gameObject.GetComponent<Button>().enabled = true;
            }
            else
            {
                this.gameObject.GetComponent<Image>().color = Color.gray;
                this.gameObject.GetComponent<Button>().enabled = false;
            }
        }

        // �{�^�����������Ƃ��̒�`
        public async void OnClickCheck()
        {
            // ���肵�����ʂ��󂯎��ꎞ�I�Ȕz��
            int[] amounts = new int[2];

            // ����
            amounts = answerCard.GetComponent<CardSystemsHB>().CheckAnswer(images.GetComponent<RecordImageSystemsHB>().GetSelectedSprites());

            // �g���₷���悤�ɕϐ��Ɋi�[
            var hit = amounts[0];
            var blow = amounts[1];

            // �񓚐����J�E���g
            count++;

            // 4�q�b�g�Ȃ�N���A
            if (hit == 4)
            {
                detail.GetComponent<DetailSystemHB>().DisappearDetail();
                sound.GetComponent<SoundSystems>().PlaySE(3);
                // ������\��
                answerCard.GetComponent<CardSystemsHB>().AppearAnswer();
                await Task.Delay(2000);
                // �N���A��ʂ�\��
                showHintText.GetComponent<ShowHintSystemHB>().ShowText("4�q�b�g");
                // �q�b�g�ƃu���[�̐����L�^����
                texts.GetComponent<RecordTextSystemsHB>().DrawText(hit, blow);
                await Task.Delay(2000);
                // �Q�[���N���AUI��\��
                endUI.GetComponent<GameOverSystems>().AppearUIOnlyText("���߂łƂ�!!");
            }
            // �񓚂�8�܂ōs������Q�[���I�[�o�[
            else if (count == 8)
            {
                detail.GetComponent<DetailSystemHB>().DisappearDetail();
                sound.GetComponent<SoundSystems>().PlaySE(3);
                answerCard.GetComponent<CardSystemsHB>().AppearAnswer();
                await Task.Delay(2000);
                // �N���A��ʂ�\��
                showHintText.GetComponent<ShowHintSystemHB>().ShowText("������...");
                // �q�b�g�ƃu���[�̐����L�^����
                texts.GetComponent<RecordTextSystemsHB>().DrawText(hit, blow);
                await Task.Delay(2000);
                endUI.GetComponent<GameOverSystems>().AppearUIOnlyText("����΂�����!!");
            }
            // �����ȊO�̓q�b�g�ƃu���[�̐���\������
            else
            {
                sound.GetComponent<SoundSystems>().PlaySE(2);
                showHintText.GetComponent<ShowHintSystemHB>().ShowText(hit + "�q�b�g " + blow + "�u���[");
                // �q�b�g�ƃu���[�̐����L�^����
                texts.GetComponent<RecordTextSystemsHB>().DrawText(hit, blow);
                // �z��̃��Z�b�g
                images.GetComponent<RecordImageSystemsHB>().SetImageNow();
                detail.GetComponent<DetailSystemHB>().MoveDetail();
            }
        }
    }
}