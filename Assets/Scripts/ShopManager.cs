using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
    GameObject characterTmp, objTmp;
    public Transform content,transformPreview;
    [SerializeField] TextMeshProUGUI price,coin,characterInfo;
    public Button purchaseButton;
    public int indexChoosing;
    private void Awake()
    {
        if (instance == null) instance = this;
        indexChoosing = PlayerPrefs.GetInt("CharacterIdSelect");
        characterTmp = content.GetChild(0).gameObject;
        foreach (Character character in GameManager.instance.characters)
        {
            objTmp = Instantiate(characterTmp, content);
            objTmp.GetComponent<CharacterDisplay>().character = character;
        }
        Destroy(characterTmp);
        Instantiate(GameManager.instance.characters[indexChoosing].prefab, transformPreview);
    }
    void Update()
    {
        if (GameManager.instance.characters[indexChoosing].isPurchased == true)
        {
            price.text = "Purchased";
        }
        else price.text = GameManager.instance.characters[indexChoosing].price.ToString();
        if (GameManager.instance.characters[indexChoosing].isPurchased == true || PlayerPrefs.GetInt("Coins") < GameManager.instance.characters[indexChoosing].price)
        {
            purchaseButton.interactable = false;
        }
        else purchaseButton.interactable = true;
        characterInfo.text = GameManager.instance.characters[indexChoosing].name.ToString();
        coin.text = PlayerPrefs.GetInt("Coins").ToString();
    }
    public void purchase()
    {
        if (GameManager.instance.characters[indexChoosing].isPurchased == false && PlayerPrefs.GetInt("Coins") >= GameManager.instance.characters[indexChoosing].price)
        {
            Debug.Log("mua");
            GameManager.instance.characters[indexChoosing].isPurchased = true;
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - GameManager.instance.characters[indexChoosing].price);
            PlayerPrefs.SetInt("CharacterIdSelect", indexChoosing);
        }
    }
    public void spawnPreviewCharacter()
    {
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        Instantiate(GameManager.instance.characters[indexChoosing].prefab,transformPreview);
    }
}
