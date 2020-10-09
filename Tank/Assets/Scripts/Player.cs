using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 属性值
    public float move_speed = 6.0f;
    private Vector3 bulletEulerAngles;
    private float timeVal;
    private float defendTimeVal = 3.0f;
    private bool isDefended = true;
    

    // 引用
    private SpriteRenderer sr;
    public Sprite[] tankSprites; // 上 右 下 左
    public GameObject bullet;
    public GameObject explode;
    public GameObject defendEffect;
    
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        timeVal = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // 是否无敌
        if(isDefended){
            defendEffect.SetActive(true);
            defendTimeVal -= Time.deltaTime;
            if(defendTimeVal <= 0) {
                isDefended = false;
                defendEffect.SetActive(false);
            }
        }

        if(PlayerManager.Instance.isDefeated == false){
            // 攻击的cd
            if(timeVal >= 0.4f){
                Attack();
                
            } else {
                timeVal += Time.deltaTime;
            }
        }
    }

    private void FixedUpdate(){
        if(PlayerManager.Instance.isDefeated == false) Move();  
    }

    // 坦克的移动方法
    private void Move(){
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");


        transform.Translate(Vector3.right * h * move_speed * Time.fixedDeltaTime, Space.World);
        if(h < 0){
            sr.sprite = tankSprites[3];
            bulletEulerAngles = new Vector3(0, 0, +90);
        }else if(h > 0){
            sr.sprite = tankSprites[1];
            bulletEulerAngles = new Vector3(0, 0, -90);
        }
        if(h != 0) return;

        transform.Translate(Vector3.up * v * move_speed * Time.fixedDeltaTime, Space.World);
        if(v < 0){
            sr.sprite = tankSprites[2];
            bulletEulerAngles = new Vector3(0, 0, 180);
        }else if(v > 0){
            sr.sprite = tankSprites[0];
            bulletEulerAngles = new Vector3(0, 0, 0);
        }
    }

    // 坦克的攻击方法
    private void Attack(){
        if(Input.GetKeyDown(KeyCode.Space)){
            // 生成子弹对象
            Instantiate(bullet, transform.position, Quaternion.Euler(transform.eulerAngles+bulletEulerAngles));
            timeVal = 0;
        }
    }

    // 坦克的死亡方法
    private void Die(){
        // 无敌
        if(isDefended) return;

        // 结束or复活
        PlayerManager.Instance.isDead = true;
        // 爆炸特效
        Instantiate(explode, transform.position, transform.rotation);
        // 死亡
        Destroy(gameObject);
    }

    // 坦克获得无敌
    private void GetShield(){
        isDefended = true;
        defendTimeVal = 3.0f;
    }
}
