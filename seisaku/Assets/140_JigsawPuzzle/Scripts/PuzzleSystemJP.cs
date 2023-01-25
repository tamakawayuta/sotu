using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// �K�v
using PublicUI;

/// <summary>
/// �Q�[���̒�`
/// </summary>

namespace JigsawPuzzle
{
    public class PuzzleSystemJP : MonoBehaviour
    {
        // �K�v�Ȃ��̂̎擾
        [SerializeField]
        private GameObject fields;
        [SerializeField]
        private GameObject clear;
        [SerializeField]
        private GameObject hintButton;
        [SerializeField]
        private GameObject answerImage;

        // �p�Y���s�[�X�Ɋi�[����o���o���̉摜�̊Ǘ�
        private List<Sprite> sprites = new List<Sprite>();

        // �����̊Ǘ�
        private List<Sprite> answers;

        // �I������Ă���s�[�X�̊Ǘ�
        private List<GameObject> selected = new List<GameObject>();

        // �g�p����摜�̖��O
        private string fileName;

        private async void Awake()
        {
            // �摜�������_���ɑI��
            var index = Random.Range(0, 5);
            SetFileName(index);

            // �Ή�����摜��ǂݍ���
            LoadSprites(this.fileName);
            hintButton.GetComponent<HintButtonEventsJP>().SetHintSprite(this.fileName);
            this.answerImage.GetComponent<AnswerSystemJP>().SetAnswerImage(this.fileName);

            fields.GetComponent<FieldSystemJP>().InstantiateFields(this.sprites.Count);
            fields.GetComponent<FieldSystemJP>().DrawSprites(this.sprites);

            List<GameObject> fieldState = fields.GetComponent<FieldSystemJP>().GetFields();

            foreach (var field in fieldState)
            {
                field.GetComponent<Image>().color = Color.white;
                field.GetComponent<Button>().enabled = false;
            }

            hintButton.SetActive(false);

            await Task.Delay(5000);

            for (var i = 0; i < 5; i++)
            {
                ShuffleSprites(this.sprites);
                fields.GetComponent<FieldSystemJP>().DrawSprites(this.sprites);
                await Task.Delay(20);
            }

            // �o���o���摜���V���b�t��
            ShuffleSprites(this.sprites);

            // �摜���s�[�X�ɃZ�b�g
            fields.GetComponent<FieldSystemJP>().DrawSprites(this.sprites);

            foreach (var field in fieldState)
            {
                field.GetComponent<Image>().color = new Color(53f, 53f, 53f);
                field.GetComponent<Button>().enabled = true;
            }

            // ���ɐ������ꏊ�ɂ���摜�ɂ��Ă̏���
            UpdateTrueFieldState();

            hintButton.SetActive(true);
        }

        // �Ή�����摜���̃Z�b�g
        private void SetFileName(int index)
        {
            switch (index)
            {
                case 0:
                    this.fileName = "puzzleImage";
                    break;
                case 1:
                    this.fileName = "ship";
                    break;
                case 2:
                    this.fileName = "KisaragiStation";
                    break;
                case 3:
                    this.fileName = "bigBang";
                    break;
                case 4:
                    this.fileName = "backroom";
                    break;
                default:
                    Debug.LogError("Error: Illegal Index");
                    break;
            }
        }

        // ���ɐ������ʒu�ɂ���摜�͑I���ł��Ȃ��悤�ɂ���
        private void UpdateTrueFieldState()
        {
            List<GameObject> fieldState = fields.GetComponent<FieldSystemJP>().GetFields();

            for (var i = 0; i < fieldState.Count; i++)
            {
                if (this.sprites[i] == answers[i])
                {
                    fieldState[i].GetComponent<Image>().color = Color.white;
                    fieldState[i].GetComponent<Button>().enabled = false;
                }
            }
        }

        // �摜�̃��[�h
        private void LoadSprites(string fileName)
        {
            Sprite[] tmp = Resources.LoadAll<Sprite>(fileName);

            foreach (var sprite in tmp)
            {
                this.sprites.Add(sprite);
            }

            this.answers = new List<Sprite>(sprites);
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

        // �摜�����ւ���
        private void ChangeSprite()
        {
            // �I�����ꂽ2�̉摜���擾
            Sprite sprite1 = this.selected[0].GetComponent<Image>().sprite;
            Sprite sprite2 = this.selected[1].GetComponent<Image>().sprite;

            // �X���b�s���O
            this.selected[0].GetComponent<Image>().sprite = sprite2;
            this.selected[1].GetComponent<Image>().sprite = sprite1;

            // �đI���ł���悤�ɂ���
            foreach (var field in selected)
            {
                field.GetComponent<Button>().enabled = true;
            }

            // �������ʒu�ɂ���摜�͑I���ł��Ȃ��悤�ɂ���
            CheckPosition();

            // �����̔���
            CheckAnswer();

            // ���Z�b�g
            this.selected.Clear();
        }

        // �����ƃt�B�[���h����v���邩���肷��
        private async void CheckAnswer()
        {
            List<GameObject> fieldState = fields.GetComponent<FieldSystemJP>().GetFields();

            for (var i = 0; i < 50; i++)
            {
                // �e�ʒu�̉摜�ŕs��v������ΐ����łȂ�
                if (fieldState[i].GetComponent<Image>().sprite != this.answers[i])
                {
                    return;
                }
            }

            this.answerImage.SetActive(true);

            await Task.Delay(3000);

            // �����̂Ƃ��̓N���A��ʂ��Ăяo��
            clear.GetComponent<GameOverSystems>().AppearUIOnlyText("���߂łƂ�!!");
        }

        // �������ʒu���̔���
        private void CheckPosition()
        {
            foreach (var field in selected)
            {
                // �I�����ꂽ�I�u�W�F�N�g�̃C���f�b�N�X���擾
                var index = field.GetComponent<FieldButtonEventsJP>().GetIndex();

                // �摜����v����Ȃ�đI���ł��Ȃ��悤�ɂ���
                if (field.GetComponent<Image>().sprite == this.answers[index])
                {
                    field.GetComponent<Button>().enabled = false;
                }
            }
        }

        // �I�����ꂽ�I�u�W�F�N�g���Ǘ�����ϐ��̒ǉ��֐�
        public void AddSelected(GameObject obj)
        {
            this.selected.Add(obj);

            // 2�ɂȂ�����X���b�s���O����
            if (this.selected.Count == 2)
            {
                ChangeSprite();
            }
        }
    }
}