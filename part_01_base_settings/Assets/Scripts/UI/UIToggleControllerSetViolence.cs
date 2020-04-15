using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIToggleControllerSetViolence : MonoBehaviour
{
    private Toggle m_Toggle;
    //public Text m_Text;
    private GameController gameController;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        m_Toggle = GetComponent<Toggle>();
        m_Toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(m_Toggle);
        });

        m_Toggle.isOn = gameController.violenceEnabled;
    }

    void ToggleValueChanged(Toggle change)
    {
        gameController.setViolence(change.isOn);
    }
}
