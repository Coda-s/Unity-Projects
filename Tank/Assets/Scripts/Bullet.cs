using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // 属性
    public float move_speed = 10.0f;
    public bool isPlayerBullet = false;

    // 引用
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up * move_speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.tag){
            case "Tank" : {
                if(!isPlayerBullet){
                    collision.SendMessage("Die");
                    Destroy(gameObject);
                }
                break;
            }
            case "Heart" : {
                collision.SendMessage("GameOver");
                Destroy(gameObject);
                break;
            }
            case "Enemy" : {
                if(isPlayerBullet){
                    collision.SendMessage("Die");
                    Destroy(gameObject);
                }
                break;
            }
            case "Wall" : {
                Destroy(collision.gameObject);
                Destroy(gameObject);
                break;
            }
            case "Barrier" : {
                Destroy(gameObject);
                break;
            }
            default : {

                break;
            }
        }
    }

}
