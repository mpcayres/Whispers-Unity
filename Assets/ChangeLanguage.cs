using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGTALK.Localization;
using UnityEngine.UI;

public class ChangeLanguage : MonoBehaviour {
    public int language;
    private Button button { get { return GetComponent<Button>(); } }

    void Start() {

        button.onClick.AddListener(() => ChangeTextLanguage());
    }



    // Update is called once per frame
    void ChangeTextLanguage()

    {
        switch (language) {
            case 1:
                LanguageSettings.actualLanguage = SupportedLanguages.EN_US;
                break;
            case 2:
                LanguageSettings.actualLanguage = SupportedLanguages.PT_BR;
                break;


        }

    }
}
