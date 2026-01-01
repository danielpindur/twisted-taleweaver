using System.ComponentModel;

namespace TwistedTaleweaver.Integration.OpenAi.Expeditions.Entities.Enums;

internal enum ExpeditionSetting
{
    // Natural Environments
    [Description("Whispering Pines")] WhisperingPines,
    [Description("Frozen Tundra")] FrozenTundra,
    [Description("Shattered Glacier")] ShatteredGlacier,
    [Description("Perilous Highlands")] PerilousHighlands,
    [Description("Verdant Labyrinth")] VerdantLabyrinth,
    [Description("Ember Wastes")] EmberWastes,
    [Description("Jade Rainforest")] JadeRainforest,
    [Description("Crystal Caverns")] CrystalCaverns,
    [Description("Mirebound Bog")] MireboundBog,
    [Description("Bleak Steppe")] BleakSteppe,
    [Description("Hollow Canopy")] HollowCanopy,
    [Description("Singing Dunes")] SingingDunes,
    [Description("Stormcarved Coast")] StormcarvedCoast,
    [Description("Midnight Tide")] MidnightTide,

    // Supernatural / Arcane Environments
    [Description("Eldritch Hollow")] EldritchHollow,
    [Description("Twilight Sanctum")] TwilightSanctum,
    [Description("Astral Frontier")] AstralFrontier,
    [Description("Veilshattered Reach")] VeilshatteredReach,
    [Description("Nightmare Marsh")] NightmareMarsh,
    [Description("Wraithwind Valley")] WraithwindValley,
    [Description("Arkhamlike District")] ArkhamlikeDistrict,
    [Description("Bloodroot Expanse")] BloodrootExpanse,
    [Description("Eternum Vaults")] EternumVaults,
    [Description("Nullstone Ravine")] NullstoneRavine,
    [Description("Cinderfall Necropolis")] CinderfallNecropolis,
    [Description("Starlit Catacombs")] StarlitCatacombs,
    [Description("Abyssal Corridors")] AbyssalCorridors,
    [Description("Dreadspire Keep")] DreadspireKeep,
    [Description("Obsidian Fathoms")] ObsidianFathoms,
    [Description("Runeseared Wilds")] RunesearedWilds,
    [Description("Evershifting Maze")] EvershiftingMaze,

    // Cosmic / Sci-Fi
    [Description("Derelict Starship")] DerelictStarship,
    [Description("Orbital Graveyard")] OrbitalGraveyard,
    [Description("Quantum Faultline")] QuantumFaultline,
    [Description("Ion Storm Drifts")] IonStormDrifts,
    [Description("Gravity Well Riftlands")] GravityWellRiftlands,
    [Description("Terraformer Ruins")] TerraformerRuins,
    [Description("Xenolith Fields")] XenolithFields,
    [Description("Cryosleeper Colony")] CryosleeperColony,
    [Description("Event Horizon Outpost")] EventHorizonOutpost,
    [Description("Dyson Husk")] DysonHusk,
    [Description("Nanite Plague Zone")] NanitePlagueZone,
    [Description("Chrono Spill Territory")] ChronoSpillTerritory,

    // Mystical Civilizations
    [Description("Forgotten Empire Ruins")] ForgottenEmpireRuins,
    [Description("Sunken Kingdom")] SunkenKingdom,
    [Description("Gilded Astrarium")] GildedAstrarium,
    [Description("Feywoven Court")] FeywovenCourt,
    [Description("Serpentine Dynasty Vaults")] SerpentineDynastyVaults,
    [Description("Ashen Monastery")] AshenMonastery,
    [Description("Celestial Observatory")] CelestialObservatory,
    [Description("Clockwork Metropolis")] ClockworkMetropolis,
    [Description("Monolith Shrines")] MonolithShrines,
    [Description("Warpfire Bastion")] WarpfireBastion,
    [Description("Rootspire Hollow")] RootspireHollow,

    // Horror / Weird Fiction
    [Description("Corpse Harbor")] CorpseHarbor,
    [Description("Viscera Gardens")] VisceraGardens,
    [Description("Lamenting Hospice")] LamentingHospice,
    [Description("Howling Atrium")] HowlingAtrium,
    [Description("Gloomridden Motel")] GloomriddenMotel,
    [Description("Puppetmaker Quarter")] PuppetmakerQuarter,
    [Description("Tallowlight District")] TallowlightDistrict,
    [Description("The Chalk Mire")] TheChalkMire,
    [Description("The Unending Basement")] TheUnendingBasement,
    [Description("The House That Hates You")] TheHouseThatHatesYou,

    // Abstract / Surreal
    [Description("Nonlinear Staircase")] NonlinearStaircase,
    [Description("Impossible Courtyard")] ImpossibleCourtyard,
    [Description("Escherian Spire")] EscherianSpire,
    [Description("Backwards Sky")] BackwardsSky,
    [Description("Dreamfracture Domain")] DreamfractureDomain,
    [Description("Recursive Village")] RecursiveVillage,
    [Description("Paradox Fields")] ParadoxFields,
    [Description("Fractured Memory Sea")] FracturedMemorySea,
    [Description("Whispermaze Archive")] WhispermazeArchive,
    [Description("Kaleidoscope Frontier")] KaleidoscopeFrontier,
    [Description("Negative City")] NegativeCity
}