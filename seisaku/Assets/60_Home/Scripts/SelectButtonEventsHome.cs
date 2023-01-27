using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SceneNames;
using PublicUI;

namespace Home
{
    public class SelectButtonEventsHome : MonoBehaviour
    {
        public Text name;
        private string gameName;
        private int buttonIndex;
        [SerializeField]
        Image uiIcon;

        private void Start()
        {
            GameSelect();
            name.text = gameName;
        }
        public async void OnClickSelect()
        {
            GameSelect();
            var fade = GameObject.Find("FadeUI");
            fade.GetComponent<FadeSystems>().FadeOut();
            await Task.Delay(3000);
            SceneManager.LoadScene("DescriptionScene");
        }

        public void SetButtonIndex(int value)
        {
            this.buttonIndex = value;
            uiIcon.sprite = Resources.Load<Sprite>((Mathf.Abs(value) % 30 + 1).ToString("icon000"));
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void GameSelect()
        {

            switch (buttonIndex)
            {
                case 0:
                    SceneData.sceneNames = SceneName.Neurasthenia;
                    gameName = "真剣衰弱";                 
                    break;
                case 1:
                    SceneData.sceneNames = SceneName.HitAndBlow;
                    gameName = "ヒット＆ブロー";
                    break;
                case 2:
                    SceneData.sceneNames = SceneName.GetBlock;
                    gameName = "ブロック取り";
                    break;
                case 3:
                    SceneData.sceneNames = SceneName.JigsawPuzzle;
                    gameName = "ジグソーパズル";
                    break;
                case 4:
                    SceneData.sceneNames = SceneName.Flash;
                    gameName = "フラッシュ";
                    break;
                case 5:
                    SceneData.sceneNames = SceneName.QuicklyImage;
                    gameName = "瞬間画像当て";
                    break;
                case 6:
                    SceneData.sceneNames = SceneName.Launchingbusiness;
                    gameName = "旗揚げ";
                    break;
                case 7:
                    SceneData.sceneNames = SceneName.Reversible;
                    gameName = "リバーシ";
                    break;
                case 8:
                    SceneData.sceneNames = SceneName.RepeatedlyHitting;
                    gameName = "連打";
                    break;
                case 9:
                    SceneData.sceneNames = SceneName.MusicGame;
                    gameName = "リズム";
                    break;
                default:
                    Debug.LogError("IllegalValue");
                    break;
            }
        }
    }
}