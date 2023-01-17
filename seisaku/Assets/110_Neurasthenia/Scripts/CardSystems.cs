using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// �K�v
using SoundManager;

/// <summary>
/// ���C���Q�[���̒�`
/// </summary>

namespace Neurasthenia
{
    public class CardSystems : MonoBehaviour
    {
        // �J�[�h�̗��ʂ̉摜
        [SerializeField]
        private Sprite backImage;

        // �J�[�h�̕\�ʂ̉摜����
        [SerializeField]
        private Sprite[] frontImages;

        // �K�v�ȃI�u�W�F�N�g�̎擾
        [SerializeField]
        private GameObject hpGauge;
        [SerializeField]
        private GameObject counter;
        [SerializeField]
        private GameObject panel;
        [SerializeField]
        private GameObject soundManager;

        // �I�΂ꂽ�J�[�h�̕\�ʂ̉摜���Ǘ�����z��
        private Sprite[] selectImages;

        // ���̃I�u�W�F�N�g�z���̎q���̃Q�[���I�u�W�F�N�g���Ǘ����郊�X�g
        private List<GameObject> cards = new List<GameObject>();

        // �I�����ꂽ�J�[�h�I�u�W�F�N�g���Ǘ����郊�X�g
        private List<GameObject> clickCards = new List<GameObject>();

        private void Awake()
        {
            // ������
            this.clickCards.Clear();

            // �q���̃J�[�h�I�u�W�F�N�g���擾����
            for (var i = 0;i < this.gameObject.transform.childCount; i++)
            {
                cards.Add(this.gameObject.transform.GetChild(i).gameObject);
            }

            // �擾�����q���̃C���[�W�𗠖ʂ̉摜�ɂ���
            foreach(var card in cards)
            {
                card.GetComponent<Image>().sprite = backImage;
            }

            // �g�p����\�ʂ̉摜��I������
            SelectCards();
        }

        // �n���ꂽ�摜�̔z����V���b�t������֐�
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

        // �g�p����J�[�h��I������
        private void SelectCardImages()
        {
            // ������
            selectImages = new Sprite[cards.Count];

            // �C���f�b�N�X��������������K�萔�I��
            for (int i = 0; i < cards.Count/2; i++)
            {
                selectImages[i] = frontImages[i];
                // �_�o����͓����摜��2�y�A�ɂȂ邽�߁A����1���ǉ�����
                selectImages[i + cards.Count / 2] = frontImages[i];
            }
        }

        private void SelectCards()
        {
            // �\�ʂɂȂ���̉摜�������V���b�t������
            ShuffleCardImages(frontImages);
            // �V���b�t�������摜��������g�p����摜��I��
            SelectCardImages();
            // �I�񂾉摜������ɍ�����
            ShuffleCardImages(selectImages);
        }

        // �s�����̂Ƃ��J�[�h��߂��񓯊��֐�
        private async void RemoveCards()
        {
            // �J�[�h�̊G�����o�������邽�߁A�f�B���C������
            await Task.Delay(1000);

            // �|�[�Y���ɃJ�[�h�𗠕Ԃ��Ȃ��l�ɂ���
            while (true)
            {
                if (Time.timeScale == 1)
                {
                    break;
                }
                await Task.Delay(2000);
            }

            // �p�l�����A�N�e�B�u�ɂ��đ��̃J�[�h��I�������Ȃ��悤�ɂ���
            panel.GetComponent<PanelSystems>().switchPanelActive();

            foreach (var card in clickCards)
            {
                // �I�����ꂽ�J�[�h���đI���ł���悤�ɂ���
                card.GetComponent<Button>().enabled = true;

                // �I�����ꂽ�J�[�h�𗠖ʂɂ���
                card.GetComponent<Image>().sprite = backImage;
            }

            // ���X�g�����Z�b�g
            this.clickCards.Clear();
        }

        // �J�[�h���Ĕz�u����񓯊��֐�
        private async void ResetCards()
        {
            // �Ĕz�u����悤��SE�𗬂�
            soundManager.GetComponent<SoundSystems>().PlaySE(1);

            // HP��2�񕜂���
            hpGauge.GetComponent<HpSystems>().HealedHp();
            hpGauge.GetComponent<HpSystems>().HealedHp();

            foreach (var card in cards)
            {
                // ��̃J�[�h�𗠖ʂɂ���
                card.GetComponent<CardEvents>().TurnBack(this.backImage);
            }

            // �Ĕz�u���ꂽ�����o�����߂ɏ����f�B���C
            await Task.Delay(700);

            // �g�p����\�ʂ̉摜���đI������
            SelectCards();

            foreach (var card in cards)
            {
                // ��̃J�[�h��I���ł���悤�ɂ���
                card.GetComponent<Button>().enabled = true;
            }
        }

        // �I�����ꂽ�摜�����������肷��
        public void CheckAnswers()
        {
            // �摜�������ł���Ƃ�
            if (this.clickCards[0].GetComponent<Image>().sprite == this.clickCards[1].GetComponent<Image>().sprite)
            {
                // ���X�g�����Z�b�g
                this.clickCards.Clear();

                // ������SE���Đ�����
                soundManager.GetComponent<SoundSystems>().PlaySE(2);

                // �J�E���g�I�u�W�F�N�g�ɐ��𐔂𑝂₷�悤���߂���
                counter.GetComponent<CounterSystems>().UpdateCount();

                // ������̃J�[�h���S�ĕ\�ɂȂ�����
                // �܂�A���𐔂�5�̔{���ɂȂ�����
                if (counter.GetComponent<CounterSystems>().CheckCount())
                {
                    // �J�[�h���Ĕz�u����
                    ResetCards();
                }
            }
            else
            {
                // �J�[�h�𗠖ʂɖ߂�
                RemoveCards();
                
                // �p�l����߂�
                panel.GetComponent<PanelSystems>().switchPanelActive();

                // �s������SE���Đ�����
                soundManager.GetComponent<SoundSystems>().PlaySE(3);

                // HP�����炷
                hpGauge.GetComponent<HpSystems>().DamagedHp();
            }
        }

        // �I�����ꂽ�J�[�h�̉摜�̃Q�b�^�[
        public Sprite GetCardImages(int cardNum)
        {
            return this.selectImages[cardNum];
        }

        // �I�����ꂽ�J�[�h�I�u�W�F�N�g��ǉ�����
        public void SetClickCards(GameObject clicked)
        { 
            clickCards.Add(clicked);
            
            //�I�������Ƃ���SE���Đ�����
            soundManager.GetComponent<SoundSystems>().PlaySE(0);
        }

        // �I�����ꂽ�J�[�h�I�u�W�F�N�g�̐���Ԃ�
        public int GetClickCardsCount()
        {
            return this.clickCards.Count;
        }
    }
}