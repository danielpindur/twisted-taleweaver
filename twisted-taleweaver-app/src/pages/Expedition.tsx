import { useParams, useNavigate } from "react-router-dom";
import { ChatRoom } from "@/components/chat/ChatRoom";
import { useUser } from "@clerk/clerk-react";
import { Loader2 } from "lucide-react";

const Expedition = () => {
  const { expeditionId } = useParams<{ expeditionId: string }>();
  const navigate = useNavigate();
  const { user, isLoaded } = useUser();

  if (!isLoaded) {
    return (
      <div className="flex justify-center items-center h-32">
       <Loader2 className="h-8 w-8 animate-spin text-primary" />
      </div>);
  }

  const handleLeaveRoom = () => {
    navigate("/");
  };

  if (!expeditionId) {
    navigate("/");
    return null;
  }

  return (
    <ChatRoom
      expeditionId={expeditionId}
      user={user!}
      onLeaveRoom={handleLeaveRoom}
    />
  );
};

export default Expedition;