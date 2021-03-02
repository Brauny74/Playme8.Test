using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

/// <summary>
/// Это класс для отображения одного предложения.
/// </summary>
public class UIOffer : MonoBehaviour
{
    [Tooltip("Назначается автоматически, RectTransform, нужен для назначения позиции.")]
    public RectTransform rectTransform;

    [Tooltip("Текст с именем игрока.")]
    public TMP_Text PlayerName;
    [Tooltip("Текст с уровнем игрока.")]
    public TMP_Text PlayerLevel;
    [Tooltip("Текст с названием товара.")]
    public TMP_Text GoodName;
    [Tooltip("Текст с количеством товара в предложении.")]
    public TMP_Text GoodAmount;
    [Tooltip("Текст с ценой предложения.")]
    public TMP_Text GoodPrice;
    [Tooltip("Изображение с портретом игрока.")]
    public Image PlayerPortrait;
    [Tooltip("Изображение с иконкой товара.")]
    public Image GoodIcon;

    public void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    /// <summary>
    /// Эта функция иницилизирует конкретный блок с предложением.
    /// </summary>
    /// <param name="offer">Экземпляр класса данных с предложением.</param>
    public void Init(Offer offer)
    {
        PlayerName.text = offer.PlayerName;
        PlayerLevel.text = offer.PlayerLevel.ToString();
        GoodAmount.text = "x"+offer.GoodAmount.ToString();
        GoodPrice.text = offer.Price.ToString();
        Good g = GameController.instance.GetGood(offer.GoodId);
        GoodName.text = g.Name;

        Addressables.LoadAssetAsync<Sprite>(g.PicAdress).Completed += OnPicLoaded;
        //Теоретически здесь можно передавать Player ID, чтобы получить конкретную аватарку от социальной сети
        //Но так как это просто тестовый проект, я передаю название файла на сервере и загружаю его.
        StartCoroutine(LoadPortrait(offer.PlayerId));
    }

    void OnPicLoaded(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<Sprite> obj)
    {
        GoodIcon.sprite = obj.Result;
    }

    IEnumerator LoadPortrait(string playerId)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture("https://raw.githubusercontent.com/Brauny74/Brauny74.PicsStorage/main/" + playerId + ".png");
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogWarning(www.error);
        }
        else
        {
            Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            PlayerPortrait.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
        }
            
    }
}
