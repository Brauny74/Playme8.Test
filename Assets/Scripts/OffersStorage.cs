using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Этот класс конвертирует JSON из текста в список предложений.
/// </summary>
public class OffersConverter
{
    class OffersWrapper
    {
        public Offer[] Offers;
    }

    public List<Offer> ResultOffers; //Это список предложений, который получается в результате конвертации.

    /// <summary>
    /// Конструктор получает текст, содержащий JSON и конвертирует его в список предложений.
    /// </summary>
    /// <param name="jsonText">Текст, содержащий JSON с предложением.</param>
    public OffersConverter(string jsonText)
    {
        ResultOffers = new List<Offer>();
        OffersWrapper wrapper = JsonUtility.FromJson<OffersWrapper>(jsonText);
        foreach (Offer offer in wrapper.Offers)
        {
            ResultOffers.Add(offer);
        }
    }
}

/// <summary>
/// Этот вспомогательный класс, который добавляется к объекту в Инспекторе и хранит ссылку на файл.
/// По большей части, чтобы разделить файл и текст и чтобы делать ссылку на файл в Инспекторе.
/// </summary>
public class OffersStorage : MonoBehaviour
{
    public TextAsset OffersFile;

    public List<Offer> GetOffers()
    {
        OffersConverter converter = new OffersConverter(OffersFile.text);
        return converter.ResultOffers;
    }
}
