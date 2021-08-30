using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI carrotText;
    [SerializeField] TextMeshProUGUI inGameCarrotText;

    public int carrotAmount;
    // Start is called before the first frame update
    void Start()
    {
        carrotAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        carrotText.text = (carrotAmount * 100).ToString();
        inGameCarrotText.text = (carrotAmount * 100).ToString();
    }

    public void IncreaseCarrotAmount()
    {
        carrotAmount++;
    }
}
