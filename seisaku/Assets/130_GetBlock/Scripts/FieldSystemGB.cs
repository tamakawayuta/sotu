using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GetBlock
{
    public class FieldSystemGB : MonoBehaviour
    {
        [SerializeField]
        private Sprite[] images;

        private List<GameObject> fields = new List<GameObject>();
        private List<Sprite> canSelectSprites = new List<Sprite>();
        private List<int> selectedIndex = new List<int>();
        private Queue<Sprite> shuffledImages = new Queue<Sprite>();

        private bool[] didSelect = {
            false, false, false, false, false,
            false, false, false, false, false,
            false, false, false, false, false,
            false, false, false, false, false,
            false, false, false, false, false
        };

        private void Awake()
        {
            for (var i = 0; i < this.gameObject.transform.childCount; i++)
            {
                var obj = this.gameObject.transform.GetChild(i).gameObject;
                obj.GetComponent<Image>().color = Color.gray;
                obj.GetComponent<Button>().enabled = false;
                fields.Add(obj);
            }

            this.gameObject.transform.GetChild(0).GetComponent<Image>().color = Color.white;
            this.gameObject.transform.GetChild(4).GetComponent<Image>().color = Color.white;
            this.gameObject.transform.GetChild(20).GetComponent<Image>().color = Color.white;
            this.gameObject.transform.GetChild(24).GetComponent<Image>().color = Color.white;

            this.gameObject.transform.GetChild(0).GetComponent<Button>().enabled = true;
            this.gameObject.transform.GetChild(4).GetComponent<Button>().enabled = true;
            this.gameObject.transform.GetChild(20).GetComponent<Button>().enabled = true;
            this.gameObject.transform.GetChild(24).GetComponent<Button>().enabled = true;

            Sprite[] tmpSprites = new Sprite[25];

            for (var i = 0; i < 5; i++)
            {
                tmpSprites[i] = images[0];
                tmpSprites[i + 5] = images[1];
                tmpSprites[i + 10] = images[2];
                tmpSprites[i + 15] = images[3];
                tmpSprites[i + 20] = images[4];
            }

            ShuffleImages(tmpSprites);

            for (var i = 0; i < tmpSprites.Length; i++)
            {
                shuffledImages.Enqueue(tmpSprites[i]);
            }

            foreach(var field in fields)
            {
                field.GetComponent<Image>().sprite = shuffledImages.Dequeue();
            }

            canSelectSprites.Add(this.gameObject.transform.GetChild(0).GetComponent<Image>().sprite);
            canSelectSprites.Add(this.gameObject.transform.GetChild(4).GetComponent<Image>().sprite);
            canSelectSprites.Add(this.gameObject.transform.GetChild(20).GetComponent<Image>().sprite);
            canSelectSprites.Add(this.gameObject.transform.GetChild(24).GetComponent<Image>().sprite);
        }

        private void ShuffleImages(Sprite[] images)
        {
            for (var i = images.Length - 1; i > 0; --i)
            {
                var j = Random.Range(0, i + 1);
                var tmp = images[i];
                images[i] = images[j];
                images[j] = tmp;
            }
        }

        public void RemoveCanSelectSprites(Sprite sprite)
        {
            canSelectSprites.Remove(sprite);
        }

        public bool CanSelectOnce(Sprite sprite)
        {
            return this.canSelectSprites.Contains(sprite);
        }

        public void AddSelectedIndex(int index)
        {
            this.selectedIndex.Add(index);
            this.didSelect[index] = true;
        }

        public void UpdateGameState()
        {
            foreach (var index in selectedIndex)
            {
                UpdateGameState(index);
            }

            selectedIndex.Clear();
        }

        private  void UpdateGameState(int index)
        {
            switch (index)
            {
                case 0:
                    if (!didSelect[1])
                    {
                        this.gameObject.transform.GetChild(1).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(1).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[5])
                    {
                        this.gameObject.transform.GetChild(5).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(5).GetComponent<Button>().enabled = true;
                    }
                    break;
                case 1:
                    if (!didSelect[2])
                    {
                        this.gameObject.transform.GetChild(2).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(2).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[6] && (didSelect[5] || didSelect[7]))
                    {
                        this.gameObject.transform.GetChild(6).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(6).GetComponent<Button>().enabled = true;
                    }
                    break;
                case 2:
                    if (!didSelect[1])
                    {
                        this.gameObject.transform.GetChild(1).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(1).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[3])
                    {
                        this.gameObject.transform.GetChild(3).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(3).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[7] && (didSelect[6] || didSelect[8]))
                    {
                        this.gameObject.transform.GetChild(7).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(7).GetComponent<Button>().enabled = true;
                    }
                    break;
                case 3:
                    if (!didSelect[2])
                    {
                        this.gameObject.transform.GetChild(2).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(2).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[8] && (didSelect[9] || didSelect[7]))
                    {
                        this.gameObject.transform.GetChild(8).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(8).GetComponent<Button>().enabled = true;
                    }
                    break;
                case 4:
                    if (!didSelect[3])
                    {
                        this.gameObject.transform.GetChild(3).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(3).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[9])
                    {
                        this.gameObject.transform.GetChild(9).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(9).GetComponent<Button>().enabled = true;
                    }
                    break;
                case 5:
                    if (!didSelect[10])
                    {
                        this.gameObject.transform.GetChild(10).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(10).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[6] && (didSelect[1] || didSelect[11]))
                    {
                        this.gameObject.transform.GetChild(6).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(6).GetComponent<Button>().enabled = true;
                    }
                    break;
                case 6:
                    if (!didSelect[1] && didSelect[2])
                    {
                        this.gameObject.transform.GetChild(1).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(1).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[5] && didSelect[10])
                    {
                        this.gameObject.transform.GetChild(5).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(5).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[7] && (didSelect[2] || didSelect[12]))
                    {
                        this.gameObject.transform.GetChild(7).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(7).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[11] && (didSelect[10] || didSelect[12]))
                    {
                        this.gameObject.transform.GetChild(11).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(11).GetComponent<Button>().enabled = true;
                    }
                    break;
                case 7:
                    if (!didSelect[2] && (didSelect[1] || didSelect[3]))
                    {
                        this.gameObject.transform.GetChild(2).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(2).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[6] && (didSelect[1] || didSelect[11]))
                    {
                        this.gameObject.transform.GetChild(6).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(6).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[8] && (didSelect[3] || didSelect[13]))
                    {
                        this.gameObject.transform.GetChild(8).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(8).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[12] && (didSelect[10] || didSelect[11]))
                    {
                        this.gameObject.transform.GetChild(12).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(12).GetComponent<Button>().enabled = true;
                    }
                    break;
                case 8:
                    if (!didSelect[3] && didSelect[2])
                    {
                        this.gameObject.transform.GetChild(3).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(3).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[9] && didSelect[14])
                    {
                        this.gameObject.transform.GetChild(9).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(9).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[7] && (didSelect[2] || didSelect[12]))
                    {
                        this.gameObject.transform.GetChild(7).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(7).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[13] && (didSelect[14] || didSelect[12]))
                    {
                        this.gameObject.transform.GetChild(13).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(13).GetComponent<Button>().enabled = true;
                    }
                    break;
                case 9:
                    if (!didSelect[14])
                    {
                        this.gameObject.transform.GetChild(14).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(14).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[8] && (didSelect[3] || didSelect[13]))
                    {
                        this.gameObject.transform.GetChild(8).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(8).GetComponent<Button>().enabled = true;
                    }
                    break;
                case 10:
                    if (!didSelect[5])
                    {
                        this.gameObject.transform.GetChild(5).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(5).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[15])
                    {
                        this.gameObject.transform.GetChild(15).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(15).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[11] && (didSelect[6] || didSelect[16]))
                    {
                        this.gameObject.transform.GetChild(11).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(11).GetComponent<Button>().enabled = true;
                    }
                    break;
                case 11:
                    if (!didSelect[6] && (didSelect[5] || didSelect[7]))
                    {
                        this.gameObject.transform.GetChild(6).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(6).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[10] && (didSelect[5] || didSelect[15]))
                    {
                        this.gameObject.transform.GetChild(10).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(10).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[16] && (didSelect[15] || didSelect[17]))
                    {
                        this.gameObject.transform.GetChild(16).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(16).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[12] && (didSelect[7] || didSelect[17]))
                    {
                        this.gameObject.transform.GetChild(12).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(12).GetComponent<Button>().enabled = true;
                    }
                    break;
                case 12:
                    if (!didSelect[11] && (didSelect[6] || didSelect[16]))
                    {
                        this.gameObject.transform.GetChild(11).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(11).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[13] && (didSelect[8] || didSelect[18]))
                    {
                        this.gameObject.transform.GetChild(13).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(13).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[7] && (didSelect[6] || didSelect[8]))
                    {
                        this.gameObject.transform.GetChild(7).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(7).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[17] && (didSelect[16] || didSelect[18]))
                    {
                        this.gameObject.transform.GetChild(17).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(17).GetComponent<Button>().enabled = true;
                    }
                    break;
                case 13:
                    if (!didSelect[14] && (didSelect[9] || didSelect[19]))
                    {
                        this.gameObject.transform.GetChild(14).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(14).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[8] && (didSelect[9] || didSelect[7]))
                    {
                        this.gameObject.transform.GetChild(8).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(8).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[18] && (didSelect[19] || didSelect[17]))
                    {
                        this.gameObject.transform.GetChild(18).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(18).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[12] && (didSelect[7] || didSelect[17]))
                    {
                        this.gameObject.transform.GetChild(12).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(12).GetComponent<Button>().enabled = true;
                    }
                    break;
                case 14:
                    if (!didSelect[9])
                    {
                        this.gameObject.transform.GetChild(9).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(9).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[19])
                    {
                        this.gameObject.transform.GetChild(19).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(19).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[13] && (didSelect[8] || didSelect[18]))
                    {
                        this.gameObject.transform.GetChild(13).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(13).GetComponent<Button>().enabled = true;
                    }
                    break;
                case 15:
                    if (!didSelect[10])
                    {
                        this.gameObject.transform.GetChild(10).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(10).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[16] && (didSelect[21] || didSelect[11]))
                    {
                        this.gameObject.transform.GetChild(16).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(16).GetComponent<Button>().enabled = true;
                    }
                    break;
                case 16:
                    if (!didSelect[15] && didSelect[10])
                    {
                        this.gameObject.transform.GetChild(15).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(15).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[21] && didSelect[22])
                    {
                        this.gameObject.transform.GetChild(21).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(21).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[11] && (didSelect[10] || didSelect[12]))
                    {
                        this.gameObject.transform.GetChild(11).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(11).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[17] && (didSelect[12] || didSelect[22]))
                    {
                        this.gameObject.transform.GetChild(17).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(17).GetComponent<Button>().enabled = true;
                    }
                    break;
                case 17:
                    if (!didSelect[22] && (didSelect[21] || didSelect[23]))
                    {
                        this.gameObject.transform.GetChild(22).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(22).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[16] && (didSelect[21] || didSelect[11]))
                    {
                        this.gameObject.transform.GetChild(16).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(16).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[18] && (didSelect[23] || didSelect[13]))
                    {
                        this.gameObject.transform.GetChild(18).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(18).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[12] && (didSelect[10] || didSelect[11]))
                    {
                        this.gameObject.transform.GetChild(12).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(12).GetComponent<Button>().enabled = true;
                    }
                    break;
                case 18:
                    if (!didSelect[19] && didSelect[14])
                    {
                        this.gameObject.transform.GetChild(19).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(19).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[23] && didSelect[22])
                    {
                        this.gameObject.transform.GetChild(23).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(23).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[13] && (didSelect[14] || didSelect[12]))
                    {
                        this.gameObject.transform.GetChild(13).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(13).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[17] && (didSelect[12] || didSelect[22]))
                    {
                        this.gameObject.transform.GetChild(17).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(17).GetComponent<Button>().enabled = true;
                    }
                    break;
                case 19:
                    if (!didSelect[14])
                    {
                        this.gameObject.transform.GetChild(14).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(14).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[18] && (didSelect[23] || didSelect[13]))
                    {
                        this.gameObject.transform.GetChild(18).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(18).GetComponent<Button>().enabled = true;
                    }
                    break;
                case 20:
                    if (!didSelect[15])
                    {
                        this.gameObject.transform.GetChild(15).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(15).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[21])
                    {
                        this.gameObject.transform.GetChild(21).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(21).GetComponent<Button>().enabled = true;
                    }
                    break;
                case 21:
                    if (!didSelect[22])
                    {
                        this.gameObject.transform.GetChild(22).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(22).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[16] && (didSelect[15] || didSelect[17]))
                    {
                        this.gameObject.transform.GetChild(16).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(16).GetComponent<Button>().enabled = true;
                    }
                    break;
                case 22:
                    if (!didSelect[21])
                    {
                        this.gameObject.transform.GetChild(21).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(21).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[23])
                    {
                        this.gameObject.transform.GetChild(23).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(23).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[17] && (didSelect[16] || didSelect[18]))
                    {
                        this.gameObject.transform.GetChild(17).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(17).GetComponent<Button>().enabled = true;
                    }
                    break;
                case 23:
                    if (!didSelect[22])
                    {
                        this.gameObject.transform.GetChild(22).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(22).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[18] && (didSelect[19] || didSelect[17]))
                    {
                        this.gameObject.transform.GetChild(18).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(18).GetComponent<Button>().enabled = true;
                    }
                    break;
                case 24:
                    if (!didSelect[19])
                    {
                        this.gameObject.transform.GetChild(19).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(19).GetComponent<Button>().enabled = true;
                    }
                    if (!didSelect[23])
                    {
                        this.gameObject.transform.GetChild(23).GetComponent<Image>().color = Color.white;
                        this.gameObject.transform.GetChild(23).GetComponent<Button>().enabled = true;
                    }
                    break;
                default:
                    break;
            }

            UpdateCanSelectSprites();
        }

        private void UpdateCanSelectSprites()
        {
            canSelectSprites.Clear();
            foreach (var field in fields)
            {
                if (field.GetComponent<Button>().enabled)
                {
                    canSelectSprites.Add(field.GetComponent<Image>().sprite);
                }
            }
        }
    }
}