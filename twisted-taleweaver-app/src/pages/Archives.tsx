import { ExpeditionArchives } from "@/components/archives/ExpeditionArchives";
import { MainLayout } from "@/components/layouts/MainLayout";
import { useClerk, useUser } from "@clerk/clerk-react";
import { Loader2 } from "lucide-react";
import { useNavigate } from "react-router-dom";

const Archives = () => {
  const { signOut } = useClerk();
  const { user, isLoaded } = useUser();
  const footerQuote = "\"Every ending is but a new beginning... in another's tale of woe.\"";

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
      <ExpeditionArchives />
    </MainLayout>
  );
};

export default Archives;