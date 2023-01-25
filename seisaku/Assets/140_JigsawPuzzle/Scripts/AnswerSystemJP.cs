using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JigsawPuzzle
{
    public class AnswerSystemJP : MonoBehaviour
    {
        [SerializeField]
        private Sprite[] sprites;

        public void SetAnswerImage(string fileName)
        {
            Sprite loadSprite = null;

            switch (fileName)
            {
                case "puzzleImage":
                    loadSprite = this.sprites[0];
                    break;
                case "ship":
                    loadSprite = this.sprites[1];
                    break;
                case "KisaragiStation":
                    loadSprite = this.sprites[2];
                    break;
                case "bigBang":
                    loadSprite = this.sprites[3];
                    break;
                case "backroom":
                    loadSprite = this.sprites[4];
                    break;
                default:
                    Debug.LogWarning("Error: Do not Loaded sprite to hintObject");
                    break;
            }

            this.gameObject.GetComponent<Image>().sprite = loadSprite;

            this.gameObject.SetActive(false);
        }
    }
}