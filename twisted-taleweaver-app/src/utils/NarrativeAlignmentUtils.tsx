import { NarrativeAlignment } from "@/__generated__/twisted-taleweaver-api/types";

export const getDisplayString = (alignment: NarrativeAlignment): string => {
    switch (alignment) {
        case NarrativeAlignment.LawfulGood:
            return "LAWFUL GOOD";
        case NarrativeAlignment.NeutralGood:
            return "NEUTRAL GOOD";
        case NarrativeAlignment.ChaoticGood:
            return "CHAOTIC GOOD";
        case NarrativeAlignment.LawfulNeutral:
            return "LAWFUL NEUTRAL";
        case NarrativeAlignment.TrueNeutral:
            return "TRUE NEUTRAL";
        case NarrativeAlignment.ChaoticNeutral:
            return "CHAOTIC NEUTRAL";
        case NarrativeAlignment.LawfulEvil:
            return "LAWFUL EVIL";
        case NarrativeAlignment.NeutralEvil:
            return "NEUTRAL EVIL";
        case NarrativeAlignment.ChaoticEvil:
            return "CHAOTIC EVIL";
        case NarrativeAlignment.Unknowable:
            return "UNKNOWABLE";
        case NarrativeAlignment.Twisted:
            return "TWISTED";
        default:
            return "UNKNOWN";
    }
};
