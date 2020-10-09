using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 属性值
    public float move_speed = 3.0f;
    private Vector3 bulletEulerAngles;
    private float v = -1;
    private float h =  0;
    private Vector3 ban;

    // 计时器
    private float timeVal;
    private float rotateTimeVal = 2.0f;
    public bool isStopped = false;
    public bool isRare = false;
    

    // 引用
    private SpriteRenderer sr;
    public Sprite[] tankSprites; // 上 右 下 左
    public GameObject bullet;
    public GameObject explode;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        timeVal = 0.0f;
        rotateTimeVal = 0.0f;
        ban = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerManager.Instance.stopTimeVal >= 10.0f){
            isStopped = false;
        } else {
            isStopped = true;
            PlayerManager.Instance.stopTimeVal += Time.deltaTime;
        }

        if(isStopped) return;

        // 攻击的时间间隔
        if(timeVal >= 3.0f){
            Attack();
            
        } else {
            timeVal += Time.deltaTime;
        }
        
    }

    private void FixedUpdate(){
        if(!isStopped) Move(false);  
    }

    // 坦克的移动方法
    private void Move(bool flag){
        float newV=0.0f, newH=0.0f;
        if(rotateTimeVal >= 2.0f){
            while(true){
                int num = Random.Range(0,8);
                if(num >= 5){
                    newV = -1f;
                    newH = 0f;
                } else if(num == 0){
                    newV = 1f;
                    newH = 0f;
                } else if(num <= 2){
                    newV = 0f;
                    newH = -1f;
                } else if(num <= 4){
                    newV = 0f;
                    newH = 1f;
                }
                if(newV != v || newH != h || !flag){
                    v = newV;
                    h = newH;
                    break;
                }
            }
            rotateTimeVal = 0;
        } else {
            rotateTimeVal += Time.fixedDeltaTime;
        }

        

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
        Instantiate(bullet, transform.position, Quaternion.Euler(transform.eulerAngles+bulletEulerAngles));
        timeVal = 0;
    }

    // 坦克的死亡方法
    private void Die(){
        // 爆炸特效
        Instantiate(explode, transform.position, transform.rotation);
        // 得分
        PlayerManager.Instance.score++;
        // 如果为稀有坦克，生成道具
        if(isRare){
            MapCreator.Instance.GenerateTool();
        }
        // 死亡
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Enemy"){
            v *= -1;
            h *= -1;
        } else if(collision.gameObject.tag == "Barrier"){
            //ban = new Vector3(h, v, 0);
            rotateTimeVal = 3.0f;
            Move(true);
        }
    }
}
