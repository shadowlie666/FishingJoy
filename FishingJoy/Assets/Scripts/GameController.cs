using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private static GameController _instance;
    public static GameController Instance
    {
        get
        {
            return _instance;
        }
    }
    public GameObject[] gunGos;
    private int costIndex = 0; //多少钱的子弹
    private int[] oneShootCost = { 5,10,20,40,60,80,100,200,300,400,500,600,700,800,900,1000};
    //子弹的钱数是规定死的，每四个对应一个枪
    private string[] lvName = { "新手", "入门", "钢铁", "青铜", "白银", "黄金", "白金", "钻石", "大师", "宗师" };
    public Text oneShootCostText;
    public GameObject[] BulletGos1;
    public GameObject[] BulletGos2;
    public GameObject[] BulletGos3;
    public GameObject[] BulletGos4;
    public Transform bulletHolder;
    public Text goldText;
    public Text lvText;
    public Text lvNameText;
    public Text smallCountDownText;
    public Text bigCountDownText;
    public Button bigCountDownButton;
    public Button backButton;
    public Button settingButton;
    public Slider expSlider;
    public int lv = 0;
    public int gold = 500;
    public const int bigCountDown = 240;
    public const int smallCountDown = 60;
    public float bigTimer = bigCountDown;
    public float smallTimer = smallCountDown;
    public int exp;
    public Color goldColor ;
    public GameObject lvUptips;
    public GameObject fireEffect;
    public GameObject changeEffect;
    public GameObject lvEffect;
    public GameObject goldEffect;
    public Sprite[] bgSprites;
    public Image bgImage;
    public GameObject seaWaveEffect;
    public int bgIndex = 0;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        gold = PlayerPrefs.GetInt("gold", gold);
        lv = PlayerPrefs.GetInt("lv", lv);
        exp = PlayerPrefs.GetInt("exp", exp);
        smallTimer = PlayerPrefs.GetFloat("scd", smallCountDown);
        bigTimer = PlayerPrefs.GetInt("bcd", bigCountDown);
        UpdateUI();
    }

    private void Update()
    {
        ChangeBulletCost();
        Fire();
        UpdateUI();
        ChangeBg();
    }

    void ChangeBg()
    {
        if(bgIndex != lv/20)
        {
            bgIndex = lv / 20;
            AudioManager.Instance.PlayEffectSound(AudioManager.Instance.seaWaveClip);
            Instantiate(seaWaveEffect);
            if (bgIndex>=3)
            {
                bgImage.sprite = bgSprites[3];
            }
            else
            {
                bgImage.sprite = bgSprites[bgIndex];
            }

            
        }
    }

    void UpdateUI()
    {
        bigTimer -= Time.deltaTime;
        smallTimer -= Time.deltaTime;
        if(smallTimer<=0)
        {
            smallTimer = smallCountDown;
            gold += 50;
        }
        if(bigTimer<=50 && bigCountDownButton.gameObject.activeSelf == false)
        {
            bigCountDownText.gameObject.SetActive(false);
            bigCountDownButton.gameObject.SetActive(true); 
        }
        while(exp>=1000+200*lv)
        {
            exp = exp - (1000 + 200 * lv);
            lv++;
            lvUptips.SetActive(true);
            lvUptips.transform.Find("LvUpText").GetComponent<Text>().text = lv.ToString();
            StartCoroutine(lvUptips.GetComponent<Ef_HideSelf>().HideSelf(0.6f));
            AudioManager.Instance.PlayEffectSound(AudioManager.Instance.lvUpClip);
            Instantiate(lvEffect);
        }
        goldText.text = "$" + gold;
        lvText.text = lv.ToString();
        if((lv/10)<=9)
        {
            lvNameText.text = lvName[lv / 10];
        }
        else
        {
            lvNameText.text = lvName[9];
        }
        smallCountDownText.text = (int)smallTimer / 10 + "  " + (int)smallTimer % 10;
        bigCountDownText.text = (int)bigTimer + "s";
        expSlider.value = ((float)exp) / (1000 + 200 * lv);
    }

    void Fire()
    {
        GameObject[] useBullet = null;
        int bulletIndex;
        if(Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            if(gold - oneShootCost[costIndex] >= 0)
            {
                switch (costIndex / 4)
                {
                    case 0:
                        useBullet = BulletGos1;
                        break;
                    case 1:
                        useBullet = BulletGos2;
                        break;
                    case 2:
                        useBullet = BulletGos3;
                        break;
                    case 3:
                        useBullet = BulletGos4;
                        break;
                }
                bulletIndex = (lv % 10 >= 9) ? 9 : lv % 10;
                gold -= oneShootCost[costIndex];
                AudioManager.Instance.PlayEffectSound(AudioManager.Instance.fireClip);
                Instantiate(fireEffect);
                GameObject bullet = Instantiate(useBullet[bulletIndex]);
                bullet.transform.SetParent(bulletHolder, false);
                bullet.transform.position = gunGos[costIndex / 4].transform.Find("FirePos").transform.position;
                bullet.transform.rotation = gunGos[costIndex / 4].transform.Find("FirePos").transform.rotation;
                bullet.AddComponent<Ef_AutoMove>().dir = Vector3.up;
                bullet.GetComponent<Ef_AutoMove>().speed = bullet.GetComponent<BulletAttr>().speed;
                bullet.GetComponent<BulletAttr>().damage = oneShootCost[costIndex];//子弹造成的伤害等于它所花的钱数
            }
            else
            {
                StartCoroutine("GoldNotEnough");
            }
        }
    }

    IEnumerator GoldNotEnough()
    {
        goldText.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        goldText.color = goldColor;
    }

    void ChangeBulletCost()
    {
        if(Input.GetAxis("Mouse ScrollWheel")<0)
        {
            JianButtonDown();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            JiaButtonDown();
        }
    }

    public void JiaButtonDown()
    {
        gunGos[costIndex / 4].SetActive(false);
        costIndex++;
        AudioManager.Instance.PlayEffectSound(AudioManager.Instance.changeClip);
        Instantiate(changeEffect);
        costIndex = (costIndex > oneShootCost.Length - 1) ? 0 : costIndex; //防止越界
        gunGos[costIndex / 4].SetActive(true);
        oneShootCostText.text = "$" + oneShootCost[costIndex];  //更新Ui显示文本
    }

    public void JianButtonDown()
    {
        gunGos[costIndex / 4].SetActive(false);
        costIndex--;
        AudioManager.Instance.PlayEffectSound(AudioManager.Instance.changeClip);
        costIndex = (costIndex < 0 ) ? oneShootCost.Length - 1 : costIndex; //防止越界
        gunGos[costIndex / 4].SetActive(true);
        oneShootCostText.text = "$" + oneShootCost[costIndex];  //更新Ui显示文本
    }

    public void OnBigCountDownButtonDown()
    {
        gold += 500;
        AudioManager.Instance.PlayEffectSound(AudioManager.Instance.rewardClip);
        Instantiate(goldEffect);
        bigCountDownButton.gameObject.SetActive(false);
        bigCountDownText.gameObject.SetActive(true);
        bigTimer = bigCountDown;
    }
}
