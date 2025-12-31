import { useRef, useState, useCallback, useLayoutEffect } from "react";
import { Button } from "@/components/ui/button";
import { Loader2, ChevronDown, AlertCircle } from "lucide-react";
import { NarrativeMessage } from "./NarrativeMessage";
import { SystemMessage } from "./SystemMessage";
import { CharacterMessage } from "./CharacterMessage";
import { MonsterMessage } from "./MonsterMessage";
import { MessageType, SenderType, type ChatMessage } from "@/__generated__/twisted-taleweaver-api/types";
import type { ExpeditionViewModel } from "@/viewmodels/ExpeditionViewModel";

export interface ChatMessagesProps {
  messages: ChatMessage[],
  participants: ExpeditionViewModel["participants"],
  loadMoreMessages: () => void,
  isLoadingError: boolean,
  isFetchingMoreMessages: boolean,
  canFetchMoreMessages: boolean
};

export const ChatMessages = ({ messages, participants, loadMoreMessages, isLoadingError, isFetchingMoreMessages, canFetchMoreMessages }: ChatMessagesProps) => {
  const messagesEndRef = useRef<HTMLDivElement>(null);
  const scrollAreaRef = useRef<HTMLDivElement>(null);
  
  const [shouldScrollToBottom, setShouldScrollToBottom] = useState(true);
  const [hasMounted, setHasMounted] = useState(false);
  const isFirstLoad = !hasMounted && messages.length > 0;

  useLayoutEffect(() => {
    if (messagesEndRef.current && scrollAreaRef.current && isFirstLoad) {
      scrollAreaRef.current.scrollTop = scrollAreaRef.current.scrollHeight;
      setHasMounted(true);
    }
  }, [messages, isFirstLoad]);

  const handleScroll = useCallback((event: any) => {
    const scrollTop = event.target.scrollTop;
    const scrollHeight = event.target.scrollHeight;
    const clientHeight = event.target.clientHeight;
    
    // Check if user is at the bottom (within 100px)
    const isAtBottom = scrollHeight - scrollTop - clientHeight < 100;
    setShouldScrollToBottom(isAtBottom);
    
    // Load more messages when scrolled to top
    if (scrollTop === 0 && !isFetchingMoreMessages && canFetchMoreMessages) {
      loadMoreMessages();
    }
  }, [isFetchingMoreMessages, canFetchMoreMessages, loadMoreMessages]);

  const scrollToBottom = () => {
    messagesEndRef.current?.scrollIntoView({ behavior: "smooth" });
    setShouldScrollToBottom(true);
  };

  const getMessageDisplay = (message: ChatMessage) => {
    if (!message.sender){
      if (message.type == MessageType.Narrative) {
        return <NarrativeMessage message={message} />
      }
      else {
        return <SystemMessage message={message} />
      }
    }
    else if (message.sender.senderType === SenderType.Character) {
      return <CharacterMessage message={message} participants={participants} />
    }
    else {
      return <MonsterMessage message={message} />
    }
  };

  if (messages.length === 0 && isFetchingMoreMessages) {
    return (
      <div className="col-span-full flex justify-center items-center h-32">
        <Loader2 className="h-8 w-8 animate-spin text-primary" />
      </div>
    )
  }
  
  return (
    <div className="flex-1 relative flex flex-col overflow-hidden">
      <div 
        className="flex-1 overflow-y-auto"
        ref={scrollAreaRef}
        onScroll={handleScroll}
      >
        <div className="space-y-4 px-4 py-4">
          {isLoadingError && (
            <div className="col-span-full flex justify-center items-center text-center text-red-500 font-crimson">
              <AlertCircle className="inline-block h-5 w-5 mr-1" />
              Failed to load messages. Try refreshing the page.
            </div>
          )}

          {/* Load more indicator */}
          {messages.length > 0 && isFetchingMoreMessages && (
            <div className="text-center py-2">
              <div className="flex items-center justify-center gap-2 text-muted-foreground">
                <div className="w-4 h-4 border-2 border-muted-foreground border-t-transparent rounded-full animate-spin"></div>
                <span className="text-sm font-crimson">Loading older messages...</span>
              </div>
            </div>
          )}
          
          {messages.map((message) => (
            <div key={message.id} className="space-y-2">
              {getMessageDisplay(message)}
            </div>
          ))}
          <div ref={messagesEndRef} />
        </div>
      </div>
      
      {/* Scroll to bottom button */}
      {!shouldScrollToBottom && (
        <Button
          onClick={scrollToBottom}
          className="absolute bottom-4 right-4 rounded-full h-10 w-10 p-0 shadow-lg bg-primary hover:bg-primary/90 text-primary-foreground z-10"
          size="icon"
        >
          <ChevronDown className="h-4 w-4" />
        </Button>
      )}
    </div>
  ); 
};