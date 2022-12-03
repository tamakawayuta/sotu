using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PublicUI;

namespace JigsawPuzzle
{
    public class PuzzleSystemJP : MonoBehaviour
    {
        [SerializeField]
        private GameObject fields;
        [SerializeField]
        private GameObject clear;
        [SerializeField]
        private GameObject hintButton;

        private List<Sprite> sprites = new List<Sprite>();
        private List<Sprite> answers;

        private List<GameObject> selected = new List<GameObject>();

        private string fileName;

        private void Awake()
        {
            var index = Random.Range(0, 1);
            SetFileName(index);

            LoadSprites(this.fileName);
            hintButton.GetComponent<HintButtonEventsJP>().SetHintSprite(this.fileName);

            ShuffleSprites(this.sprites);
            fields.GetComponent<FieldSystemJP>().InstantiateFields(this.sprites.Count);
            fields.GetComponent<FieldSystemJP>().DrawSprites(this.sprites);

            UpdateTrueFieldState();
        }

        private void SetFileName(int index)
        {
            switch (index)
            {
                case 0:
                    this.fileName = "puzzleImage";
                    break;
                default:
                    Debug.LogError("Error: Illegal Index");
                    break;
            }
        }

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

        private void ChangeSprite()
        {
            Sprite sprite1 = this.selected[0].GetComponent<Image>().sprite;
            Sprite sprite2 = this.selected[1].GetComponent<Image>().sprite;

            this.selected[0].GetComponent<Image>().sprite = sprite2;
            this.selected[1].GetComponent<Image>().sprite = sprite1;

            foreach (var field in selected)
            {
                field.GetComponent<Button>().enabled = true;
            }

            CheckPosition();
            CheckAnswer();

            this.selected.Clear();
        }

        private void CheckAnswer()
        {
            List<GameObject> fieldState = fields.GetComponent<FieldSystemJP>().GetFields();

            for (var i = 0; i < 50; i++)
            {
                if (fieldState[i].GetComponent<Image>().sprite != this.answers[i])
                {
                    return;
                }
            }

            clear.GetComponent<GameOverSystems>().AppearUIOnlyText("‚¨‚ß‚Å‚Æ‚¤!!");
        }

        private void CheckPosition()
        {
            foreach (var field in selected)
            {
                var index = field.GetComponent<FieldButtonEventsJP>().GetIndex();

                if (field.GetComponent<Image>().sprite == this.answers[index])
                {
                    field.GetComponent<Button>().enabled = false;
                }
            }
        }

        public void AddSelected(GameObject obj)
        {
            this.selected.Add(obj);

            if (this.selected.Count == 2)
            {
                ChangeSprite();
            }
        }
    }
}