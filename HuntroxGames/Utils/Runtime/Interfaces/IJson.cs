using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HuntroxGames
{
    public interface IJson<out T>
    {
        string ToJson();
        T FromJson(string json);
    }
}
