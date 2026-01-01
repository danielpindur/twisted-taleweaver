using System.ComponentModel;

namespace TwistedTaleweaver.Integration.OpenAi.Expeditions.Entities.Enums;

internal enum ExpeditionObjective
{
    // Classic Adventure Goals
    [Description("Retrieve Artifact")] RetrieveArtifact,
    [Description("Rescue Captive")] RescueCaptive,
    [Description("Slay Beast")] SlayBeast,
    [Description("Escort Caravan")] EscortCaravan,
    [Description("Map Unknown Territory")] MapUnknownTerritory,
    [Description("Secure Supply Route")] SecureSupplyRoute,
    [Description("Scout Enemy Position")] ScoutEnemyPosition,
    [Description("Sabotage Stronghold")] SabotageStronghold,
    [Description("Negotiate Truce")] NegotiateTruce,
    [Description("Deliver Message")] DeliverMessage,
    [Description("Retrieve Lost Caravan")] RetrieveLostCaravan,

    // Arcane & Supernatural
    [Description("Seal Ancient Rift")] SealAncientRift,
    [Description("Break Cursed Bond")] BreakCursedBond,
    [Description("Bind Restless Spirit")] BindRestlessSpirit,
    [Description("Uncover Forbidden Ritual")] UncoverForbiddenRitual,
    [Description("Disrupt Dark Coven")] DisruptDarkCoven,
    [Description("Awaken Dormant Guardian")] AwakenDormantGuardian,
    [Description("Silence Eternal Choir")] SilenceEternalChoir,
    [Description("Quell Phantom Uprising")] QuellPhantomUprising,
    [Description("Banish Outer Entity")] BanishOuterEntity,

    // Investigation & Mystery
    [Description("Track Missing Expedition")] TrackMissingExpedition,
    [Description("Identify Source of Disturbance")] IdentifySourceOfDisturbance,
    [Description("Unravel Historical Conspiracy")] UnravelHistoricalConspiracy,
    [Description("Decode Eldritch Script")] DecodeEldritchScript,
    [Description("Recover Stolen Knowledge")] RecoverStolenKnowledge,
    [Description("Investigate Temporal Anomaly")] InvestigateTemporalAnomaly,
    [Description("Follow Cryptic Prophecy")] FollowCrypticProphecy,
    [Description("Locate Vanished Settlement")] LocateVanishedSettlement,

    // Survival & Extraction
    [Description("Survive Anomaly")] SurviveAnomaly,
    [Description("Evacuate Civilians")] EvacuateCivilians,
    [Description("Escape Collapsing Zone")] EscapeCollapsingZone,
    [Description("Navigate Hazardous Maze")] NavigateHazardousMaze,
    [Description("Recover Vital Resources")] RecoverVitalResources,
    [Description("Extract Stranded Team")] ExtractStrandedTeam,

    // Cosmic / Sci-Fi Objectives
    [Description("Reactivate Terraformer")] ReactivateTerraformer,
    [Description("Stabilize Quantum Core")] StabilizeQuantumCore,
    [Description("Retrieve Cryo Data")] RetrieveCryoData,
    [Description("Purge Nanite Swarm")] PurgeNaniteSwarm,
    [Description("Disable AI Overseer")] DisableAIOverseer,
    [Description("Investigate Ghost Signal")] InvestigateGhostSignal,
    [Description("Repair Orbital Array")] RepairOrbitalArray,
    [Description("Locate Warp Beacon")] LocateWarpBeacon,
    [Description("Contain Biohazard Breach")] ContainBiohazardBreach,
    [Description("Access Memory Vault")] AccessMemoryVault,

    // Horror / Weird Fiction
    [Description("Survive Nightfall")] SurviveNightfall,
    [Description("Cleanse Haunted District")] CleanseHauntedDistrict,
    [Description("Confront House Entity")] ConfrontHouseEntity,
    [Description("Retrieve Lost Dream")] RetrieveLostDream,
    [Description("Dispel Manifested Fear")] DispelManifestedFear,
    [Description("Put Restless Town to Sleep")] PutRestlessTownToSleep,
    [Description("Stop the Clock That Should Not Tick")] StopTheClockThatShouldNotTick,

    // Strange / Surreal (max creativity)
    [Description("Capture Echo of Future")] CaptureEchoOfFuture,
    [Description("Collect Tears of Stone")] CollectTearsOfStone,
    [Description("Bargain with Silent God")] BargainWithSilentGod,
    [Description("Steal Color from the Sky")] StealColorFromTheSky,
    [Description("Reassemble Broken Story")] ReassembleBrokenStory,
    [Description("Unweave Living Tapestry")] UnweaveLivingTapestry,
    [Description("Calm the Weeping Mountain")] CalmTheWeepingMountain,
    [Description("Revive Eternal Season")] ReviveEternalSeason,
    [Description("Solve Impossible Riddle")] SolveImpossibleRiddle,
    [Description("Appease the Door That Thinks")] AppeaseTheDoorThatThinks,
    [Description("Chart Dreamlogic")] ChartDreamlogic,
    [Description("Gather Shards of Memory")] GatherShardsOfMemory,
    [Description("Restore Concept of Hope")] RestoreConceptOfHope,

    // Purely Abstract / Meta Objectives
    [Description("Debug Reality Fold")] DebugRealityFold,
    [Description("Patch Narrative Glitch")] PatchNarrativeGlitch,
    [Description("Stabilize Emotional Topology")] StabilizeEmotionalTopology,
    [Description("Retrieve Lost Chapter")] RetrieveLostChapter,
    [Description("Locate Origin of Rumor")] LocateOriginOfRumor,
    [Description("Collapse False Timeline")] CollapseFalseTimeline
}