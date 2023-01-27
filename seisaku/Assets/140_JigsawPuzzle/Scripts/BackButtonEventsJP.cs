using UnityEngine;
using SoundManager;

/// <summary>
/// �q���g�摜���N���[�Y����֐��̒�`
/// </summary>

namespace JigsawPuzzle
{
    public class BackButtonEventsJP : MonoBehaviour
    {
        [SerializeField]
        private GameObject sound;

        public void OnClickBack()
        {
            sound.GetComponent<SoundSystems>().PlaySE(3);
            this.gameObject.SetActive(false);
        }
    }
}