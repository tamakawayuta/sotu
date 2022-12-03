using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JigsawPuzzle
{
    public class HintButtonEventsJP : MonoBehaviour
    {
        [SerializeField]
        private GameObject hint;

        [SerializeField]
        private Sprite[] sprites;

        private Sprite puzzleImage;

        public void SetHintSprite(string fileName)
        {
            switch (fileName)
            {
                case "puzzleImage":
                    this.puzzleImage = this.sprites[0];
                    break;
                default:
                    Debug.LogWarning("Error: Do not Loaded sprite to hintObject");
                    break;
            }

            this.hint.transform.GetChild(0).GetComponent<Image>().sprite = this.puzzleImage;
            this.hint.SetActive(false);
        }

        public void OnClickHint()
        {
            this.hint.SetActive(true);
        }
    }
}