using Exiled.API.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastRespawnUtility
{
    public class Config : IConfig
    {
        /// <inheritdoc />
        public bool IsEnabled { get; set; } = true;

        /// <inheritdoc />
        public bool IsDebugEnabled { get; set; } = false;


        /// <summary>
        /// Sets the default time to wait to respawn a player. If set to -1.0f then player will not respawn through this plugin.
        /// </summary>
        public float UniversalRespawnTimer { get; set; } = 5.0f;

        /// <summary>
        /// Default role for all classes to respawn as.
        /// </summary>
        public RoleType UniversalDefaultRole { get; set; } = RoleType.ChaosMarauder;


        /// <summary>
        /// What to respawn a player as based on their previous role. Probability occurance given by float. If no role can be selected based on probability, default to UniversalDefaultRole
        /// </summary>
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
       
    }
}
