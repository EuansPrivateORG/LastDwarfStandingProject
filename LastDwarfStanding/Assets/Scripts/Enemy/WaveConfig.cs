using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Last Dwarf Standing/Wave Config")]
public class WaveConfig : ScriptableObject
{
    public List<GameObject> enemyList = new List<GameObject>();
}
