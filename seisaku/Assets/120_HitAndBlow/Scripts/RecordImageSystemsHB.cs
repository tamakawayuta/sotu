using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���[�U�[�����͂����摜���Ǘ�����
/// </summary>

namespace HitAndBlow
{
    public class RecordImageSystemsHB : MonoBehaviour
    {
        // �K�v�Ȃ��̂̎擾
        [SerializeField]
        private GameObject selectButton;

        // ���g���Ă�����̓I�u�W�F�N�g
        private GameObject imageNow;

        // �񓚂��󂯕t����Q�[���I�u�W�F�N�g�̎擾
        private Queue<GameObject> images = new Queue<GameObject>();

        // ���͂��ꂽ�摜�̊Ǘ�
        private Sprite[] selectedSprite = new Sprite[4];
        
        // �S�ĉ񓚂����͂��ꂽ���Ǘ�
        private bool isImageSetFull = false;

        private void Awake()
        {
            // ���̓I�u�W�F�N�g�̃{�^���������Ȃ��l�ɏ�����
            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    this.gameObject.transform.GetChild(i).transform.GetChild(j).GetComponent<Button>().enabled = false;
                }
            }

            // ���̓I�u�W�F�N�g�̎擾
            for (var i = 0; i < 8; i++)
            {
                images.Enqueue(this.gameObject.transform.GetChild(i).gameObject);
            }

            // �g�p������̓I�u�W�F�N�g�����o��
            imageNow = images.Dequeue();

            for (var i = 0; i < 4; i++)
            {
                this.imageNow.transform.GetChild(i).GetComponent<Image>().color = Color.white;
            }
        }

        private void Update()
        {
            // ���[�U�[���摜��I�����Ă���Ȃ���͂��󂯕t����
            if (selectButton.GetComponent<SelectButtonEventsHB>().GetSelectSpriteNow() != null)
            {
                for (var i = 0; i < 4; i++)
                {
                    imageNow.transform.GetChild(i).GetComponent<Button>().enabled = true;
                }
            }
        }

        // ���̓I�u�W�F�N�g��OnClick�֐�
        public void OnClickRecord(int index)
        {
            // �Ή�������̓I�u�W�F�N�g�̃X�v���C�g�ɑI�����ꂽ�X�v���C�g������
            imageNow.transform.GetChild(index).GetComponent<Image>().sprite = selectButton.GetComponent<SelectButtonEventsHB>().GetSelectSpriteNow();
            
            // ���͂��ꂽ�X�v���C�g���L�^����
            selectedSprite[index] = imageNow.transform.GetChild(index).GetComponent<Image>().sprite;

            // �F��߂�
            imageNow.transform.GetChild(index).GetComponent<Image>().color = Color.white;

            // �񓚂��S�ē��͂��ꂽ���m�F����
            SetIsImageSetFull();
        }

        // ���Z�b�g�֐�
        public void SetImageNow()
        {
            // ���͂��ꂽ�摜���L�^����z������Z�b�g
            selectedSprite = new Sprite[4];

            // ���͍ς݂̌Â����̓I�u�W�F�N�g�𖳌���
            SetActiveImageNow(false);

            // �V�������̓I�u�W�F�N�g���擾
            imageNow = images.Dequeue();

            // �V�������̓I�u�W�F�N�g�̓��͂�L����
            SetActiveImageNow(true);

            for (var i = 0; i < 4; i++)
            {
                this.imageNow.transform.GetChild(i).GetComponent<Image>().color = Color.white;
            }

            // �t���O�����Z�b�g
            this.isImageSetFull = false;
        }

        // ���͂��ꂽ�摜�z��̃Q�b�^�[
        public Sprite[] GetSelectedSprites()
        {
            return this.selectedSprite;
        }

        // �t���O�̃Q�b�^�[
        public bool GetIsImageSetFull()
        {
            return this.isImageSetFull;
        }

        // �t���O�̔���
        private void SetIsImageSetFull()
        {
            for (var i = 0; i < 4; i++)
            {
                // �L�������ꂽ���̓I�u�W�F�N�g�̑S�Ăɉ񓚂����͂���Ă��Ȃ��Ȃ�t���O�͗����Ȃ�
                if (imageNow.transform.GetChild(i).GetComponent<Image>().sprite == null)
                {
                    isImageSetFull = false;
                    return;
                }
            }

            isImageSetFull = true;
        }

        // ���̓I�u�W�F�N�g�̗L�����Ɩ�����
        private void SetActiveImageNow(bool val)
        {
            for (var i = 0; i < 4; i++)
            {
                imageNow.transform.GetChild(i).GetComponent<Button>().enabled = val;
            }
        }
    }
}