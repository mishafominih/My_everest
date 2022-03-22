using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static List<Action> MoveUpEvent;
    public static List<Action> MoveRightEvent;
    public static List<Action> MoveDownEvent;
    public static List<Action> MoveLeftEvent;
    public static List<Action> StayEvent;

    private static InputManager self;

    private List<KeyCode> pressedKeys;
    private List<KeyCode> checkedKeys;
    private KeyCode lastPressedKey;
    private Dictionary<KeyCode, List<Action>> buttonEvents;

    private void Awake()
    {
        if (self == null)
            self = this;
        else
            throw new Exception("Объект InputManager уже создан!");

        MoveUpEvent = new List<Action>();
        MoveRightEvent = new List<Action>();
        MoveDownEvent = new List<Action>();
        MoveLeftEvent = new List<Action>();
        StayEvent = new List<Action>();

        pressedKeys = new List<KeyCode>();
        checkedKeys = new List<KeyCode>
        {
            KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D
        };
        buttonEvents = new Dictionary<KeyCode, List<Action>>
        {
            { KeyCode.W, MoveUpEvent },
            { KeyCode.D, MoveRightEvent },
            { KeyCode.S, MoveDownEvent },
            { KeyCode.A, MoveLeftEvent }
        };
    }

    void Update()
    {
        var key = getLastPressedKey();
        List<Action> events;
        buttonEvents.TryGetValue(key, out events);
        CallEvent(events != null ? events : StayEvent);
    }

    private KeyCode getLastPressedKey()
    {
        foreach (var key in checkedKeys)
        {
            var isDown = Input.GetKeyDown(key);
            var isUp = Input.GetKeyUp(key);
            if (isDown && !pressedKeys.Contains(key))
            {
                lastPressedKey = key;
                pressedKeys.Add(key);
            }
            if (isUp && pressedKeys.Contains(key))
            {
                pressedKeys.Remove(key);
                if (lastPressedKey == key)
                {
                    lastPressedKey = pressedKeys.LastOrDefault();
                }
            }
        }
        return lastPressedKey;
    }

    private void CallEvent(List<Action> events)
    {
        foreach (var evnt in events)
        {
            evnt.Invoke();
        }
    }
}
