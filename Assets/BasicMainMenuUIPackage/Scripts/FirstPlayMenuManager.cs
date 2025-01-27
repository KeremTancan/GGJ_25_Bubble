using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FirstPlayMenuManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private TextMeshProUGUI uyariText;
    [SerializeField] private GameObject normalMainMenu;

    private TouchScreenKeyboard keyboard;

    private void Awake()
    {
        usernameInputField.characterLimit = 20;
        uyariText.text = "";

        // Mobil platformda klavyeyi açmak için event ekliyoruz
        usernameInputField.onSelect.AddListener(OnInputFieldSelected);
        usernameInputField.onDeselect.AddListener(OnInputFieldDeselected);
    }

    public void Next()
    {
        CheckUsername();        
    }

    private void OnDestroy()
    {
        // Listener'ları temizle
        usernameInputField.onSelect.RemoveListener(OnInputFieldSelected);
        usernameInputField.onDeselect.RemoveListener(OnInputFieldDeselected);
    }

    private void OnInputFieldSelected(string text)
    {
        // Mobil platformda klavyeyi otomatik aç
        keyboard = TouchScreenKeyboard.Open(usernameInputField.text, TouchScreenKeyboardType.Default);
    }

    private void OnInputFieldDeselected(string text)
    {
        // Klavyeyi kapat
        if (keyboard != null && keyboard.active)
        {
            keyboard.active = false;
            keyboard = null;
        }
    }

    private void Update()
    {
        // Eğer klavye açıksa ve metin değişmişse, TMP_InputField'e metni eşitle
        if (keyboard != null && keyboard.active)
        {
            usernameInputField.text = keyboard.text;
        }
    }

    void GoNext()
    {
        PlayerPrefs.SetString("Username", usernameInputField.text);
        gameObject.SetActive(false);
        normalMainMenu.SetActive(true);
    }

    void CheckUsername()
    {
        uyariText.text = "";

        string typedName = usernameInputField.text;

        if (string.IsNullOrEmpty(typedName))
        {
            uyariText.text = "Kullanıcı adı boş olamaz";
            Fail();
            return;
        }

        if (typedName.Contains(" ") || typedName.Contains("\n") || typedName.Contains("\t"))
        {
            uyariText.text = "Kullanıcı adı boşluk içeremez";
            Fail();
            return;
        }

        LeaderboardManager.Instance.IsUsernameAlreadyExist(typedName, GoNext, Fail);
    }

    void Fail(bool fromLeaderboard = false)
    {
        usernameInputField.ActivateInputField();
        if (fromLeaderboard)
        {
            uyariText.text = "Kullanıcı adı zaten alınmış";
        }
    }
}
