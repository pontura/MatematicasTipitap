using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ConfirmPopup : MonoBehaviour {

    public GameObject panel;

    public Text title;

    public Text field1;
    public Text field2;
    public Text field3;

    public int result;
    public List<string> nums;
    private System.Action functionListener;

    void Start () {
        panel.SetActive(false);
    }
    public void Open(System.Action functionListener)
    {
        this.functionListener = functionListener;
        nums.Clear();
        int num1 = Random.Range(3, 8);
        int num2 = Random.Range(3, 8);

        title.text = num1.ToString() +  "x" + num2.ToString();

        result = num1 * num2;

        nums.Add ( result.ToString() );
        nums.Add ( (result - Random.Range(2,12)).ToString() );
        nums.Add ( (result + Random.Range(2, 12 )).ToString());

        Utils.ShuffleListTexts(nums);

        field1.text = nums[0];
        field2.text = nums[1];
        field3.text = nums[2];

        panel.SetActive(true);
    }
    public void Close()
    {
        panel.SetActive(false);
    }
    public void Clicked(int id)
    {
        string toCheckResult = "";
        switch(id)
        {
            case 1: toCheckResult = field1.text; break;
            case 2: toCheckResult = field2.text; break;
            case 3: toCheckResult = field3.text; break;
        }
        if (result.ToString() == toCheckResult)
            Go();
        Close();
    }
    void Go()
    {
        print("OK");
        functionListener();
    }
}
