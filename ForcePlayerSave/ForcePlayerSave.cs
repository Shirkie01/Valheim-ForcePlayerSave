using BepInEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Reflection;

[BepInPlugin("5d419a70-bc24-4807-bec3-be56c40acd9e", "Force Player Save", "1.0.0.0")]
public class ForcePlayerSave : BaseUnityPlugin
{
    private KeyCode saveKey = KeyCode.RightShift;

    private void Update()
    {
        if(Player.m_localPlayer == null)
        {
            return;
        }

        if(Input.GetKeyDown(saveKey))
        {
            try
            {                
                Player.m_localPlayer.Message(MessageHud.MessageType.TopLeft, "Saving...");                
                var saveTimerField = typeof(Game).GetField("m_saveTimer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);                
                saveTimerField.SetValue(Game.instance, EnvMan.instance.m_dayLengthSec + 1);                
            }
            catch(Exception e)
            {
                Debug.LogError("Failed to force save." + Environment.NewLine + e);
                Player.m_localPlayer.Message(MessageHud.MessageType.TopLeft, "Save failed.");                
            }
        }
    }
}

