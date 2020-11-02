using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DotsAnimation : MonoBehaviour
{
    public float Interval;
    public int DotAmount;

    private Text _myText;
    private float _intervalCounter;
    private int _dotCounter;

    void Start()
    {
        _myText = GetComponent<Text>();
        _myText.text = "";
        _intervalCounter = Interval;
        _dotCounter = 0;
    }

    void Update()
    {
        if (_intervalCounter >= 0)
        {
            _intervalCounter -= Time.deltaTime;
        }
        else
        {
            _intervalCounter = Interval;
            _dotCounter++;
            if (_dotCounter > DotAmount)
            {
                _dotCounter = 0;
                _myText.text = "";
            }
            else
            {
                 _myText.text += ".";
            }

        }
            
    }
}
