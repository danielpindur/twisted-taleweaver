import { useRef, useEffect, useState, useCallback } from "react";
import { Button } from "@/components/ui/button";
import { Eye, Skull, BookOpen, ChevronDown } from "lucide-react";
import { formatTime } from "@/utils/TimeUtils";
import type { ChatMessage } from "@/interfaces/ChatMessage";
import type { ExpeditionParticipant } from "@/generated/twisted-taleweaver-api";

export interface ChatMessagesProps {
  messages: ChatMessage[],
  participants: ExpeditionParticipant[],
  onLoadMoreMessages?: () => void,
};

export const ChatMessages = ({ messages, participants, onLoadMoreMessages }: ChatMessagesProps) => {
  const messagesEndRef = useRef<HTMLDivElement>(null);
  const scrollAreaRef = useRef<HTMLDivElement>(null);
  const [displayedMessages, setDisplayedMessages] = useState<ChatMessage[]>([]);
  const [isLoading, setIsLoading] = useState(false);
  const [shouldScrollToBottom, setShouldScrollToBottom] = useState(true);
  const [isInitialLoad, setIsInitialLoad] = useState(true);
  
  // Show only last 50 messages initially
  useEffect(() => {
    const last50 = messages.slice(-50);
    setDisplayedMessages(last50);
  }, [messages]);
  
  // Auto-scroll to bottom only for new messages when already at bottom
  useEffect(() => {
    if (isInitialLoad && messagesEndRef.current && scrollAreaRef.current) {
      // On initial load, set scroll position directly to bottom without animation
      setTimeout(() => {
        if (scrollAreaRef.current) {
          scrollAreaRef.current.scrollTop = scrollAreaRef.current.scrollHeight;
          setIsInitialLoad(false);
        }
      }, 0);
    } else if (shouldScrollToBottom && messagesEndRef.current && !isInitialLoad) {
      // For new messages, smooth scroll
      messagesEndRef.current.scrollIntoView({ behavior: "smooth" });
    }
  }, [displayedMessages, shouldScrollToBottom, isInitialLoad]);

  const handleScroll = useCallback((event: any) => {
    const scrollTop = event.target.scrollTop;
    const scrollHeight = event.target.scrollHeight;
    const clientHeight = event.target.clientHeight;
    
    // Check if user is at the bottom (within 100px)
    const isAtBottom = scrollHeight - scrollTop - clientHeight < 100;
    setShouldScrollToBottom(isAtBottom);
    
    // Load more messages when scrolled to top
    if (scrollTop === 0 && displayedMessages.length < messages.length && onLoadMoreMessages && !isLoading) {
      setIsLoading(true);
      onLoadMoreMessages();
      
      // Reset loading state after a delay (simulating API response time)
      setTimeout(() => {
        setIsLoading(false);
      }, 1000);
    }
  }, [displayedMessages.length, messages.length, onLoadMoreMessages, isLoading]);

  const scrollToBottom = () => {
    messagesEndRef.current?.scrollIntoView({ behavior: "smooth" });
    setShouldScrollToBottom(true);
  };
  
  return (
    <div className="flex-1 relative flex flex-col overflow-hidden">
      <div 
        className="flex-1 overflow-y-auto"
        ref={scrollAreaRef}
        onScroll={handleScroll}
      >
        <div className="space-y-4 px-4 py-4">
          {/* Load more indicator */}
          {displayedMessages.length < messages.length && (
            <div className="text-center py-2">
              {isLoading ? (
                <div className="flex items-center justify-center gap-2 text-muted-foreground">
                  <div className="w-4 h-4 border-2 border-muted-foreground border-t-transparent rounded-full animate-spin"></div>
                  <span className="text-sm font-crimson">Loading older messages...</span>
                </div>
              ) : (
                <Button 
                  variant="ghost" 
                  size="sm" 
                  onClick={() => handleScroll({ target: { scrollTop: 0, scrollHeight: scrollAreaRef.current?.scrollHeight || 0, clientHeight: scrollAreaRef.current?.clientHeight || 0 } })}
                  className="text-muted-foreground hover:text-foreground"
                >
                  Scroll to top to load {Math.min(50, messages.length - displayedMessages.length)} older messages
                </Button>
              )}
            </div>
          )}
          
          {displayedMessages.map((message) => (
            <div key={message.id} className="space-y-2">
              {message.sender === "taleweaver" && (
                <div className="bg-gradient-taleweaver p-4 rounded-lg border border-border shadow-evil">
                  <div className="flex items-center gap-2 mb-2">
                    <BookOpen className="h-4 w-4 text-taleweaver-foreground" />
                    <span className="font-cinzel font-medium text-taleweaver-foreground">
                      The Twisted Taleweaver
                    </span>
                    <span className="text-xs text-taleweaver-foreground/70">
                      {formatTime(message.timestamp)}
                    </span>
                  </div>
                  <p className="font-crimson text-taleweaver-foreground italic">
                    {message.content}
                  </p>
                </div>
              )}
              
              {message.sender === "character" && (
                <div className="bg-Character/10 p-3 rounded-lg border border-Character/20">
                  <div className="flex items-center gap-2 mb-1">
                    {/*(() => {
                      const participant = participants.find(p => p.character?.characterId === message.characterId);
                      const IconComponent = getClassIcon(participant!.character!.class);
                      return <IconComponent className="h-4 w-4 text-Character" />;
                    })()*/}
                    <span className="font-crimson font-medium text-Character">
                      {message.characterName}
                    </span>
                    <span className="text-xs text-muted-foreground">
                      {formatTime(message.timestamp)}
                    </span>
                  </div>
                  <p className="font-crimson">{message.content}</p>
                </div>
              )}
              
              {message.sender === "monster" && (
                <div className="bg-destructive/10 p-3 rounded-lg border border-destructive/20">
                  <div className="flex items-center gap-2 mb-1">
                    <Skull className="h-4 w-4 text-destructive" />
                    <span className="font-crimson font-medium text-destructive">
                      {message.monsterName}
                    </span>
                    <span className="text-xs text-muted-foreground">
                      {formatTime(message.timestamp)}
                    </span>
                  </div>
                  <p className="font-crimson text-destructive/90">{message.content}</p>
                </div>
              )}
              
              {message.sender === "system" && (
                <div className="bg-muted/50 p-2 rounded-md border border-border">
                  <div className="flex items-center gap-2">
                    <Eye className="h-3 w-3 text-muted-foreground" />
                    <span className="text-sm font-crimson text-muted-foreground">
                      {message.content}
                    </span>
                    <span className="text-xs text-muted-foreground ml-auto">
                      {formatTime(message.timestamp)}
                    </span>
                  </div>
                </div>
              )}
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