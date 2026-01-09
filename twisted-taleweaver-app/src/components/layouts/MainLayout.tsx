import { TopNavigation } from "@/components/navigations/TopNavigation";
import type { UserResource } from "@clerk/types";
import { FooterQuote } from "@/components/ui/footerquote";

interface MainLayoutProps {
  user: UserResource;
  onLogout: () => void;
  footerQuote?: string;
  children: React.ReactNode;
}

export const MainLayout = ({ user, onLogout, footerQuote, children }: MainLayoutProps) => {
  return (
    <div className="min-h-screen bg-background flex flex-col">
      <TopNavigation user={user} onLogout={onLogout} />
      
      <main className="flex-1 pb-6">
        {children}
      </main>

      {footerQuote && footerQuote.length > 0 && (
        <FooterQuote footerQuote={footerQuote} />
      )}
    </div>
  );
};