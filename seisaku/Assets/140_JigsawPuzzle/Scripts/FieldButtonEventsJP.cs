using UnityEngine;
using UnityEngine.UI;
using SoundManager;

/// <summary>
/// �p�Y���s�[�X��OnClick
/// </summary>

namespace JigsawPuzzle
{
    public class FieldButtonEventsJP : MonoBehaviour
    {
        // �t�B�[���h���Ǘ�����I�u�W�F�N�g
        private PuzzleSystemJP field;

        // �e�s�[�X�����C���f�b�N�X
        private int index;

        private void Awake()
        {
            // �擾
            this.field = GameObject.Find("Background_Image").GetComponent<PuzzleSystemJP>();
        }

        // �C���f�b�N�X�̃Z�b�^�[
        public void SetIndex(int index)
        {
            this.index = index;
        }

        // �C���f�b�N�X�̃Q�b�^�[
        public int GetIndex()
        {
            return this.index;
        }

        // OnClick��`
        public void OnClickfield()
        {
            // ���̃s�[�X���đI���ł��Ȃ��悤�ɂ���
            this.gameObject.GetComponent<Button>().enabled = false;
            this.gameObject.GetComponent<Image>().color = Color.white;

            GameObject.Find("SoundManager").GetComponent<SoundSystems>().PlaySE(0);

            // �I�����ꂽ���Ƃ�ʒm����
            field.AddSelected(this.gameObject);
        }
    }
}