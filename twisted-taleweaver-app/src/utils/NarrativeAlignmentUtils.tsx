import { NarrativeAlignment } from "@/generated/twisted-taleweaver-api/models/NarrativeAlignment";

export const getDisplayString = (alignment: NarrativeAlignment): string => {
    switch (alignment) {
        case NarrativeAlignment.LAWFUL_GOOD:
            return "Lawful Good";
        case NarrativeAlignment.NEUTRAL_GOOD:
            return "Neutral Good";
        case NarrativeAlignment.CHAOTIC_GOOD:
            return "Chaotic Good";
        case NarrativeAlignment.LAWFUL_NEUTRAL:
            return "Lawful Neutral";
        case NarrativeAlignment.TRUE_NEUTRAL:
            return "True Neutral";
        case NarrativeAlignment.CHAOTIC_NEUTRAL:
            return "Chaotic Neutral";
        case NarrativeAlignment.LAWFUL_EVIL:
            return "Lawful Evil";
        case NarrativeAlignment.NEUTRAL_EVIL:
            return "Neutral Evil";
        case NarrativeAlignment.CHAOTIC_EVIL:
            return "Chaotic Evil";
        case NarrativeAlignment.UNKNOWABLE:
            return "Unknowable";
        case NarrativeAlignment.TWISTED:
            return "Twisted";
        default:
            return "Unknown";
    };
};