using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonParent : MonoBehaviour
{
    [SerializeField] GameObject[] m_buttons;
    [SerializeField] GameObject m_buttonParent;
    [SerializeField] float radius;
    float y;
    void Start()
    {
        GameObject buttonObj = Instantiate(m_buttons[0], m_buttonParent.transform);
        buttonObj.transform.parent = m_buttonParent.transform;

        //print($"{radius}/{}")

        Equation();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Equation()
    {
        
      //print($"({radius},{yCoordinate})");
    }





}
