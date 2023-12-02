using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class PlayerData2 : MonoBehaviour
{
   public static void SaveData()
    {
        Player player = GameSceneManager.Instance.Player;


        XmlDocument xmlDoc = new XmlDocument();
        XmlElement XmlEl = xmlDoc.CreateElement("PlayerDB");
        xmlDoc.AppendChild(XmlEl);

        XmlElement ElementSetting = xmlDoc.CreateElement("Player");

        //int HP = (int)player.Health.HP;
        //int MP = (int)player.Mana.MP;

        ElementSetting.SetAttribute("HP", 10.ToString());

        //ElementSetting.SetAttribute("CurrentHP", HP.ToString());
        //ElementSetting.SetAttribute("CurrentMP", MP.ToString());
        //ElementSetting.SetAttribute("Gold", player.Gold.ToString());

        XmlEl.AppendChild(ElementSetting);

        xmlDoc.Save(Application.dataPath + "/Resources/PlayerData.xml");
    }

    public static void LoadData()
    {
        if (!System.IO.File.Exists(Application.dataPath + "/Resources/PlayerData.xml")) return;

        XmlDocument XmlDoc = new XmlDocument();

        XmlDoc.Load(Application.dataPath + "/Resources/PlayerData.xml");
        XmlElement XmlEl = XmlDoc["PlayerDB"];

        Player player = GameSceneManager.Instance.Player;

        foreach (XmlElement ItemElement in XmlEl.ChildNodes)
        {
            //player.Health.HP = System.Convert.ToSingle(ItemElement.GetAttribute("CurrentHP"));
            //player.Mana.MP = System.Convert.ToSingle(ItemElement.GetAttribute("CurrentMP"));
            //player.Gold = System.Convert.ToInt32(ItemElement.GetAttribute("Gold"));
        }
    }
}
