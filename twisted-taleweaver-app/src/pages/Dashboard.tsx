import { ExpeditionSelection } from "@/components/home/ExpeditionSelection";
import { MainLayout } from "@/components/layouts/MainLayout";
import { useNavigate } from "react-router-dom";
import { useClerk, useUser } from "@clerk/clerk-react";
import { Loader2 } from "lucide-react";

const Dashboard = () => {
  const { signOut } = useClerk();
  const { user, isLoaded } = useUser();
  const footerQuote = "\"Choose wisely, for each path leads but to a different shade of ruin...\"";

  const navigate = useNavigate();

  const handleLogout = () => {
    signOut();
    navigate("/");
  };

  if (!isLoaded) {
    return (
      <div className="flex justify-center items-center h-32">
       <Loader2 className="h-8 w-8 animate-spin text-primary" />
      </div>);
  }

  return (
    <MainLayout user={user!} onLogout={handleLogout} footerQuote={footerQuote}>
      <ExpeditionSelection />
    </MainLayout>
  );
};

export default Dashboard;