using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Это общий класс, используемый для связи между объектами.
/// </summary>
public class GameController : MonoBehaviour
{
    /// <summary>
    /// Это вспомогательный класс хранилища типов товаров.
    /// Решение, которе Unity использует для JSON по умолчанию, не поддерживает массивы данных.
    /// </summary>
    [Serializable]
    class GoodsWrapper
    {
        public Good[] Goods;
    }

    public static GameController instance;
    [Tooltip("Хранилище с ссылкой на текстовый файл с предложениями. Должен содержать компонент OffersStorage.")]
    public OffersStorage Offers;    
    [Tooltip("Ссылка на текст с JSON, который содержит информацию о типах товаров.")]
    public TextAsset GoodsJson;
    [Tooltip("Окно, где будут появляться предложения. Должен содержать компонент UIOffersContent.")]
    public UIOffersContent UIContent;

    private List<Good> Goods;

    private void Awake()
    {
        //Этот класс - singleton, так как такие классы обычно нужны для общего доступа.
        if (instance != null)
        {
            Destroy(this);
        }
        else {
            instance = this;
        }

        //Здесь генерируется список типов товаров.
        Goods = new List<Good>();
        string goodsJson = GoodsJson.text;
        GoodsWrapper wrapper = JsonUtility.FromJson<GoodsWrapper>(goodsJson);
        foreach (Good g in wrapper.Goods)
        {
            Goods.Add(g);
        }
    }

    public void Start()
    {
        //Иницилизируем окно с предложениями.
        //Получаем для этого список предложений из хранилища предложений.
        UIContent.Init(Offers.GetOffers());
    }

    /// <summary>
    /// Эта функция возвращает класс товара с определённым id
    /// </summary>
    /// <param name="goodId">id класса данных</param>
    /// <returns>Класс данных с соответствующим товаром.</returns>
    public Good GetGood(string goodId)
    {
        foreach (Good good in Goods)
        {
            if (good.Id == goodId)
            {
                return good;
            }
        }
        Debug.LogWarning("There is no good with id " + goodId);
        return null;
    }
}
