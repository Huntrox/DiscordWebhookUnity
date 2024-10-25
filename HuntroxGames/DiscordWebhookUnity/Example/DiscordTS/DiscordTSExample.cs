using System;
using System.Linq;
using HuntroxGames.Utils;
using HuntroxGames.Utils.DiscordWebhook;
using TMPro;
using UnityEngine;

namespace HuntroxGames.Examples
{
    public class DiscordTSExample : MonoBehaviour
    {
        [Header("Webhook Settings")] 
        [SerializeField] private string webhookName = "DTS Bot";
        [SerializeField] private string webhookAvatarUrl = ""; 
        [SerializeField] private string webhookUrl = "";

        [Header("UI References")]
        [SerializeField] private TMP_Dropdown tsTypeDropdown;
        [SerializeField] private TMP_InputField textInput;

        private void Start()
        {
            //clear all options in tsTypeDropdown
            tsTypeDropdown.ClearOptions();
            //create options for all DiscordTimestampFormat enum values
            tsTypeDropdown.AddOptions(Enum.GetNames(typeof(DiscordTimestampFormat)).ToList());
            tsTypeDropdown.value = 0;
            tsTypeDropdown.RefreshShownValue();
        }
        public void SendWebhook()
        {
            var content = textInput.text;
            var tsType = (DiscordTimestampFormat)tsTypeDropdown.value;
            var timestamp = Utils.Utils.DateToDiscordTimestamp(DateTime.Now, tsType);
            var webhook = new Webhook
            {
                username = webhookName,
                avatar_url = webhookAvatarUrl
            };
            webhook.SetContent($"{content} {timestamp}");
            webhook.SendWebhook(webhookUrl);
            
        }
    }
}
