using HuntroxGames.Examples;
using UnityEditor;
using UnityEngine;

namespace HuntroxGames.Utils.Editor
{
    [CustomEditor(typeof(WebhookSendExample))]
    public class WebhookSendExampleEditor : UnityEditor.Editor
    {
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Send Webhook"))
            {
                var webhookSendExample = (WebhookSendExample)target;
                webhookSendExample.Send();
            }

            if (GUILayout.Button("Send Webhook from Resources Folder"))
            {
                var webhookSendExample = (WebhookSendExample)target;
                webhookSendExample.SendWebHook();
            }

            if (GUILayout.Button("Webhook with Texture2D"))
            {
                var webhookSendExample = (WebhookSendExample)target;
                webhookSendExample.SendWebHookWithTexture2D();
            }
        }
    }
}
