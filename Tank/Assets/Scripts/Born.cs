using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour
{
    public GameObject player;
    public GameObject[] enemyList;
    public bool createPlayer = false;

    public static List<GameObject> enemys;


    // Start is called before the first frame update
    void Start()
    {
        Invoke("BornTank", 1.0f);
        Destroy(gameObject, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BornTank(){
        if(createPlayer){
            Instantiate(player, transform.position, Quaternion.identity);
        } else {
            int num = Random.Range(0, 6);
            if(num < 4) num /= 2;
            else num -= 2;
            GameObject enemy = Instantiate(enemyList[num], transform.position, Quaternion.identity);
            if(num >= 2) enemy.GetComponent<Enemy>().isRare = true;
        }
    }
}
