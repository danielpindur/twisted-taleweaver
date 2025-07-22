using TwistedTaleweaver.Common.Extensions;

namespace TwistedTaleweaver.Expeditions.Helpers;

public static class NarrationHelper
{
    public static string GeneratePrologue()
    { 
        var messages = new[]
        {
            "So... brave little souls have stepped forward. The gate creaks open, and the realm stretches its jaws. Let us begin.",
            "Fools, all of you — but delicious ones. Your expedition is cast, your doom inked. I can hardly wait to turn the page.",
            "The pieces are placed, the blood still warm. You march toward legend... or something far less poetic.",
            "Ah, the scent of courage and poor decisions. Very well — your tale begins where screams echo longest.",
            "The realm accepts your offering. Whether you return whole... is none of my concern.",
            "You chose to walk this path — twisted, cold, and clawed. I merely hold the quill.",
            "Steel, spells, and a spark of hope. Such fragile tools in such a merciless tale.",
            "The expedition stirs. Monsters grin in the dark. I suggest you stop blinking.",
            "A few steps forward, and already the shadows whisper your names. How charming.",
            "You’ve entered the tale, knowingly or not. May your screams be as vivid as your ambitions."
        };

        return messages.Random();
    }

    public static string GenerateSuccessEpilogue()
    {
        var messages = new[]
        {
            "Against every curse and claw, they endured. The expedition survives — for now.",
            "The tale finds its end... and this time, the ending favors the bold.",
            "They return — tattered, bloodied, triumphant. The expedition lives on in whispered awe.",
            "The shadows recede, if only briefly. Victory — bitter, but theirs.",
            "The realm groans, denied its feast. The expedition stands, unbeaten.",
            "A flicker of glory in the dark — they faced the abyss and crawled back alive.",
            "Some return. Fewer speak. But the realm acknowledges their defiance... with interest.",
            "Bloodied but breathing, they slip the realm’s jaws... this time.",
            "The path was cruel, the cost steep — but the expedition claws its way into legend.",
            "Their tale is not over. The realm remembers... and waits."
        };

        return messages.Random();
    }

    public static string GenerateFailureEpilogue()
    {
        var messages = new[]
        {
            "The tale collapses in ruin. None will sing of this expedition — only whisper.",
            "They fought, they fled, they fell. The expedition ends in dust and disgrace.",
            "A misstep, a scream, silence. The expedition fails — utterly.",
            "The realm closes its jaws. The expedition is devoured.",
            "No glory. No survivors worth naming. The expedition lies broken beneath its own ambition.",
            "The darkness took them all. Not even echoes remain.",
            "The page is torn, the names erased. The expedition is no more.",
            "Buried beneath hubris and shadow — not even carrion dared follow.",
            "None return. The realm feeds well tonight.",
            "Their tale ends not with honor... but with hush."
        };

        return messages.Random();
    }

    public static string GenerateMonsterAppearMessage(string monsterName)
    {
        var messages = new[]
        {
            "From the murk steps {0}, stitched of nightmares and malice.",
            "The air bends, and {0} slithers into the tale — eager, starved.",
            "{0} awakens, its presence tearing through the veil like a scream.",
            "The realm exhales, and {0} answers — all teeth and spite.",
            "A shadow takes form — {0}, summoned to twist the tale further.",
            "The earth groans... {0} has arrived, and mercy fled with the sun.",
            "Lurking no longer, {0} now stands — cruel, ancient, amused.",
            "With a hiss and a grin, {0} joins the fray. The tale darkens.",
            "Steel falters and courage wanes as {0} enters the story.",
            "You called for a challenge. The realm sent {0}."
        };

        return messages.Random().Format(monsterName);
    }

    public static string GenerateCharacterHitMessage(string characterName, string monsterName)
    {
        var messages = new[]
        {
            "{0}'s strike lands true - steel bites deep.",
            "A clean hit - {0} draws first blood.",
            "{0} carves through the dark, and {1} recoils.",
            "Blade meets flesh - {0} wounds {1}.",
            "{0} slashes fiercely, and {1} staggers back.",
            "A brutal swing - {0} finds their mark.",
            "The crowd would cheer, if they dared - {0} strikes solidly.",
            "{0}'s weapon sings, and {1} feels the sting.",
            "With grim precision, {0} strikes and pain blooms in {1}.",
            "{0} lunges, and the blow lands where it hurts.",
            "The silence is broken by steel - {0} hits hard.",
            "A flash of motion - {0} cuts through {1}'s defenses.",
            "The tale turns red as {0}'s blow lands.",
            "There’s no mercy in {0} today — and {1} learns that fast.",
            "{0}'s attack tears through shadow and into {1}."
        };

        return  messages.Random().Format(characterName, monsterName);
    }
    
    public static string GenerateCharacterMissedMessage(string characterName, string monsterName)
    {
        var messages = new[]
        {
            "{0} strikes... and the blow finds only shadow.",
            "Steel arcs through the dark, but {0}'s aim betrays them.",
            "{0} lunges — {1} does not even flinch.",
            "A swing, a snarl, and {0} misses their mark.",
            "{0}'s strike sings through the air — to no avail.",
            "With effort and fury, {0} attacks... but {1} steps aside like smoke.",
            "{0} misjudges distance, and the realm laughs silently.",
            "A glorious attempt — and yet {0} hits nothing but mist.",
            "The blade falls wide. {0}'s confidence may not recover.",
            "Darkness dances, and {0}'s strike is swallowed whole.",
            "{0} charges with resolve... and {1} is already gone.",
            "An attack, bold and blind — {0} meets only silence.",
            "{0} swings — {1} watches, amused.",
            "A breath, a blur, and {0}'s strike becomes a ghost story.",
            "{0}'s fury erupts — {1} is untouched, unbothered."
        };

        return messages.Random().Format(characterName, monsterName);
    }

    public static string GenerateCharacterDiesMessage(string characterName, string monsterName)
    {
        var messages = new[]
        {
            "{0} stumbles, choking on their last breath.",
            "The blow is fatal — {0} collapses without a sound.",
            "{1}'s strike finds home, and {0}'s story ends.",
            "Silence claims {0}, as blood pools at their feet.",
            "A final cry... then nothing. {0} has fallen.",
            "There is no mercy. {0} meets their end beneath {1}.",
            "{0}'s fate was sealed — this was the final page.",
            "The shadows embrace {0}, never to let go.",
            "Steel, fang, or claw — it matters not. {0} is dead.",
            "{1}'s wrath leaves {0} cold and still."
        };

        return messages.Random().Format(characterName, monsterName);
    }

    public static string GenerateMonsterHitMessage(string characterName, string monsterName)
    {
        var messages = new[]
        {
            "{1} strikes — {0} cries out in pain.",
            "A savage blow — {0} reels from {1}'s wrath.",
            "{0} is too slow, and {1} tears through them.",
            "The shadows erupt — {1} finds flesh.",
            "No escape — {0} is hit hard.",
            "A blur of fangs and claws — {0} bleeds.",
            "{1} lashes out, and {0} pays the price.",
            "With terrifying grace, {1} cuts into {0}.",
            "{0} falters — the monster does not.",
            "{1} descends, and {0} is caught in the storm.",
            "Pain blooms — {0} cannot stop the blow.",
            "{0} is marked now, a trophy of {1}'s fury.",
            "There is no warning — only agony, as {1} connects.",
            "{0} is hit. The tale darkens.",
            "A cruel impact — {0} staggers beneath it."
        };

        return messages.Random().Format(characterName, monsterName);
    }
    
    public static string GenerateMonsterMissedMessage(string characterName, string monsterName)
    {
        var messages = new[]
        {
            "{1} strikes — but {0} slips just beyond reach.",
            "A lunge, a roar... nothing but empty air as {0} dodges.",
            "{0} moves like a whisper, and {1} misses.",
            "The blow was coming, and still {0} escapes it.",
            "{0} ducks low — {1}'s claws shred only shadow.",
            "{1} miscalculates, and {0} is gone.",
            "{0} watches the strike pass by — calm, untouched.",
            "A narrow miss — {0} owes the realm no thanks.",
            "The beast snaps — {0} isn't there.",
            "Air tears where {0} stood — {1} misses again.",
            "A wide swing — {0} dances away untouched.",
            "The strike is strong. The strike is wasted. {0} lives.",
            "{1}'s fury is real — but {0} denies it blood.",
            "{0} sees the future and steps aside.",
            "This time, {1} fails — and {0} still stands."
        };

        return messages.Random().Format(characterName, monsterName);
    }
    
    public static string GenerateMonsterDiesMessage(string characterName, string monsterName)
    {
        var messages = new[]
        {
            "{1} lets out a final screech before crumbling into dust.",
            "The enemy falls — {0} stands victorious in the silence.",
            "With a shudder, {1} collapses. The realm grows quieter.",
            "{0}'s strike ends it — the threat lies still.",
            "No more screams. No more breath. {1} is no more.",
            "The creature thrashes... then stills. {0} watches it die.",
            "{1} falters, shrieks... and fades into the void.",
            "A killing blow — {0} delivers the end with no mercy.",
            "Darkness flees with the fall of {1}.",
            "The battle turns — {0} has slain their foe."
        };

        return messages.Random().Format(characterName, monsterName);
    }
}