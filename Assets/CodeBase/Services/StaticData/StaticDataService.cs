﻿using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string MoneyPackConfigPath = "StaticData/MoneyPackConfig";
        private const string DefaultLocationsPath = "StaticData/DefaultLocations";

        private MoneyPackConfig _moneyPackConfig;
        private Dictionary<LocationKey, LocationStaticData> _defaultLocations;

        public MoneyPackConfig MoneyPackConfig =>
            _moneyPackConfig;

        private TData GetDataFor<TData, TKey>(TKey key, IReadOnlyDictionary<TKey, TData> from) =>
            from.TryGetValue(key, out TData staticData)
                ? staticData
                : throw new NullReferenceException($"There is no {from.First().Value.GetType().Name} data with key: {key}");

        public LocationStaticData LocationFor(LocationKey key) =>
            GetDataFor(key, _defaultLocations);

        public void Load()
        {
            _moneyPackConfig = Resources.Load<MoneyPackConfig>(MoneyPackConfigPath);
            _defaultLocations = LoadFor<LocationStaticData, LocationKey>(DefaultLocationsPath, x => x.Key);
        }

        private Dictionary<TKey, TData> LoadFor<TData, TKey>(string path, Func<TData, TKey> keySelector) where TData : ScriptableObject =>
            Resources
                .LoadAll<TData>(path)
                .ToDictionary(keySelector, x => x);
    }
}