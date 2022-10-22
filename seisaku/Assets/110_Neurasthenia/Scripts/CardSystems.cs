using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using SoundManager;

namespace Neurasthenia
{
    /// <summary>
    /// �Q�[���̒�`
    /// </summary>

    [DisallowMultipleComponent]
    public sealed class CardSystems : MonoBehaviour
    {
        [SerializeField,TooltipAttribute("�J�[�h�̗��ʂ̃X�v���C�g")]
        private Sprite backImage;         
        [SerializeField,TooltipAttribute("�J�[�h�̕\�ʂ̃X�v���C�g")]
        private Sprite[] frontImages;       

        [SerializeField, TooltipAttribute("HP�Q�[�W������")]
        private GameObject hpGauge;
        [SerializeField, TooltipAttribute("�X�R�A���L�^����e�L�X�g������")]
        private GameObject counter;
        [SerializeField, TooltipAttribute("�p�l��������")]
        private GameObject panel;
        [SerializeField, TooltipAttribute("���I�u�W�F�N�g������")]
        private GameObject soundManager;

        //�J�[�h�I�u�W�F�N�g�̊Ǘ�
        private List<GameObject> cards = new List<GameObject>();
        //�I�����ꂽ�J�[�h�̊Ǘ�
        private List<GameObject> clickCards = new List<GameObject>();
        //�I�����ꂽ�摜�f�ނ̊Ǘ�
        private Sprite[] selectImages;

        private void Awake()
        {
            //�J�[�h�I�u�W�F�N�g�̎擾
            for (var i = 0;i < this.gameObject.transform.childCount; i++)
            {
                cards.Add(this.gameObject.transform.GetChild(i).gameObject);
            }

            //�J�[�h�I�u�W�F�N�g�𗠖ʂɂ���
            foreach(var card in cards)
            {
                card.GetComponent<Image>().sprite = backImage;
            }

            //�摜�f�ނ��V���b�t������
            ShuffleCardImages(frontImages);
            //���̒�����摜��I������
            SelectCardImages();
            //�I�������摜���V���b�t������
            ShuffleCardImages(selectImages);

            //������
            this.clickCards.Clear();
        }

        private void ShuffleCardImages(Sprite[] images)
        {
            //�t�B�b�V���[-�C�F�[�c�̃V���b�t���@
            //�����Ń������g�p�ʂ��Œ�
            for (var i = images.Length - 1; i > 0; --i)
            {
                var j = Random.Range(0, i + 1);
                var tmp = images[i];
                images[i] = images[j];
                images[j] = tmp;
            }
        }

        private void SelectCardImages()
        {
            //�J�[�h�I�u�W�F�N�g�Ɠ����v�f���̔z���p��
            selectImages = new Sprite[cards.Count];

            for (int i = 0; i < cards.Count/2; i++)
            {
                //�J�[�h��I�����Ċi�[����
                //�����摜��2���K�v�Ȃ̂Ŋi�[����
                selectImages[i] = frontImages[i];
                selectImages[i + cards.Count / 2] = frontImages[i];
            }
        }

        //�I�����ꂽ�J�[�h�������Ă��邩�̔��菈��
        public void CheckAnswers()
        {
            //�I�����ꂽ�J�[�h�̉摜������
            if (this.clickCards[0].GetComponent<Image>().sprite == this.clickCards[1].GetComponent<Image>().sprite)
            {
                //�I�����ꂽ�J�[�h�L�^�����Z�b�g
                this.clickCards.Clear();
                //������SE�̍Đ�
                soundManager.GetComponent<SoundSystems>().PlaySE(2);
                //�X�R�A���Z
                counter.GetComponent<CounterSystems>().UpdateCount();
                //�S�Ă߂���ꂽ��J�[�h���Ĕz�u����
                if (counter.GetComponent<CounterSystems>().CheckCount())
                {
                    ResetCards();
                }
            }
            //�I�����ꂽ�J�[�h�̉摜���Ⴄ
            else
            {
                //�J�[�h�𗠕Ԃ��ɖ߂�
                RemoveCards();
                //�p�l�����A�N�e�B�u��
                panel.GetComponent<PanelSystems>().switchPanelActive();
                //�ԈႢ��SE���Đ�
                soundManager.GetComponent<SoundSystems>().PlaySE(3);
                //Hp�����炷
                hpGauge.GetComponent<HpSystems>().DamagedHp();
            }
        }

        //�I�����ꂽ�J�[�h�I�u�W�F�N�g�����摜��Ԃ�
        public Sprite GetCardImages(int cardNum)
        {
            return this.selectImages[cardNum];
        }

        public void SetClickCards(GameObject clicked)
        {
            //�J�[�h���߂���SE�̍Đ�
            soundManager.GetComponent<SoundSystems>().PlaySE(0);
            //�I�����ꂽ�J�[�h�I�u�W�F�N�g�̕ۑ�
            clickCards.Add(clicked);
        }

        //�����J�[�h�I�u�W�F�N�g���I������Ă��邩�Ԃ�
        public int GetClickCardsCount()
        {
            return this.clickCards.Count;
        }

        private async void RemoveCards()
        {
            //�J�[�h�̉摜�������邽�߂̃f�B���C
            await Task.Delay(1000);

            //�|�[�Y��ʂ�Q�[���I�[�o�[���\������Ă���Ƃ��͏����𒆒f
            while (true)
            {
                if (Time.timeScale == 1)
                {
                    break;
                }
                await Task.Delay(2000);
            }

            //�I�����ꂽ�J�[�h�𗠖ʂɖ߂�
            foreach (var card in clickCards)
            {
                card.GetComponent<Image>().sprite = backImage;
                //�J�[�h���܂��I�ׂ�悤�ɂ���
                card.GetComponent<Button>().enabled = true;
            }

            //�I�������L�^�̃��Z�b�g
            this.clickCards.Clear();
            //�p�l�����A�N�e�B�u��
            panel.GetComponent<PanelSystems>().switchPanelActive();
        }

        private async void ResetCards()
        {
            //�Ĕz�u����SE���Đ�
            soundManager.GetComponent<SoundSystems>().PlaySE(1);

            //�̗͂�2�񕜂���
            hpGauge.GetComponent<HpSystems>().HealedHp();
            hpGauge.GetComponent<HpSystems>().HealedHp();

            //�����҂��Ă���J�[�h�𗠕Ԃ�
            await Task.Delay(700);
            foreach (var card in cards)
            {
                card.GetComponent<Image>().sprite = backImage;
                //�J�[�h��I�ׂ�悤�ɂ���
                card.GetComponent<Button>().enabled = true;
            }

            //�摜�f�ނ̍đI��
            ShuffleCardImages(frontImages);
            SelectCardImages();
            ShuffleCardImages(selectImages);
        }
    }
}