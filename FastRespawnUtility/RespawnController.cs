using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FastRespawnUtility
{
    public class RespawnController
    {
        private Main PluginInstance;

        public RespawnController(Main main)
        {
            this.PluginInstance = main;
        }

        public enum RespawnType : ushort {
            DefaultByConfig,
            TimeByRole,
            RoleToRoleRules,
        }


        internal void OnDying(DyingEventArgs ev)
        {
            if(ev.Target == null){
                return;
            }
            if(ev.Target.Role == null){
                return;
            }

            if(ev.Target.Role.Type is RoleType.Spectator || ev.Target.Role.Type is RoleType.None){
                return;
            }

            Log.Debug("OnDying 1", PluginInstance.Config.IsDebugEnabled);
            if(PluginInstance.Config.RespawnRerollTypes.TryGetValue(ev.Target.Role.Type, out Dictionary<RoleType, float> rolesToSpawnAs)){
                foreach (KeyValuePair<RoleType, float> rolesToCheck in rolesToSpawnAs){
                    float cur_ran = UnityEngine.Random.value;
                    Log.Debug($"OnDying 1.5 {cur_ran}", PluginInstance.Config.IsDebugEnabled);
                    if (cur_ran <= Mathf.Clamp(rolesToCheck.Value, 0.00f, 1.00f))
                    {
                        Log.Debug("OnDying 2", PluginInstance.Config.IsDebugEnabled);
                        if (CustomRespawn(rolesToCheck.Key, RespawnType.RoleToRoleRules, ev.Target)){
                            Log.Debug("OnDying 3", PluginInstance.Config.IsDebugEnabled);
                            return;
                        }
                    }
                }
                Log.Debug("OnDying 4", PluginInstance.Config.IsDebugEnabled);
                CustomRespawn(ev.Target.Role.Type, RespawnType.DefaultByConfig, ev.Target);
            }
            else if(PluginInstance.Config.RoleRespawnTime.TryGetValue(ev.Target.Role.Type, out float timeToWait)){
                Log.Debug("OnDying 5", PluginInstance.Config.IsDebugEnabled);
                if(!CustomRespawn(ev.Target.Role.Type, RespawnType.TimeByRole, ev.Target, timeToWait)){
                    Log.Debug("OnDying 5.5", PluginInstance.Config.IsDebugEnabled);
                    CustomRespawn(ev.Target.Role.Type, RespawnType.DefaultByConfig, ev.Target);
                }
            }
            else if(PluginInstance.Config.UniversalRespawnTimer != -1.0f){
                Log.Debug("OnDying 6", PluginInstance.Config.IsDebugEnabled);
                CustomRespawn(ev.Target.Role.Type, RespawnType.DefaultByConfig, ev.Target);
            }
        }

        private bool CustomRespawn(RoleType role, RespawnType respawnReason, Player player, float secondsToWait = 5.0f)
        {
            switch(respawnReason){ 
                case RespawnType.DefaultByConfig:
                    // If we're defaulting to config, use universal respawn timer and role
                    Log.Debug($"RespawnType.DefaultByConfig {PluginInstance.Config?.UniversalRespawnTimer ?? secondsToWait}", PluginInstance.Config.IsDebugEnabled);
                    Timing.CallDelayed(PluginInstance.Config?.UniversalRespawnTimer ?? secondsToWait, delegate {
                        player.SetRole(PluginInstance.Config.UniversalDefaultRole, Exiled.API.Enums.SpawnReason.ForceClass);
                    });
                    break;
                case RespawnType.RoleToRoleRules:
                    // If we have a role spawn time associated, use that, otherwise default to universal.
                    if(PluginInstance.Config.RoleRespawnTime.TryGetValue(role, out float timeToWait)){
                        Log.Debug($"RespawnType.RoleToRoleRules normal path {timeToWait}", PluginInstance.Config.IsDebugEnabled);
                        if (timeToWait < 0.0f)
                        {
                            return false;
                        }
                        Timing.CallDelayed(timeToWait, delegate {
                            player.SetRole(role, Exiled.API.Enums.SpawnReason.ForceClass);
                        });
                    }
                    else{
                        Log.Debug($"RespawnType.RoleToRoleRules false path {timeToWait}", PluginInstance.Config.IsDebugEnabled);
                        // We did not find the rules for this role, so we will not use it and continue iterating the roles in RespawnRerollTypes
                        return false;
                    }
                    break;
                case RespawnType.TimeByRole:
                    // If we have the role specified in the RoleRespawnTime, player spawns as that role based on time limit.
                    Log.Debug($"RespawnType.TimeByRole {secondsToWait}", PluginInstance.Config.IsDebugEnabled);
                    if (secondsToWait < 0.0f)
                    {
                        return false;
                    }
                    Timing.CallDelayed(secondsToWait, delegate
                    {
                        player.SetRole(role, Exiled.API.Enums.SpawnReason.ForceClass);
                    });
                    break;
                default:
                    break;
            }
            return true;
        }

    }
}