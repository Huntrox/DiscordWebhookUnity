using HuntroxGames.Utils;
using UnityEngine;

namespace HuntroxGames.Utils
{
    
    public class WebService : Singleton<WebService>
    {
        protected override void Awake()
        {
            destroyOnLoad = false;
            base.Awake();
            if (gameObject.hideFlags != HideFlags.HideInHierarchy) 
                gameObject.hideFlags = HideFlags.HideInHierarchy;
            
        }
    }   

}