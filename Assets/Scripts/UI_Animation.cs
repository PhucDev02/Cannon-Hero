using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class UI_Animation : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        transform.localScale = new Vector3(0.2f, 0.2f, 0);
    }
    private void OnEnable()
    {
        transform.DOScale(1f, 0.5f).SetEase(Ease.InOutSine);
    }
    private void Start()
    {
        transform.DOScale(1f, 0.5f).SetEase(Ease.InOutSine);
    }
}
