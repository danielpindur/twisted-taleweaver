import { RoomSelection } from "@/components/home/RoomSelection";
import { MainLayout } from "@/components/layouts/MainLayout";
import { useNavigate } from "react-router-dom";
import { useClerk, useUser } from "@clerk/clerk-react";
import { LoadingSpinner } from "@/components/ui/spinner";

const Rooms = () => {
  const { signOut } = useClerk();
  const { user, isLoaded } = useUser();

  const navigate = useNavigate();

  const handleJoinRoom = (roomId: string) => {
    navigate(`/room/${roomId}`);
  };

  const handleLogout = () => {
    signOut();
    navigate("/");
  };

  if (!isLoaded) {
    return (<LoadingSpinner />);
  }

  return (
    <MainLayout user={user!} onLogout={handleLogout}>
      <RoomSelection 
        user={user!}
        onJoinRoom={handleJoinRoom}
        onLogout={handleLogout}
      />
    </MainLayout>
  );
};

export default Rooms;