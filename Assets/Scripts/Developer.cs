using UnityEngine;
using UnityEditor;

public class Developer 
{
   // [MenuItem("Developer/ResetGame")]
    public static void ResetGame()
    {
        Debug.Log("Reset");
        PlayerPrefs.DeleteAll();
        for(int i=1;i<GameManager.instance.characters.Length;i++)
        {
            GameManager.instance.characters[i].isPurchased = false;
        }
    }
    //[MenuItem("Developer/Hack Coin")]
    public static void BuffCoin()
    {
        Debug.Log("HackCoin");
        PlayerPrefs.SetInt("Coins", 1000);
    }
}
