using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �q���g�̊Ǘ�
/// </summary>

namespace JigsawPuzzle
{
    public class HintButtonEventsJP : MonoBehaviour
    {
        // �摜���i�[����I�u�W�F�N�g�̊Ǘ�
        [SerializeField]
        private GameObject hint;

        // �Q�[���Ŏg����摜�̊Ǘ�
        [SerializeField]
        private Sprite[] sprites;

        // ���ۂɎg���Ă���摜�̊Ǘ�
        private Sprite puzzleImage;

        // �q���g�摜�̃Z�b�g
        public void SetHintSprite(string fileName)
        {
            // �Ή�����摜���擾����
            switch (fileName)
            {
                case "puzzleImage":
                    this.puzzleImage = this.sprites[0];
                    break;
                case "ship":
                    this.puzzleImage = this.sprites[1];
                    break;
                case "KisaragiStation":
                    this.puzzleImage = this.sprites[2];
                    break;
                case "bigBang":
                    this.puzzleImage = this.sprites[3];
                    break;
                case "backroom":
                    this.puzzleImage = this.sprites[4];
                    break;
                default:
                    Debug.LogWarning("Error: Do not Loaded sprite to hintObject");
                    break;
            }

            // �摜���Z�b�g
            this.hint.transform.GetChild(0).GetComponent<Image>().sprite = this.puzzleImage;

            // �ʏ�͔�A�N�e�B�u��
            this.hint.SetActive(false);
        }

        // OnClick��`
        public void OnClickHint()
        {
            this.hint.SetActive(true);
        }
    }
}