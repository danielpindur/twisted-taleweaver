import { ExpeditionSelection } from "@/components/home/ExpeditionSelection";
import { MainLayout } from "@/components/layouts/MainLayout";
import { useNavigate } from "react-router-dom";
import { useClerk, useUser } from "@clerk/clerk-react";
import { LoadingSpinner } from "@/components/ui/spinner";

const Dashboard = () => {
  const { signOut } = useClerk();
  const { user, isLoaded } = useUser();

  const navigate = useNavigate();

  const handleLogout = () => {
    signOut();
    navigate("/");
  };

  if (!isLoaded) {
    return (<LoadingSpinner />);
  }

  return (
    <MainLayout user={user!} onLogout={handleLogout}>
      <ExpeditionSelection />
    </MainLayout>
  );
};

export default Dashboard;