using UnityEngine;

/// <summary>
/// �q���g�摜���N���[�Y����֐��̒�`
/// </summary>

namespace JigsawPuzzle
{
    public class BackButtonEventsJP : MonoBehaviour
    {
        public void OnClickBack()
        {
            this.gameObject.SetActive(false);
        }
    }
}