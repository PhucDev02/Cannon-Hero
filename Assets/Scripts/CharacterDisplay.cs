using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    public Character character;
    public GameObject button;
    void Start()
    {
        button.GetComponent<Image>().sprite = character.sprite;
    }
    private void Update()
    {
        if (ShopManager.instance.indexChoosing == character.index)
        {
             button.GetComponent<Image>().color = new Color(255 , 255, 255);
        }
        else button.GetComponent<Image>().color = new Color(0, 0, 0);
    }
    public void setIdChoose()
    {
        ShopManager.instance.indexChoosing = character.index;
        ShopManager.instance.spawnPreviewCharacter();
        if(GameManager.instance.characters[character.index].isPurchased==true)
        {
            PlayerPrefs.SetInt("CharacterIdSelect", character.index);
        }
    }
}
