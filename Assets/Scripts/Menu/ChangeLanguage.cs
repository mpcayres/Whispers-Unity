using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGTALK.Localization;
using UnityEngine.UI;

public class ChangeLanguage : MonoBehaviour {
    public int language;
    private Button button { get { return GetComponent<Button>(); } }
    PreferencesManager script;

    private void Awake()
    {
        GameObject menuCanvas = GameObject.Find("MenuCanvas").gameObject;
        script = menuCanvas.GetComponent<PreferencesManager>();
    }

    void Start()
    {
        button.onClick.AddListener(() => ChangeTextLanguage());
    }

    void ChangeTextLanguage()
    {
        switch (language) {
            case 1:
                PlayerPrefs.SetString("Language", "EN_US");
                LanguageSettings.actualLanguage = SupportedLanguages.EN_US;
                script.ChangeTextMenu("New Game", "Continue", "Load Game", "Choose the mission", "Controls", "Options", "Language", "Exit", "Back");
                break;
            case 2:
                PlayerPrefs.SetString("Language", "PT_BR");
                LanguageSettings.actualLanguage = SupportedLanguages.PT_BR;
                script.ChangeTextMenu("Novo Jogo", "Continuar", "Carregar Jogo", "Escolha a missão", "Controles", "Opções", "Idioma", "Sair", "Voltar");
                break;
        }
    }

}
