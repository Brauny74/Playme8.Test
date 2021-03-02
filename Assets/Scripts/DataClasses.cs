using System;
//Это вспомогательные классы, которые хранят данные для передачи между рабочими классами.
//Они Seriazable, так как они частично хранятся в JSON

/// <summary>
/// Это класс данных, описывающий тип товаров.
/// </summary>
[Serializable]
public class Good
{
    public string Id;
    public string Name;
    public string PicAdress;
    public int Price;
}

/// <summary>
/// Это класс данных, описывающий каждое отдельное предложение.
/// </summary>
[Serializable]
public class Offer
{
    public string PlayerId;
    public string PlayerName;
    public int PlayerLevel;
    public string GoodId;
    public int GoodAmount;
    public int Price;

    public override string ToString()
    {
        return PlayerName + "LV:" + PlayerLevel + " sells " + GoodAmount + " " + GoodId + " for " + Price;
    }
}    
