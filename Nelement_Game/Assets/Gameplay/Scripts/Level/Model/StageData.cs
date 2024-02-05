using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ProyectNelement.Gameplay.Level.Model
{
    [Serializable]
    public class RespawnData
    {
        public int Index;
        public Vector3 Position;
    }
    
    [Serializable, CreateAssetMenu(fileName = "StageDataSo", menuName = "Gameplay/Level/StageData", order = 0)]
    public class StageData : ScriptableObject
    {
        #region EXPOSED_FIELDS
        [SerializeField] private int index = 0;
        [SerializeField] private List<StageData> concurrentStages = null;
        [SerializeField] private List<RespawnData> spawnPoints = null;
        [SerializeField] private AssetReferenceGameObject levelReference = null;
        #endregion

        #region PROPERTIES
        public int Index => index;
        public List<StageData> ConcurrentStages => concurrentStages;
        public List<RespawnData> SpawnPoints => spawnPoints;
        public AssetReferenceGameObject LevelReference => levelReference;
        #endregion
    }
}