using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GhostMessage", menuName = "Ghost Message")]
[System.Serializable]
public class GhostMessage : ScriptableObject
{
    public List<RealMessage> messages = new List<RealMessage>();
}