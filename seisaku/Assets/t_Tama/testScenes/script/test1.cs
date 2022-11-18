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

    int scene;//�V�[�������B�Q�[����ʂ�1,�X�^�[�g�E���ʉ�ʂ�0�B1�ɂȂ�ƃQ�[���X�^�[�g�I

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


        //�X���C�_�[�̍ő�l�̐ݒ�
        hpSlider1.maxValue = maxHp1;

        //�X���C�_�[�̍ő�l�̐ݒ�
        hpSlider2.maxValue = maxHp2;

        scene = 0;
        timrLimit = 20;
    }

    // Update is called once per frame
    void Update()
    {
        //�X���C�_�[�̌��ݒl�̐ݒ�
        hpSlider1.value = nowHp1;
        //�X���C�_�[�̌��ݒl�̐ݒ�
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
        {  //�X�^�[�g�{�^�����������O�̏���
            strat.SetActive(true);//�X�^�[�g�{�^���̔z�u
        }
        if (scene == 1)
        {  //�X�^�[�g�{�^���������ꂽ���Ƃ̏����B�Q�[���X�^�[�g�I
            mainGame.SetActive(true);
            timrLimit -= Time.deltaTime;
            //Debug.Log(timrLimit);
            if (timrLimit < 0)
            {    //�Q�[���I������\��
                finish();       //finish()��
            }
            timeL.text = "�c��" + (int)timrLimit + "�b";
        }
    }

    public void finish()
    {
        if(nowHp1 > nowHp2)
        {
            Debug.Log("�̏����I");
        }

        if (nowHp2 > nowHp1)
        {
            Debug.Log("�Ԃ̏����I");
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
        strat.SetActive(false);          //�X�^�[�g�{�^��������
        scene = 1;                          //�ϐ�start��1�����B�Q�[�����X�^�[�g������
    }

}
