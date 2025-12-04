import { Link, useLocation } from "react-router-dom";
import { Button } from "@/components/ui/button";
import { Home, Archive, LogOut } from "lucide-react";
import { cn } from "@/lib/utils";
import type { UserResource } from "@clerk/types";

interface TopNavigationProps {
  user: UserResource;
  onLogout: () => void;
}

export const TopNavigation = ({ user, onLogout }: TopNavigationProps) => {
  const location = useLocation();

  const isActive = (path: string) => location.pathname === path;

  return (
    <nav className="sticky top-0 z-50 bg-background/95 backdrop-blur-sm border-b border-border">
      <div className="max-w-6xl mx-auto px-6 py-4">
        <div className="flex justify-between items-center">
          {/* Logo and Title */}
          <div className="flex items-center gap-6">
            <h1 className="text-2xl font-cinzel font-bold text-primary">
              Twisted Taleweaver
            </h1>
            
            {/* Navigation Links */}
            <div className="flex gap-2">
              <Button
                asChild
                variant={isActive("/rooms") ? "default" : "ghost"}
                className={cn(
                  "gap-2 font-crimson"
                )}
              >
                <Link to="/rooms">
                  <Home className="h-4 w-4" />
                  Rooms
                </Link>
              </Button>
              
              <Button
                asChild
                variant={isActive("/archives") ? "default" : "ghost"}
                className={cn(
                  "gap-2 font-crimson"
                )}
              >
                <Link to="/archives">
                  <Archive className="h-4 w-4" />
                  Archives of Fallen Heroes
                </Link>
              </Button>
            </div>
          </div>

          {/* User Info and Logout */}
          <div className="flex items-center gap-4">
            <span className="text-muted-foreground font-crimson">
              Welcome, <span className="text-primary font-medium">{user.username}</span>
            </span>
            <Button variant="outline" onClick={onLogout} className="gap-2 font-crimson">
              <LogOut className="h-4 w-4" />
              Flee the Realm
            </Button>
          </div>
        </div>
      </div>
    </nav>
  );
};