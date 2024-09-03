using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/Level/Wave")]
public class WaveLevel : ScriptableObject
{
    public int totalWaves = 5;  // Total jumlah wave
    public float waveInterval = 5f;  // Jeda antara wave
    public int waveMultiplier = 5;  // Jeda antara wave
    public Vector2[] spawnPositions; // Mengatur Posisi Spawn
}
