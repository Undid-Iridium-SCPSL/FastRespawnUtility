
![FastRespawnUtility ISSUES](https://img.shields.io/github/issues/Undid-Iridium/FastRespawnUtility)
![FastRespawnUtility FORKS](https://img.shields.io/github/forks/Undid-Iridium/FastRespawnUtility)
![FastRespawnUtility LICENSE](https://img.shields.io/github/license/Undid-Iridium/FastRespawnUtility)


![FastRespawnUtility LATEST](https://img.shields.io/github/v/release/Undid-Iridium/FastRespawnUtility?include_prereleases&style=flat-square)
![FastRespawnUtilityy LINES](https://img.shields.io/tokei/lines/github/Undid-Iridium/FastRespawnUtility)
![FastRespawnUtility DOWNLOADS](https://img.shields.io/github/downloads/Undid-Iridium/FastRespawnUtility/total?style=flat-square)


# FastRespawnUtility

Ability to control respawn rules. 


## REQUIREMENTS
* Exiled: V5.1.3 minimum
* SCP:SL Server: V11.2

# Example config
```
fast_respawn_utility:
  is_enabled: true
  is_debug_enabled: true
  universal_respawn_timer: 5
  universal_default_role: ChaosMarauder
  respawn_reroll_types:
    FacilityGuard:
      FacilityGuard: 0.75
    NtfPrivate:
      FacilityGuard: 0.75
    NtfSergeant:
      FacilityGuard: 0.75
    NtfCaptain:
      FacilityGuard: 0.75
    NtfSpecialist:
      FacilityGuard: 0.75
    ChaosConscript:
      FacilityGuard: 0.75
    ChaosMarauder:
      FacilityGuard: 0.75
    ChaosRepressor:
      FacilityGuard: 0.75
    ChaosRifleman:
      FacilityGuard: 0.75
    Scp049:
      FacilityGuard: 0.75
    Scp0492:
      FacilityGuard: 0.75
    Scp079:
      FacilityGuard: 0.75
    Scp096:
      FacilityGuard: 0.75
    Scp106:
      FacilityGuard: 0.75
    Scp173:
      FacilityGuard: 0.75
    Scp93953:
      FacilityGuard: 0.75
    Scp93989:
      FacilityGuard: 0.75
    Scientist:
      FacilityGuard: 0.75
    Tutorial:
      FacilityGuard: 0.75
    Spectator:
      FacilityGuard: 0.75
  role_respawn_time:
    FacilityGuard: 5
    NtfPrivate: 5
    NtfSergeant: 5
    NtfCaptain: 5
    NtfSpecialist: 5
    ChaosConscript: 5
    ChaosMarauder: 5
    ChaosRepressor: 5
    ChaosRifleman: 5
    Scp049: 5
    Scp0492: 5
    Scp079: 5
    Scp096: 5
    Scp106: 5
    Scp173: 5
    Scp93953: 5
    Scp93989: 5
    Scientist: 5
    Tutorial: 5
```
