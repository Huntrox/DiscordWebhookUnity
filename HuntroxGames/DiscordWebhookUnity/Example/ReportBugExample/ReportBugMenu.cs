using HuntroxGames.Utils.DiscordWebhook;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HuntroxGames
{
    public class ReportBugMenu : MonoBehaviour
    {
        
        [Header("Webhook Settings")] 
        [SerializeField] private string webhookName = "Bug Report Bot";
        [SerializeField] private string webhookAvatarUrl = ""; 
        [SerializeField] private string webhookUrl = "";
        
        [Header("UI Elements")]
        [SerializeField] private TMP_InputField reportTitleTextField;
        [SerializeField] private TMP_InputField reportTextField;
        [SerializeField] private Toggle playerLogToggle;
        [SerializeField] private Toggle systemInfoToggle;


        
        
        public void SendReport()
        {
            //creating new webhook with username and avatar url if provided
            var webhook = new Webhook
            {
                username = webhookName,
                avatar_url = webhookAvatarUrl
            };
            
            // getting user input
            var reportTitle = reportTitleTextField.text;
            var report = reportTextField.text;
            var playerLog = playerLogToggle.isOn;
            var systemInfo = systemInfoToggle.isOn;
            
            //formatting title and report text
            var webhookContent = $"# __{reportTitle}__\n\n{report}";
            
            //set webhook content from user input
            webhook.SetContent(webhookContent);

            // checking if user wants to send player log
            //if yes, create attachment with player log file and add it to webhook
            if (playerLog)
                webhook.AddAttachment(CreatePlayerLogAttachment());

            // checking if user wants to send system info
            //if yes, create embed with system info
            if (systemInfo) 
                webhook.AddEmbed(CreateSystemInfoEmbed());
            
            webhook.SendWebhook(webhookUrl);
        }


        
        private static Attachment CreatePlayerLogAttachment()
        {
            var playerLogPath = Application.persistentDataPath + "/player.log";
            return new Attachment(playerLogPath, "player_log");
        }
        
        private static Embed CreateSystemInfoEmbed()
        {
            //Creating embed with system info
            return new Embed
            {
                title = "System Info",
                description = "Report System information",
                embedColor = Color.cyan,
                fields = new[]
                {
                    new Field { name = "OS", value = SystemInfo.operatingSystem },
                    new Field { name = "CPU", value = SystemInfo.processorType ,inline = true},
                    new Field { name = "Memory", value = SystemInfo.systemMemorySize.ToString() ,inline = true},
                    new Field { name = "", value ="" ,inline = false},//empty line to break fields
                    new Field { name = "GPU", value = SystemInfo.graphicsDeviceName ,inline = true},
                    new Field { name = "GPU Memory", value = SystemInfo.graphicsMemorySize.ToString() ,inline = true},
                    new Field { name = "Screen", value = $"{Screen.width}x{Screen.height}" },
                }
            };
        }
    }
}
