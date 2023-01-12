using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// �K�v
using PublicUI;

/// <summary>
/// �����̐ݒ�Ƃ��̔���
/// </summary>

namespace HitAndBlow
{
    public class CardSystemsHB : MonoBehaviour
    {
        // �J�[�h�̗��ʂ̉摜
        [SerializeField]
        private Sprite backImage;

        // �J�[�h�̕\�ʂ̉摜����
        [SerializeField]
        private Sprite[] frontImages;

        // �K�v�Ȃ��̂̎擾
        [SerializeField]
        private GameObject endUI;

        // �q���̃J�[�h�I�u�W�F�N�g�̊Ǘ�
        private List<GameObject> cards = new List<GameObject>();

        // ���ۂɎg�p����\�ʂ̉摜�̊Ǘ�
        private Sprite[] selectImages;

        private void Awake()
        {
            // �q���̎擾
            for (var i = 0; i < this.gameObject.transform.childCount; i++)
            {
                cards.Add(this.gameObject.transform.GetChild(i).gameObject);
            }

            foreach (var card in cards)
            {
                // �摜�𗠖ʂɂ���
                card.GetComponent<Image>().sprite = backImage;
                card.GetComponent<Image>().color = Color.white;
            }

            // �\�ʂ̉摜�������V���b�t��
            ShuffleCardImages(frontImages);

            // �g�p����摜��I������
            SelectCardImages();

            // �I�������摜������ɃV���b�t��
            ShuffleCardImages(selectImages);

            for (var i = 0; i < 4; i++)
            {
                this.cards[i].GetComponent<Image>().sprite = selectImages[i];
            }
        }

        // �󂯎�����z����V���b�t������
        // �t�B�b�V���\�C�F�[�c�̃V���b�t��
        private void ShuffleCardImages(Sprite[] images)
        {
            for (var i = images.Length - 1; i > 0; --i)
            {
                var j = Random.Range(0, i + 1);
                var tmp = images[i];
                images[i] = images[j];
                images[j] = tmp;
            }
        }

        // �g�p����摜��I������
        private void SelectCardImages()
        {
            // �q���̐��Ɠ����I�Ԃ悤�ɏ�����
            selectImages = new Sprite[cards.Count];

            for (int i = 0; i < 4; i++)
            {
                // �����_���ȃC���f�b�N�X��I��
                // �����摜���I�΂�邱�Ƃ�����
                var randomIndex = Random.Range(0, frontImages.Length);
                selectImages[i] = frontImages[randomIndex];
            }
        }

        // �J�[�h��\�ɂ��ē���������
        public async void AppearAnswer()
        {
            // �e�q���ɂ��ČĂяo��
            for (var i = 0; i < selectImages.Length; i++)
            {
                AppearAnimation(cards[i], i);
            }

            await Task.Delay(3800);

            // �Q�[���N���AUI��\��
            endUI.GetComponent<GameOverSystems>().AppearUIOnlyText("���߂łƂ�!!");
        }

        // �J�[�h��\�ɂ���A�j���[�V����
        private async void AppearAnimation(GameObject card,int index)
        {
            // �󂯎�����J�[�h�I�u�W�F�N�g�̑傫�����擾
            var scaleX = card.GetComponent<RectTransform>().localScale.x;
            var scaleY = card.GetComponent<RectTransform>().localScale.y;

            // X�����炵�Ă߂��銴���ɂ���
            while (scaleX > 0f)
            {
                card.GetComponent<RectTransform>().localScale = new Vector3(scaleX,scaleY,1f);
                scaleX -= 0.05f;
                await Task.Delay(30);
            }

            // �Ή�����\�̉摜���Z�b�g
            card.GetComponent<Image>().sprite = selectImages[index];

            // X�����ɖ߂�
            while (scaleX < 1f)
            {
                card.GetComponent<RectTransform>().localScale = new Vector3(scaleX, scaleY, 1f);
                scaleX += 0.05f;
                await Task.Delay(30);
            }
        }

        public int[] CheckAnswer(Sprite[] answers)
        {
            int[] hitAndBlowAmounts = new int[2];

            // �󂯎�����z��̒��������������Ƃ���-1��Ԃ�
            if (answers.Length != 4)
            {
                Debug.LogError("argsments length not equals 4");
                return hitAndBlowAmounts;
            }

            // �q�b�g���̊Ǘ�
            var hitAmount = 0;

            // �u���[���̊Ǘ�
            var blowAmount = 0;

            // ���Ƀq�b�g�Ɣ��肳�ꂽ�Ƃ���̓u���[�̔�������Ȃ�
            bool[] didAddAmount = { false, false, false, false };

            // �q�b�g�̔���
            for (var i = 0; i < 4; i++)
            {
                // �����C���f�b�N�X�ɓ����摜������Ȃ�q�b�g
                if (selectImages[i] == answers[i])
                {
                    hitAmount++;
                    didAddAmount[i] = true;
                }
            }

            // �u���[�̔���
            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    // �Ⴄ�C���f�b�N�X�ɓ����摜������
                    // �����ꂪ�q�b�g���肳�ꂽ�摜�łȂ��Ȃ�u���[
                    if (answers[i] == selectImages[j] && 
                        i != j &&
                        !didAddAmount[j])
                    {
                        blowAmount++;
                        didAddAmount[j] = true;
                    }
                }
            }

            // ���߂������Z�b�g����
            hitAndBlowAmounts[0] = hitAmount;
            hitAndBlowAmounts[1] = blowAmount;

            return hitAndBlowAmounts;
        }
    }
}