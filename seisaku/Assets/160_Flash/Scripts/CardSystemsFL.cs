using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

//�K�v
using PublicUI;

/// <summary>
/// �Q�[���̒�`
/// </summary>

namespace Flash
{
    public class CardSystemsFL : MonoBehaviour
    {
        // �摜�S
        [SerializeField]
        private Sprite[] sprites;

        // �K�v�Ȃ��̂̎擾
        [SerializeField]
        private GameObject answer;
        [SerializeField]
        private GameObject button;
        [SerializeField]
        private GameObject count;
        [SerializeField]
        private GameObject clearUI;

        // ���ۂɎg�p����摜�Q
        private List<Sprite> useSprites;

        // �����̊Ǘ�
        private List<Sprite> answers;

        // ���𐔂̊Ǘ�
        private int answerAmount = 0;

        // �����ƂȂ�摜�̖����̊Ǘ�
        private int selectSpriteAmount = 2;

        // ������\������Ԋu�̊Ǘ�
        private int delayTime = 2000;

        private async void Awake()
        {
            // �I�������\��
            button.SetActive(false);
            answer.SetActive(false);

            // �摜���V���b�t��
            ShuffleSprites(this.sprites);

            // �摜��I������
            SelectSprites(selectSpriteAmount);

            await Task.Delay(2000);

            // �I�������摜�𓚂��Ƃ��ĕ\��
            await this.answer.GetComponent<AnswerSystemFL>().SetSprites(this.useSprites,2000);
            this.answers = new List<Sprite>(useSprites);

            // �S�ẴX�v���C�g���V���b�t��
            ShuffleSprites(this.sprites);

            // �����������I�u�W�F�N�g���A�N�e�B�u��
            this.answer.SetActive(false);

            // �I������\��
            this.button.GetComponent<SelectSystemFL>().SetButtonSprites(this.sprites);
        }

        // �t�B�b�V���\�C�F�[�c�̃V���b�t��
        private void ShuffleSprites(Sprite[] sprites)
        {
            for (var i = sprites.Length - 1; i > 0; --i)
            {
                var j = Random.Range(0, i + 1);
                var tmp = sprites[i];
                sprites[i] = sprites[j];
                sprites[j] = tmp;
            }
        }

        // �g�p����摜��I������
        private void SelectSprites(int selectAmount)
        {
            useSprites = new List<Sprite>();
            
            for (var i = 0; i < selectAmount; i++)
            {
                useSprites.Add(this.sprites[i]);
            }
        }

        // 2��ڈȍ~�̏���
        private async void ResetSprites()
        {
            // ���𐔂��J�E���g
            this.answerAmount++;

            // �V���b�t������
            ShuffleSprites(this.sprites);

            // �����ƂȂ�摜�̖����𑝂₷�����肷��
            UpdateSelectSpriteAmount();

            // ������I������
            SelectSprites(selectSpriteAmount);

            // ����������
            await this.answer.GetComponent<AnswerSystemFL>().SetSprites(this.useSprites, 2000);
            this.answers = new List<Sprite>(useSprites);

            // �I������ݒ肷��
            ShuffleSprites(this.sprites);
            this.answer.SetActive(false);
            this.button.GetComponent<SelectSystemFL>().SetButtonSprites(this.sprites);
        }

        // �����ƂȂ�摜�̖����𑝂₷������
        private void UpdateSelectSpriteAmount()
        {
            // �p�ӂ����摜���������ƂȂ�摜�̖����������ꍇ�́A������\������Ԋu�����炷
            if (this.selectSpriteAmount > this.sprites.Length - 1)
            {
                UpdateDelayTime();
                return;
            }

            // ���𐔂�3�̔{���ɂȂ�Ƃ��A�����ƂȂ�摜�̖����𑝂₷
            if (this.answerAmount % 3 == 0)
            {
                this.selectSpriteAmount++;
            }
        }

        // ������\������Ԋu�̐ݒ�
        private void UpdateDelayTime()
        {
            // �����l�̐ݒ�
            if (this.delayTime < 0)
            {
                return;
            }

            this.delayTime -= 20;
        }

        // �����̔���
        public void CheckAnswer(Sprite sprite)
        {
            // �����̏���
            if (this.answers.Contains(sprite))
            {
                // �������珜��
                this.answers.Remove(sprite);

                // �X�R�A�����Z
                this.count.GetComponent<CountSystemFL>().AddCount();

                // �S�ĉ񓚂��ꂽ�玟�̖����o��
                if (this.answers.Count == 0)
                {
                    this.button.SetActive(false);
                    ResetSprites();
                }
            }
            // �s�����̏���
            else
            {
                this.clearUI.GetComponent<GameOverSystems>().AppearGameOverUI(this.count.GetComponent<CountSystemFL>().GetCount());
            }
        }
    }
}