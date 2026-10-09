using Modding;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using BingoSync.CustomGoals;
using BingoSync.Interfaces;

namespace BingoGoalPack2 {
    public class BingoGoalPack2: Mod {
        new public string GetName() => "BingoGoalPack2";
        public override string GetVersion() => "1.2.0.0";

        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects) {
            OrderedLoader.OnReadyForGoalsGameModes += SetupGoalsGameModes;
        }

        private void SetupGoalsGameModes(object _, EventArgs __) {
            Assembly assembly = Assembly.GetExecutingAssembly();

            Dictionary<string, BingoGoal> myGoals = processEmbeddedJson(assembly, "Goals");
            IGameMode mode = new SimpleGameMode("GoalPack2", myGoals);
            Goals.RegisterGoalsForCustom("Goal Pack 2", myGoals);
        }

        private Dictionary<string, BingoGoal> processEmbeddedJson(Assembly assembly, string jsonName) {
            string resourceName = assembly.GetManifestResourceNames().Single(str => str.EndsWith("Squares." + jsonName + ".json"));
            Stream stream = assembly.GetManifestResourceStream(resourceName);
            return Goals.ProcessGoalsStream(stream);
        }
    }
}