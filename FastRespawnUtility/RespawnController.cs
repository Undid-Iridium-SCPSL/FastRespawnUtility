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
        private RespawnControllerMain PluginInstance;

        public RespawnController(RespawnControllerMain main)
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
            
            if (!RespawnControllerMain.isEnabledAtRuntime){
                Log.Debug("OnDying, Plugin disabled at runtime", PluginInstance.Config.IsDebugEnabled);
                return;
            }

            if(PluginInstance.Config.FirstSpawnOnly){
                return;
            }

            if(ev.Target == null){
                return;
            }
            if(ev.Target.Role == null){
                return;
            }

            if(ev.Target.Role.Type is RoleType.Spectator || ev.Target.Role.Type is RoleType.None){
                return;
            }

            Log.Debug("OnDying, Target was defined and was not spectator/none. ", PluginInstance.Config.IsDebugEnabled);
            if(PluginInstance.Config.RespawnRerollTypes.TryGetValue(ev.Target.Role.Type, out Dictionary<RoleType, float> rolesToSpawnAs)){
                foreach (KeyValuePair<RoleType, float> rolesToCheck in rolesToSpawnAs){
                    float currentRandom = UnityEngine.Random.value;
                    float boundedProabilityVal = Mathf.Clamp(rolesToCheck.Value, 0.00f, 1.00f);
                    Log.Debug($"OnDying, RespawnRerollType currentRandom: {currentRandom} versus probability: {boundedProabilityVal}", PluginInstance.Config.IsDebugEnabled);
                    if (currentRandom <= boundedProabilityVal)
                    {
                        Log.Debug("OnDying, Proability was within threshhold.", PluginInstance.Config.IsDebugEnabled);
                        if (CustomRespawn(rolesToCheck.Key, RespawnType.RoleToRoleRules, ev.Target)){
                            Log.Debug("OnDying CustomRespawn was successful for probability path.", PluginInstance.Config.IsDebugEnabled);
                            return;
                        }
                    }
                }
                Log.Debug("OnDying, CustomRespawn/Probability did not succeed, defaulting to DefaultByConfig", PluginInstance.Config.IsDebugEnabled);
                CustomRespawn(ev.Target.Role.Type, RespawnType.DefaultByConfig, ev.Target);
            }
            else if(PluginInstance.Config.RoleRespawnTime.TryGetValue(ev.Target.Role.Type, out float timeToWait)){
                Log.Debug("OnDying, Checking against RoleRespawnTime", PluginInstance.Config.IsDebugEnabled);
                if(!CustomRespawn(ev.Target.Role.Type, RespawnType.TimeByRole, ev.Target, timeToWait)){
                    Log.Debug("OnDying, RoleRespawnTime did not contain Role, running default", PluginInstance.Config.IsDebugEnabled);
                    CustomRespawn(ev.Target.Role.Type, RespawnType.DefaultByConfig, ev.Target);
                }
            }
            else if(PluginInstance.Config.UniversalRespawnTimer != -1.0f){
                Log.Debug("OnDying, Default path", PluginInstance.Config.IsDebugEnabled);
                CustomRespawn(ev.Target.Role.Type, RespawnType.DefaultByConfig, ev.Target);
            }
        }

        internal void OnSpawningTeam(RespawningTeamEventArgs ev)
        {
            if (!RespawnControllerMain.isEnabledAtRuntime)
            {
                Log.Debug("OnSpawning, Plugin disabled at runtime", PluginInstance.Config.IsDebugEnabled);
                return;
            }

            if (!RespawnControllerMain.Instance.Config.NormalGameSpawning){
                ev.IsAllowed = false;
            }
        }

        internal void OnSpawning(SpawningEventArgs ev)
        {
            if(!RespawnControllerMain.isEnabledAtRuntime){
                Log.Debug("OnSpawning, Plugin disabled at runtime", PluginInstance.Config.IsDebugEnabled);
                return;
            }

            if(PluginInstance.Config.StopSpawningAfterWarhead && Warhead.IsDetonated){
                Log.Debug("OnSpawning, StopSpawningAfterWarhead and Detonated was true", PluginInstance.Config.IsDebugEnabled);
                Timing.CallDelayed(PluginInstance.Config.SpawningReRollDelay, delegate
                {
                    Log.Debug("OnSpawning, Running SpawningReRoll", PluginInstance.Config.IsDebugEnabled);
                    ev.Player.SetRole(RoleType.Spectator, Exiled.API.Enums.SpawnReason.ForceClass);
                });
                return;
            }
            if(!ev.Player.SessionVariables.TryGetValue("RespawnedAtStart", out object startVerifiedFlag)){
                Log.Debug("OnSpawning, First time spawning, RespawnedAtStart added to SessionVar", PluginInstance.Config.IsDebugEnabled);
                ev.Player.SessionVariables.Add("RespawnedAtStart", true);
                Timing.CallDelayed(PluginInstance.Config.SpawningReRollDelay, delegate
                {
                    Log.Debug("OnSpawning, Running RespawnedAtStart", PluginInstance.Config.IsDebugEnabled);
                    ev.Player.SetRole(PluginInstance.Config.UniversalDefaultRole, Exiled.API.Enums.SpawnReason.ForceClass);
                });
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
                        CustomizePlayerPosition(player);
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
                            CustomizePlayerPosition(player, role);
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
                        CustomizePlayerPosition(player, role);
                    });
                    break;
                default:
                    return false;
            }
            return true;
        }

        private void CustomizePlayerPosition(Player player){
            if (!PluginInstance.Config.UniversalCustomSpawn.Equals(Vector3.negativeInfinity))
            {
                Timing.CallDelayed(PluginInstance.Config.UniversalCustomSpawnWaitTime, delegate { player.Position = PluginInstance.Config.UniversalCustomSpawn; });
            }
        }
        private void CustomizePlayerPosition(Player player, RoleType role)
        {
            if (PluginInstance.Config.RespawnRollSpawnPositions.TryGetValue(role, out Vector3 pos))
            {
                Timing.CallDelayed(PluginInstance.Config.UniversalCustomSpawnWaitTime, delegate { player.Position = pos; });
            }
            else if (!PluginInstance.Config.UniversalCustomSpawn.Equals(Vector3.negativeInfinity))
            {
                Timing.CallDelayed(PluginInstance.Config.UniversalCustomSpawnWaitTime, delegate { player.Position = PluginInstance.Config.UniversalCustomSpawn; });
            }
        }

    }
}