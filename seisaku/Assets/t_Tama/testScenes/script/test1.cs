using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test1 : MonoBehaviour
{
    public Slider hpSlider1;
    public Slider hpSlider2;
    public Text hpText1;
    public Text hpText2;

    private int divideHp;

    private float maxHp1;
    private float nowHp1;
    public int hp1;

    private float maxHp2;
    private float nowHp2;
    public int hp2;

    int scene;//シーンを代入。ゲーム画面は1,スタートは0。1になるとゲームスタート！

    float timrLimit;
    [SerializeField]
    private Text timeL;


    [SerializeField]
    private GameObject strat;
    [SerializeField]
    private GameObject mainGame;
    [SerializeField]
    private GameObject result;

    float waitTime;

    float limit;
    float difficultyLimit;


    [SerializeField]
    private GameObject[] stageC;

    [SerializeField]
    private Text resultText;

    // Start is called before the first frame update
    void Start()
    {

        strat.SetActive(false);
        mainGame.SetActive(false);
        result.SetActive(false);
        maxHp1 = 100f;
        nowHp1 = 50f;
        hp1 = 50;

        maxHp2 = 100f;
        nowHp2 = 50f;
        hp2 = 50;


        //スライダーの最大値の設定
        hpSlider1.maxValue = maxHp1;

        //スライダーの最大値の設定
        hpSlider2.maxValue = maxHp2;

        scene = 0;
        timrLimit = 20;
        Difficulty(0);
    }

    // Update is called once per frame
    void Update()
    {
        //スライダーの現在値の設定
        hpSlider1.value = nowHp1;
        //スライダーの現在値の設定
        hpSlider2.value = nowHp2;

        hpText1.text = "" + hp1;
        hpText2.text = "" + hp2;

        game();
    }

    void HpSlider1()
    {
        if (nowHp2 > 0)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                nowHp2 -= 1f;
                hp2 -= 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            nowHp1 += 1f;
            hp1 += 1;
        }
    }

    void HpSlider2()
    {
        if (nowHp1 > 0)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                nowHp1 -= 1f;
                hp1 -= 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            nowHp2 += 1f;
            hp2 += 1;
        }
    }

    IEnumerator LevelDifficulty()
    {
        if (nowHp1 > 0)
        {
            nowHp1 -= 1f;
            hp1 -= 1;
        }
        nowHp2 += 1f;
        hp2 += 1;

        Debug.Log(nowHp1 + ":" + nowHp2 + ":" + hp1 + ":" + hp2);
        yield return new WaitForSeconds(waitTime);
    }

    public void game()
    {
        if (scene == 0)
        {  //スタートボタンが押される前の処理
            strat.SetActive(true);//スタートボタンの配置
        }
        if (scene == 1)
        {  //スタートボタンが押されたあとの処理。ゲームスタート！

            HpSlider1();
            if (divideHp == 1)
            {
                HpSlider2();
            }

            limit -= Time.deltaTime;
            if (divideHp == 0)
            {
                if (limit < 0)
                {
                    waitTime = 10.0f;
                    StartCoroutine(LevelDifficulty());
                    limit = difficultyLimit;
                }
            }
            mainGame.SetActive(true);
            timrLimit -= Time.deltaTime;
            //Debug.Log(timrLimit);
            if (timrLimit < 0)
            {    //ゲーム終了時を表す
                result.SetActive(true);
                finish();       //finish()へ
            }
            timeL.text = "残り" + (int)timrLimit + "秒";
        }
    }

    public void finish()
    {
        if(divideHp == 0)
        {
            if (nowHp1 > nowHp2)
            {
                resultText.text = "あなたの勝ち！";
                Debug.Log("あなたの勝ち！");
            }

            if (nowHp2 > nowHp1)
            {
                resultText.text = "CPUの勝ち！";
                Debug.Log("CPUの勝ち！");
            }
        }

        if (divideHp == 1)
        {
            if (nowHp1 > nowHp2)
            {
                resultText.text = "青の勝ち！";
                Debug.Log("青の勝ち！");
            }

            if (nowHp2 > nowHp1)
            {
                resultText.text = "赤の勝ち！";
                Debug.Log("赤の勝ち！");
            }
        }


    }
    public void Continue()
    {
        maxHp1 = 100f;
        nowHp1 = 50f;
        hp1 = 50;

        maxHp2 = 100f;
        nowHp2 = 50f;
        hp2 = 50;

        scene = 0;
        timrLimit = 20;
        mainGame.SetActive(false);
        result.SetActive(false);
        strat.SetActive(true);
    }

    public void sButton()
    {
        strat.SetActive(false);          //スタートボタンを消す
        scene = 1;                          //変数startに1を代入。ゲームをスタートさせる
    }

    public void OnePersonStratButton()
    {
        strat.SetActive(false);          //スタートボタンを消す
        scene = 1;                          //変数startに1を代入。ゲームをスタートさせる
        divideHp = 0;
    }
    public void TwoPersonStratButton()
    {
        strat.SetActive(false);          //スタートボタンを消す
        scene = 1;                          //変数startに1を代入。ゲームをスタートさせる
        divideHp = 1;
    }

    public void Difficulty(int x)
    {
        switch (x)
        {
            case 0:
                difficultyLimit = 0.4f;
                break;
            case 1:
                difficultyLimit = 0.2f;
                break;
            case 2:
                difficultyLimit = 0.01f;
                break;
        }
        //選択時のカラー変更
        stageC[0].GetComponent<Image>().color = Color.white;
        stageC[1].GetComponent<Image>().color = Color.white;
        stageC[2].GetComponent<Image>().color = Color.white;
        switch (x)
        {
            case 0:
                stageC[0].GetComponent<Image>().color = Color.red;
                break;
            case 1:
                stageC[1].GetComponent<Image>().color = Color.red;
                break;
            case 2:
                stageC[2].GetComponent<Image>().color = Color.red;
                break;
        }
    }
}
