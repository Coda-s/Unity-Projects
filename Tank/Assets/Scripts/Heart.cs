using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{

    private SpriteRenderer sr; 

    // 引用
    public Sprite BrokenSprite;
    public GameObject Explode;
    public AudioClip dieAudio;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GameOver(){
        sr.sprite = BrokenSprite;
        Instantiate(Explode, transform.position, transform.rotation);
        PlayerManager.Instance.isDefeated = true;
        AudioSource.PlayClipAtPoint(dieAudio, transform.position);
    }

}
