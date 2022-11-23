using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JigsawPuzzle
{
    public class PuzzleSystemJP : MonoBehaviour
    {
        private List<Sprite> sprites = new List<Sprite>();

        private void Awake()
        {
            LoadSprites("puzzleImage");
        }

        private void LoadSprites(string fileName)
        {
            Sprite[] tmp = Resources.LoadAll<Sprite>(fileName);

            foreach (var sprite in tmp)
            {
                this.sprites.Add(sprite);
            }

            Debug.Log(this.sprites.Count);
        }
    }
}