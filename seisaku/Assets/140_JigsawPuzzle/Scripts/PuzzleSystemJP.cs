using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JigsawPuzzle
{
    public class PuzzleSystemJP : MonoBehaviour
    {
        [SerializeField]
        private GameObject fields;

        private List<Sprite> sprites = new List<Sprite>();
        private List<Sprite> answers;

        private void Awake()
        {
            LoadSprites("puzzleImage");
            ShuffleSprites(this.sprites);
            fields.GetComponent<FieldSystemJP>().InstantiateFields(this.sprites.Count);
            fields.GetComponent<FieldSystemJP>().DrawSprites(this.sprites);

            Debug.Log(this.sprites.Count);
            Debug.Log(this.answers.Count);
        }

        private void LoadSprites(string fileName)
        {
            Sprite[] tmp = Resources.LoadAll<Sprite>(fileName);

            foreach (var sprite in tmp)
            {
                this.sprites.Add(sprite);
            }

            this.answers = new List<Sprite>(sprites);
        }

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
    }
}