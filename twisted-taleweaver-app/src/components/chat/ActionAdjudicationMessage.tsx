import { useState } from "react";
import { formatTime } from "@/utils/TimeUtils";
import { AlertTriangle, ChevronDown } from "lucide-react";
import Markdown from "react-markdown";
import type { ChatMessage } from "@/__generated__/twisted-taleweaver-api/types";

export interface ActionAdjudicationMessageProps {
  message: ChatMessage
}

export const ActionAdjudicationMessage = ({ message }: ActionAdjudicationMessageProps) => {
  const [expanded, setExpanded] = useState(false);

  return (
    <div className="bg-muted/20 p-3 rounded-lg border border-border shadow-sm">
      {/* Header */}
      <button
        type="button"
        onClick={() => setExpanded(v => !v)}
        className="flex w-full items-center gap-2 text-left"
      >
        <AlertTriangle className="h-4 w-4 text-muted-foreground" />

        <span className="font-crimson font-medium text-muted-foreground">
          Action Adjudication
        </span>

        <span className="text-xs text-muted-foreground/70 ml-auto">
          {formatTime(new Date(message.timestamp))}
        </span>

        <ChevronDown
          className={`h-4 w-4 text-muted-foreground transition-transform ${
            expanded ? "rotate-180" : ""
          }`}
        />
      </button>

      {/* Collapsible content */}
      {expanded && (
        <div className="font-crimson text-muted-foreground whitespace-pre-line mt-2 ">
          <Markdown>
            {message.content}
          </Markdown>
        </div>
      )}
    </div>
  );
};
