import { TopNavigation } from "@/components/navigations/TopNavigation";
import type { UserResource } from "@clerk/types";

interface MainLayoutProps {
  user: UserResource;
  onLogout: () => void;
  children: React.ReactNode;
}

export const MainLayout = ({ user, onLogout, children }: MainLayoutProps) => {
  return (
    <div className="min-h-screen bg-background">
      <TopNavigation user={user} onLogout={onLogout} />
      <main className="pb-6">
        {children}
      </main>
    </div>
  );
};