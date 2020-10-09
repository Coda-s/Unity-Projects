using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools : MonoBehaviour
{   
    /**
     *  0. 增加生命
     *  1. 静止器
     *  2. 城墙
     *  3. 炸弹
     *  4. 升级
     *  5. 无敌
     */
    public int toolId;
    
    // 引用
    //public GameObject enemy;
    //public GameObject mapCreator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag != "Tank") return;

        switch(toolId){
            case 0 : {
                // 增加生命
                PlayerManager.Instance.heart++;
                Destroy(gameObject);
                break;
            }
            case 1 : {
                // 静止器
                PlayerManager.Instance.stopTimeVal = 0.0f;
                Destroy(gameObject);
                break;
            }
            case 2 : {
                // 城墙
                MapCreator.Instance.isMakeBarrier = true;
                MapCreator.Instance.barrierTimeVal = 0.0f;
                Destroy(gameObject);
                break;
            }
            case 3 : {
                // 炸弹
                //born.DestroyEnemys();
                GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
                for(int i=0; i<enemys.Length; i++){
                    Destroy(enemys[i]);
                }
                Destroy(gameObject);
                break;
            }
            case 4 : {
                // 升级
                Destroy(gameObject);
                break;
            }
            case 5 : {
                // 无敌
                collision.SendMessage("GetShield");
                Destroy(gameObject);
                break;
            }
            default : {
                break;
            }
        }
    }

    
}
