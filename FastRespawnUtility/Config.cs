using Exiled.API.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FastRespawnUtility
{
    public class Config : IConfig
    {
        /// <inheritdoc />
        [Description("Whether to enabled or disable plugin")]
        public bool IsEnabled { get; set; } = true;

        /// <inheritdoc />
        [Description("Whether to enabled/disable debug")]
        public bool IsDebugEnabled { get; set; } = false;


        /// <summary>
        /// Sets the default time to wait to respawn a player. If set to -1.0f then player will not respawn through this plugin.
        /// </summary>
        [Description("Sets the default time to wait to respawn a player. If set to -1.0f then player will not respawn through this plugin.")]
        public float UniversalRespawnTimer { get; set; } = 5.0f;

        /// <summary>
        /// Sets the default time to wait to reposition a player. Tune this based on server 
        /// </summary>
        [Description("Sets the default time to wait to respawn a player for their custom position. Tune this based on server ")]
        public float UniversalCustomSpawnWaitTime { get; set; } = 1.5f;

        /// <summary>
        /// Default role for all classes to respawn as.
        /// </summary>
        [Description("Default role for all classes to respawn as.")]
        public RoleType UniversalDefaultRole { get; set; } = RoleType.ChaosMarauder;

        /// <summary>
        /// Default spawn location for all classes to respawn as. Be VERY specific as map is semi-procedural
        /// </summary>
        [Description("Default spawn location for all classes to respawn as. Be VERY specific as map is semi-procedural")]
        public Vector3 UniversalCustomSpawn { get; set; } = Vector3.negativeInfinity;

        /// <summary>
        /// Whether to allow spawning after Warhead goes off.
        /// </summary>
        [Description("Whether to allow spawning after Warhead goes off.")]
        public bool StopSpawningAfterWarhead { get; set; } = false;

        /// <summary>
        /// How long to wait to respawn a player at start of round to UniversalRole.
        /// </summary>
        [Description("How long to wait to respawn a player at start of round to UniversalRole.")]
        public float SpawningReRollDelay { get; set; } = 2.0f;

        /// <summary>
        /// Prevent normal game spawning (Spawning Team for example).
        /// </summary>
        [Description("Normal game spawning (Spawning Team for example).")]
        public bool NormalGameSpawning { get; set; } = false;

        /// <summary>
        /// Overwrite first spawn the first time only."
        /// </summary>
        [Description("Overwrite first spawn the first time only.")]
        public bool FirstSpawnOnly { get; set; } = false;

        /// <summary>
        /// What to respawn a player as based on their previous role. Probability occurance given by float. If no role can be selected based on probability, default to UniversalDefaultRole
        /// </summary>
        [Description("What to respawn a player as based on their previous role. Probability occurance given by float. If no role can be selected based on probability, default to UniversalDefaultRole.")]
        public Dictionary<RoleType, Dictionary<RoleType, float>> RespawnRerollTypes { get; set; } = new Dictionary<RoleType, Dictionary<RoleType, float>>
        {
                { RoleType.FacilityGuard, new Dictionary<RoleType, float>() { { RoleType.FacilityGuard, 0.0f } } },
                { RoleType.NtfPrivate, new Dictionary<RoleType, float>() { { RoleType.FacilityGuard, 0.0f } } },
                { RoleType.NtfSergeant, new Dictionary<RoleType, float>() { { RoleType.FacilityGuard, 0.0f } } },
                { RoleType.NtfCaptain, new Dictionary<RoleType, float>() { { RoleType.FacilityGuard, 0.0f } } },
                { RoleType.NtfSpecialist, new Dictionary<RoleType, float>() { { RoleType.FacilityGuard, 0.0f } } },

                { RoleType.ChaosConscript, new Dictionary<RoleType, float>() { { RoleType.FacilityGuard, 0.0f } } },
                { RoleType.ChaosMarauder, new Dictionary<RoleType, float>() { { RoleType.FacilityGuard, 0.0f } } },
                { RoleType.ChaosRepressor, new Dictionary<RoleType, float>() { { RoleType.FacilityGuard, 0.0f } } },
                { RoleType.ChaosRifleman, new Dictionary<RoleType, float>() { { RoleType.FacilityGuard, 0.0f } } },

                { RoleType.Scp049, new Dictionary<RoleType, float>() { { RoleType.FacilityGuard, 0.0f } } },
                { RoleType.Scp0492, new Dictionary<RoleType, float>() { { RoleType.FacilityGuard, 0.0f } } },
                { RoleType.Scp079, new Dictionary<RoleType, float>() { { RoleType.FacilityGuard, 0.0f } } },
                { RoleType.Scp096, new Dictionary<RoleType, float>() { { RoleType.FacilityGuard, 0.0f } } },
                { RoleType.Scp106, new Dictionary<RoleType, float>() { { RoleType.FacilityGuard, 0.0f } } },
                { RoleType.Scp173, new Dictionary<RoleType, float>() { { RoleType.FacilityGuard, 0.0f } } },
                { RoleType.Scp93953, new Dictionary<RoleType, float>() { { RoleType.FacilityGuard, 0.0f } } },
                { RoleType.Scp93989, new Dictionary<RoleType, float>() { { RoleType.FacilityGuard, 0.0f } } },

                {RoleType.ClassD,  new Dictionary<RoleType, float>() { { RoleType.FacilityGuard, 0.0f } } },
                {RoleType.Scientist,  new Dictionary<RoleType, float>() { { RoleType.FacilityGuard, 0.0f } } },
                {RoleType.Tutorial,  new Dictionary<RoleType, float>() { { RoleType.FacilityGuard, 0.0f } } },
                {RoleType.Spectator,  new Dictionary<RoleType, float>() { { RoleType.FacilityGuard, 0.0f } } },

        };

        /// <summary>
        /// Sets how long to wait before respawning role, -1.0f means do not respawn, if not in this list then it defaults to UniversalRespawnTimer value.
        /// </summary>
        [Description("Sets how long to wait before respawning role, -1.0f means do not respawn, if not in this list then it defaults to UniversalRespawnTimer value.")]
        public Dictionary<RoleType, float> RoleRespawnTime { get; set; } = new Dictionary<RoleType, float>
        {         
                { RoleType.FacilityGuard,-1.0f},
                { RoleType.NtfPrivate,-1.0f},
                { RoleType.NtfSergeant,-1.0f},
                { RoleType.NtfCaptain,-1.0f},
                { RoleType.NtfSpecialist,-1.0f},

                { RoleType.ChaosConscript,-1.0f},
                { RoleType.ChaosMarauder,-1.0f},
                { RoleType.ChaosRepressor,-1.0f},
                { RoleType.ChaosRifleman,-1.0f},

                { RoleType.Scp049,-1.0f},
                { RoleType.Scp0492,-1.0f},
                { RoleType.Scp079,-1.0f},
                { RoleType.Scp096,-1.0f},
                { RoleType.Scp106,-1.0f},
                { RoleType.Scp173,-1.0f},
                { RoleType.Scp93953,-1.0f},
                { RoleType.Scp93989,-1.0f},

                {RoleType.ClassD, -1.0f},
                {RoleType.Scientist, -1.0f},
                {RoleType.Tutorial, -1.0f},

        };

        /// <summary>
        /// What to respawn a player as based on their previous role. Probability occurance given by float. If no role can be selected based on probability, default to UniversalDefaultRole
        /// </summary>
        [Description("Where to respawn a player as based on their previous role. If not specified default to UniversalRespawnPosition, NegativeInfinity means do not respawn at custom location")]
        public Dictionary<RoleType, Vector3> RespawnRollSpawnPositions { get; set; } = new Dictionary<RoleType, Vector3>
        {
                { RoleType.FacilityGuard, Vector3.negativeInfinity },
                { RoleType.NtfPrivate, Vector3.negativeInfinity },
                { RoleType.NtfSergeant, Vector3.negativeInfinity },
                { RoleType.NtfCaptain, Vector3.negativeInfinity },
                { RoleType.NtfSpecialist, Vector3.negativeInfinity },

                { RoleType.ChaosConscript, Vector3.negativeInfinity },
                { RoleType.ChaosMarauder, Vector3.negativeInfinity },
                { RoleType.ChaosRepressor, Vector3.negativeInfinity },
                { RoleType.ChaosRifleman, Vector3.negativeInfinity },

                { RoleType.Scp049, Vector3.negativeInfinity },
                { RoleType.Scp0492, Vector3.negativeInfinity },
                { RoleType.Scp079,  Vector3.negativeInfinity },
                { RoleType.Scp096,  Vector3.negativeInfinity },
                { RoleType.Scp106,  Vector3.negativeInfinity },
                { RoleType.Scp173,  Vector3.negativeInfinity },
                { RoleType.Scp93953,  Vector3.negativeInfinity },
                { RoleType.Scp93989,  Vector3.negativeInfinity },

                {RoleType.ClassD,   Vector3.negativeInfinity },
                {RoleType.Scientist,   Vector3.negativeInfinity },
                {RoleType.Tutorial,   Vector3.negativeInfinity },
                {RoleType.Spectator,   Vector3.negativeInfinity },

        };
    }
}
