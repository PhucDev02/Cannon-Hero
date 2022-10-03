using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="Character",menuName ="Character")]
[SerializeField]
public class Character : ScriptableObject
{
    public new string name;
    public int index;
    public GameObject prefab;
    public bool isPurchased;
    public int price;
    public Sprite sprite;
}
