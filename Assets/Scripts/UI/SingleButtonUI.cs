using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SingleButton : MonoBehaviour
{
    private Button buttonSingle;
    private Image busy;
    private Image ready;
    private TextMeshProUGUI spellName;

    [SerializeField] private float cooldownTime = 10f;
    private float maxCooldonwTime;

    private void Awake()
    {
        busy = transform.Find("busy").GetComponent<Image>();
        ready = transform.Find("ready").GetComponent<Image>();

        spellName = transform.Find("spellName").GetComponent<TextMeshProUGUI>();

        buttonSingle = GetComponent<Button>();

        buttonSingle.onClick.AddListener(() =>
        {
            busy.fillAmount = 1f;
        });

        maxCooldonwTime = cooldownTime;

        busy.fillAmount = 0f;

    }

    private void Update()
    {
        StartSpell();
    }

    private void StartSpell()
    {
        if (busy.fillAmount != 0f)
        {
            cooldownTime -= Time.deltaTime;

            busy.fillAmount = cooldownTime / maxCooldonwTime;
        }
        else
        {
            cooldownTime = maxCooldonwTime;
        }
    }


}
