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

    private float maxHp1;
    private float nowHp1;
    public int hp1;

    private float maxHp2;
    private float nowHp2;
    public int hp2;

    int scene;//シーンを代入。ゲーム画面は1,スタート・結果画面は0。1になるとゲームスタート！

    float timrLimit;
    [SerializeField]
    private Text timeL;


    [SerializeField]
    private GameObject strat;
    [SerializeField]
    private GameObject mainGame;

    // Start is called before the first frame update
    void Start()
    {
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

        HpSlider1();
        HpSlider2();

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

        if (nowHp1 < 100)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                nowHp1 += 1f;
                hp1 += 1;
            }
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
        if (nowHp2 < 100)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                nowHp2 += 1f;
                hp2 += 1;
            }
        }
    }

    public void game()
    {
        if (scene == 0)
        {  //スタートボタンが押される前の処理
            strat.SetActive(true);//スタートボタンの配置
        }
        if (scene == 1)
        {  //スタートボタンが押されたあとの処理。ゲームスタート！
            mainGame.SetActive(true);
            timrLimit -= Time.deltaTime;
            //Debug.Log(timrLimit);
            if (timrLimit < 0)
            {    //ゲーム終了時を表す
                finish();       //finish()へ
            }
            timeL.text = "残り" + (int)timrLimit + "秒";
        }
    }

    public void finish()
    {
        if(nowHp1 > nowHp2)
        {
            Debug.Log("青の勝ち！");
        }

        if (nowHp2 > nowHp1)
        {
            Debug.Log("赤の勝ち！");
        }

        maxHp1 = 100f;
        nowHp1 = 50f;
        hp1 = 50;

        maxHp2 = 100f;
        nowHp2 = 50f;
        hp2 = 50;

        scene = 0;
        timrLimit = 20;

        mainGame.SetActive(false);
        strat.SetActive(true);
    }

    public void sButton()
    {
        strat.SetActive(false);          //スタートボタンを消す
        scene = 1;                          //変数startに1を代入。ゲームをスタートさせる
    }

}
