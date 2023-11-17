using Fungus;
using UnityEngine;

[EventHandlerInfo("Wendigo",
    "On Start",
    "The block will execute when the game is started.")]
[AddComponentMenu("")]
public class GameStartEvent : EventHandler
{
    private void Start()
    {
        ExecuteBlock();
    }
}
