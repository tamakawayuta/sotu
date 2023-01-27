using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// �K�v
using PublicUI;
using SoundManager;

/// <summary>
/// �Q�[���̒�`
/// </summary>

namespace QuicklyImage
{
    public class CardSystemsQI : MonoBehaviour
    {
        // �J�[�h�̉摜����
        [SerializeField]
        private Sprite[] sprites;

        // �K�v�Ȃ��̂̎擾
        [SerializeField]
        private GameObject select;
        [SerializeField]
        private GameObject answerObject;
        [SerializeField]
        private GameObject time;
        [SerializeField]
        private GameObject guide;
        [SerializeField]
        private GameObject score;
        [SerializeField]
        private GameObject clearUI;
        [SerializeField]
        private GameObject sound;

        // �I�����ƂȂ�I�u�W�F�N�g����
        private List<GameObject> selectButtons = new List<GameObject>();

        // ���ۂɎg�p����摜�̊Ǘ�
        private List<Sprite> useSprites = new List<Sprite>();

        // ��������
        private int delayTime = 100;

        // �V���b�t���A�j���[�V�����̃t���O
        private bool isShuffleTime = true;

        private async void Awake()
        {
            // �I�����ƂȂ�I�u�W�F�N�g���擾
            for (var i = 0; i < select.transform.childCount; i++)
            {
                this.selectButtons.Add(this.select.transform.GetChild(i).gameObject);
            }

            // �g�p����摜�����X�g�ŊǗ�
            foreach (var sprite in sprites)
            {
                this.useSprites.Add(sprite);
            }

            sound.GetComponent<SoundSystems>().PlaySE(0);
            ShuffleAnimation();

            // �摜���V���b�t��
            ShuffleSprites(useSprites);

            //�t�F�[�h�C�����I���܂őҋ@
            await Task.Delay(2000);

            // �|�[�Y���Ȃ���������܂őҋ@
            while (Time.timeScale == 0)
            {
                await Task.Delay(1000);
            }

            this.isShuffleTime = false;

            // �I�������Z�b�g
            for (var i = 0; i < selectButtons.Count; i++)
            {
                selectButtons[i].GetComponent<SelectButtonEventsQI>().SetSprite(useSprites[i]);
            }

            // �������Z�b�g
            SetAnswer();

            // �J�E���g�_�E�����J�n����
            this.time.GetComponent<TimeSystemsQI>().StartCountdown(delayTime);
        }

        // 2��ڈȍ~�̏���
        private async void ReShuffle()
        {
            // �摜��������x�V���b�t������
            ShuffleSprites(useSprites);

            // �����Ԃ��J���ăv���C���[�Ɉ�ċz������
            await Task.Delay(1000);

            // �|�[�Y���Ȃ���������܂őҋ@
            while (Time.timeScale == 0)
            {
                await Task.Delay(1000);
            }

            sound.GetComponent<SoundSystems>().PlaySE(0);
            ShuffleAnimation();

            await Task.Delay(700);

            // �����o�����Ƃ�`����
            this.guide.GetComponent<GuideSystemQI>().SetText("���[��..!");
            await Task.Delay(1000);

            this.isShuffleTime = false;

            // �I�������Z�b�g
            for (var i = 0; i < selectButtons.Count; i++)
            {
                selectButtons[i].GetComponent<SelectButtonEventsQI>().SetSprite(useSprites[i]);
            }

            // �������Z�b�g
            SetAnswer();

            // �J�E���g�_�E�����J�n
            this.time.GetComponent<TimeSystemsQI>().StartCountdown(delayTime);
        }

        // �t�B�b�V���\�C�F�[�c�̃V���b�t��
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

        // ������ݒ肷��
        private void SetAnswer()
        {
            // �I�����Ƃ��Đݒ肵��4�̒�����1�𓚂��Ƃ���
            var index = Random.Range(0, 4);
            this.answerObject.GetComponent<AnswerEventsQI>().SetAnswerSprite(this.useSprites[index]);

            this.guide.GetComponent<GuideSystemQI>().SetText("���������ׁI");
        }

        //  �V���b�t���̃A�j���[�V����
        private async void ShuffleAnimation()
        {
            this.isShuffleTime = true;

            while (this.isShuffleTime)
            {
                foreach (var button in selectButtons)
                {
                    var index = Random.Range(0, this.sprites.Length);
                    button.GetComponent<Image>().sprite = this.sprites[index];
                }

                var answerIndex = Random.Range(0, this.sprites.Length);
                this.answerObject.GetComponent<Image>().sprite = this.sprites[answerIndex];

                await Task.Delay(20);
            }
        }

        // ���x���f�U�C��
        // �R�[������قǐ������Ԃ��Z���Ȃ�
        private void UpdateDelayTime()
        {
            if (this.delayTime < 10)
            {
                return;
            }

            this.delayTime -= 3;
        }

        // �����̔���
        public async void CheckAnswer(Sprite sprite)
        {
            // �����̏���
            if (sprite == this.answerObject.GetComponent<Image>().sprite)
            {
                sound.GetComponent<SoundSystems>().PlaySE(1);
                this.guide.GetComponent<GuideSystemQI>().SetText("�����ˁI");
                this.time.GetComponent<TimeSystemsQI>().SetIsTimeUp();

                // �������Ԃ����炷
                UpdateDelayTime();

                // ���Ԃɉ����ăX�R�A�����Z����
                float bonusValue = this.time.GetComponent<Image>().fillAmount;
                bonusValue *= 100;
                int newScore = (int)bonusValue;

                for (var i=1;i < newScore + 1; i++)
                {
                    this.score.GetComponent<ScoreSystemsQI>().AddScore(i);
                    await Task.Delay(20);
                }
            }
            // �s�����Ȃ�Q�[���I�[�o�[
            else
            {
                AppearClearUI();
            }

            // �ăV���b�t��
            ReShuffle();
        }

        // �Q�[���I�����̃E�B���h�E��\������
        public void AppearClearUI()
        {
            this.clearUI.GetComponent<GameOverSystems>().AppearGameOverUI(this.score.GetComponent<ScoreSystemsQI>().GetScore());
        }
    }
}