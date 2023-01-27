using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

//�K�v
using PublicUI;
using SoundManager;

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
        [SerializeField]
        private GameObject praise;
        [SerializeField]
        private GameObject sound;

        // ���ۂɎg�p����摜�Q
        private List<Sprite> useSprites;

        // �����̊Ǘ�
        private Queue<Sprite> answers;

        // ���𐔂̊Ǘ�
        private int answerAmount = 0;

        // �����ƂȂ�摜�̖����̊Ǘ�
        private int selectSpriteAmount = 1;

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
            this.answers = new Queue<Sprite>(useSprites);

            // �S�ẴX�v���C�g���V���b�t��
            ShuffleSprites(this.sprites);

            while (true)
            {
                if (this.useSprites.Count == 8)
                {
                    break;
                }

                var index = Random.Range(0, this.sprites.Length);

                if (!this.useSprites.Contains(this.sprites[index]))
                {
                    this.useSprites.Add(this.sprites[index]);
                }
            }

            ShuffleSprites(this.useSprites);

            while (true)
            {
                if (Time.timeScale == 1)
                {
                    break;
                }
                await Task.Delay(2000);
            }

            // �����������I�u�W�F�N�g���A�N�e�B�u��
            this.answer.SetActive(false);

            // �I������\��
            sound.GetComponent<SoundSystems>().PlaySE(2);
            this.button.GetComponent<SelectSystemFL>().SetButtonSprites(this.useSprites);
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

        private void ShuffleSprites(List<Sprite> sprites)
        {
            for (var i = sprites.Count - 1; i > 0; --i)
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
            this.answers = new Queue<Sprite>(useSprites);

            // �I������ݒ肷��
            ShuffleSprites(this.sprites);

            while (true)
            {
                if (this.useSprites.Count == 8)
                {
                    break;
                }

                var index = Random.Range(0, this.sprites.Length);

                if (!this.useSprites.Contains(this.sprites[index]))
                {
                    this.useSprites.Add(this.sprites[index]);
                }
            }

            ShuffleSprites(this.useSprites);

            while (true)
            {
                if (Time.timeScale == 1)
                {
                    break;
                }
                await Task.Delay(2000);
            }

            this.answer.SetActive(false);

            sound.GetComponent<SoundSystems>().PlaySE(2);
            this.button.GetComponent<SelectSystemFL>().SetButtonSprites(this.useSprites);
        }

        // �����ƂȂ�摜�̖����𑝂₷������
        private void UpdateSelectSpriteAmount()
        {
            // �p�ӂ����摜���������ƂȂ�摜�̖����������ꍇ�́A������\������Ԋu�����炷
            if (this.selectSpriteAmount > 7)
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
        public async void CheckAnswer(Sprite sprite)
        {
            var answer = this.answers.Dequeue();

            // �����̏���
            if (answer == sprite)
            {
                sound.GetComponent<SoundSystems>().PlaySE(1);
                // �X�R�A�����Z
                this.count.GetComponent<CountSystemFL>().AddCount();

                // �S�ĉ񓚂��ꂽ�玟�̖����o��
                if (this.answers.Count == 0)
                {

                    await this.praise.GetComponent<PraiseSystemFL>().AppearText();

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