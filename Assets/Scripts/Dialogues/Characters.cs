using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/Character", order = 1)]
public class Characters : ScriptableObject
{
    [Tooltip("Character name")] public string Name;
    //[Tooltip("Character sprite")]public ImageCharacter[] Sprites;
}

[Serializable]
public class ImageCharacter
{
    public enum emotions
    {
        normal,
        happy,
        smile,
        sad,
        cry,
        angry,
        furious
    }

    [Tooltip("Emotion name")] public emotions EmotionName;
    [Tooltip("Emotion sprite")] public Sprite SpriteEmotion;
}