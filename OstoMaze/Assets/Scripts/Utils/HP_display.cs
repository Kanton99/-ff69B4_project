using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_display : MonoBehaviour
{
    public Image[] sprites;
    public MainController player;

    public GameObject origin;

    const int max_hp = 10;
    Image[] hearts;
    private void Start()
    {
        player = GameObject.Find("Bran").GetComponent<MainController>();
        hearts = new Image[5];
        for(int i = 0;i< max_hp/2; i ++)
        {
            Image heart = Instantiate(sprites[0],Vector3.zero+new Vector3((105*i),0,0),Quaternion.identity);
            heart.transform.SetParent(origin.transform, false);
            hearts[i] = heart;
        }
    }

    private void Update()
    {
        UpdateSprite(player.hp);
    }

    public void UpdateSprite(int hp)
    {
        //assunto che l'hp massima è 10;
        int i;
        for(i = 0; i < hp; i++)
        {
            hearts[i%2==0 ? (i/2):((i-1)/2)].GetComponent<Image>().sprite = sprites[(i+1)%2].sprite;
        }
        for(i = i+1; i < max_hp; i += 2)
        {
            hearts[i / 2].GetComponent<Image>().sprite = sprites[2].sprite;
        }
    }
}
