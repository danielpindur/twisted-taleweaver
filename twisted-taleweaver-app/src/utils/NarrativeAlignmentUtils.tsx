import { NarrativeAlignment } from "@/__generated__/twisted-taleweaver-api/types";

export const getDisplayString = (alignment: NarrativeAlignment): string => {
    switch (alignment) {
        case NarrativeAlignment.LawfulGood:
            return "Lawful Good";
        case NarrativeAlignment.NeutralGood:
            return "Neutral Good";
        case NarrativeAlignment.ChaoticGood:
            return "Chaotic Good";
        case NarrativeAlignment.LawfulNeutral:
            return "Lawful Neutral";
        case NarrativeAlignment.TrueNeutral:
            return "True Neutral";
        case NarrativeAlignment.ChaoticNeutral:
            return "Chaotic Neutral";
        case NarrativeAlignment.LawfulEvil:
            return "Lawful Evil";
        case NarrativeAlignment.NeutralEvil:
            return "Neutral Evil";
        case NarrativeAlignment.ChaoticEvil:
            return "Chaotic Evil";
        case NarrativeAlignment.Unknowable:
            return "Unknowable";
        case NarrativeAlignment.Twisted:
            return "Twisted";
        default:
            return "Unknown";
    };
};