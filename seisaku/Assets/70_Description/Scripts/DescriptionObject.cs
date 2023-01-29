using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SoundManager;
using PublicUI;

public class DescriptionObject : MonoBehaviour
{
    string sceneName;
    public Text description;
    [SerializeField]
    private GameObject sound;

    // Start is called before the first frame update
    void Start()
    {

        switch (SceneData.sceneNames.ToString())
        {
            case "Neurasthenia":
                sceneName = "Maingame";
                Neurasthenia();
                break;
            case "HitAndBlow":
                sceneName = "MaingameHB";
                HitAndBlow();
                break;
            case "GetBlock":
                sceneName = "MaingameGB";
                GetBlock();
                break;
            case "JigsawPuzzle":
                sceneName = "MaingameJP";
                JigsawPuzzle();
                break;
            case "QuicklyImage":
                sceneName = "MaingameQI";
                QuicklyImage();
                break;
            case "Flash":
                sceneName = "MaingameFL";
                Flash();
                break;
            case "Launchingbusiness":
                sceneName = "Launching_business";
                Launchingbusiness();
                break;
            case "Reversible":
                sceneName = "Reversible";
                Reversible();
                break;
            case "RepeatedlyHitting":
                sceneName = "RepeatedlyHitting";
                RepeatedlyHitting();
                break;
            case "MusicGame":
                sceneName = "MusicGame";
                MusicGame();
                break;
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
    public async void OnClickStartButton()
    {
        sound.GetComponent<SoundSystems>().PlaySE(0);
        var fade = GameObject.Find("FadeUI");
        fade.GetComponent<FadeSystems>().FadeOut();
        await Task.Delay(3000);
        SceneManager.LoadScene(sceneName);
    }

    public void Neurasthenia()
    {
        description.text = "盤面のカードを2枚選んで、ペアを揃えていこう！\n\n外れるとライフが減るので、0になるまでにどれだけ沢山ペアを揃えられるか挑戦だ！ ";
    }

    public void HitAndBlow()
    {
        description.text = "隠されている4枚の画像を正しい順番に並べよう！\n\n4枚の画像は同じものが2枚以上使われることもあるから注意してね！\n \n選んだ画像と順番が正しかったらヒット！ \n順番が違うならブローになるよ！ \n8回以内に、正しい画像と順番を見つけよう！ ";
    }

    public void GetBlock()
    {
        description.text = "二人対戦のターンバトルゲームだ！ \n\n隅にあるブロックを選択して取っていこう！ \n同じ絵柄のブロックはまとめてキャッチできるぞ！\n \n相手より沢山のブロックを取るか、\n1つのブロックを全部集めたら君の勝利だ！ ";
    }

    public void JigsawPuzzle()
    {
        description.text = "2つのピースを選んでどんどん交換していこう！ \n\nどれだけ少ない手順で絵を完成できるかな？ ";
    }

    public void QuicklyImage()
    {
        description.text = "表示された画像と同じものを選択肢から選ぼう！ \n\n時間内に選べなかったり、間違えたら終了！\nn \n素早く当てれば高得点だ！ ";
    }

    public void Flash()
    {
        description.text = "最初に出てきた画像を覚えて選ぼう！ \n\n順番は気にせず、どれが出てきたかだけ覚えるのが大事だよ！\n \nたくさん当てて、目指せハイスコア！ ";
    }

    public void Launchingbusiness()
    {
        description.text = "指示に従って旗を上げ下げしよう！ \n\nボタンを押して旗を動かすことができるよ！\n \n正しかったらスコアがどんどん増えていくぞ！ ";
    }

    public void Reversible()
    {
        description.text = "オセロとも言うお馴染みのゲーム！ \n\nコマを挟んでどんどん自分の色を増やしていこう！\n \n2〜4人の大人数で遊ぶこともできるぞ！ ";
    }

    public void RepeatedlyHitting()
    {
        description.text = "出てきたキーを素早く押して目指せハイスコア！ \n\n出てくるキーはAからZの24種類！\n \nモタモタしてると切り替わっちゃうから注意！ ";
    }

    public void MusicGame()
    {
        description.text = "リズムに合わせて5つのレーンの音符を叩こう！\n \n出てくる音符は「D,F,G,H,J」の5種類！\n \nタイミング良く押したらスコアが増えていくよ！ ";
    }
}