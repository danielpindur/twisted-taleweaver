import { Link } from "react-router-dom";
import { Button } from "@/components/ui/button";
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card";
import { Sword, Shield, Users, Crown, Sparkles } from "lucide-react";
import { useClerk, SignedIn, SignedOut } from "@clerk/clerk-react";

const Landing = () => {
  const { openSignIn } = useClerk()

  return (
    <div className="min-h-screen bg-gradient-to-br from-background via-background to-muted/20">
      {/* Hero Section */}
      <div className="relative overflow-hidden">
        <div className="absolute inset-0 bg-gradient-to-r from-primary/5 to-secondary/5" />
        
        <div className="relative max-w-6xl mx-auto px-6 py-20">
          <div className="text-center space-y-8">
            {/* Logo and Title */}
            <div className="flex items-center justify-center gap-4 mb-8">
              <Crown className="h-12 w-12 text-primary" />
            <h1 className="text-6xl font-cinzel font-bold text-primary drop-shadow-lg">
              Twisted Taleweaver
            </h1>
              <Sparkles className="h-12 w-12 text-primary drop-shadow-lg" />
            </div>

            {/* Subtitle */}
            <p className="text-2xl font-crimson text-muted-foreground max-w-3xl mx-auto leading-relaxed">
              Embark on epic adventures in a world where every decision shapes your destiny. 
              Create heroes, explore treacherous dungeons, and forge legends with friends.
            </p>

            {/* CTA Buttons */}
            <div className="flex justify-center pt-8">
              <SignedOut>
                <Button
                  asChild
                  size="lg"
                  className="gap-2 font-crimson text-lg px-8 py-4"
                  onClick={() => openSignIn({
                    afterSignInUrl: "/rooms",
                    afterSignUpUrl: "/rooms",
                  })}>
                  <Link to="#">
                    <Sword className="h-5 w-5" />
                    Begin Your Quest
                  </Link>
                </Button>
              </SignedOut>
              <SignedIn>
                <Button asChild size="lg" className="gap-2 font-crimson text-lg px-8 py-4" onClick={() => openSignIn()}>
                  <Link to="/rooms">
                    <Sword className="h-5 w-5" />
                    Explore the Realms
                  </Link>
                </Button>
              </SignedIn>
            </div>
          </div>
        </div>
      </div>

      {/* Features Section */}
      <div className="max-w-6xl mx-auto px-6 py-8">
        <h2 className="text-4xl font-cinzel font-bold text-center mb-12 text-primary">
          Features of the Realm
        </h2>
        
        <div className="grid md:grid-cols-3 gap-8">
          <Card className="bg-card/50 backdrop-blur-sm border-border/50">
            <CardHeader className="text-center">
              <Users className="h-12 w-12 text-primary mx-auto mb-4" />
              <CardTitle className="font-cinzel">Party Adventures</CardTitle>
            </CardHeader>
            <CardContent>
              <CardDescription className="font-crimson text-center">
                Team up with fellow adventurers to tackle challenging expeditions and share the glory of victory.
              </CardDescription>
            </CardContent>
          </Card>

          <Card className="bg-card/50 backdrop-blur-sm border-border/50">
            <CardHeader className="text-center">
              <Sword className="h-12 w-12 text-primary mx-auto mb-4" />
              <CardTitle className="font-cinzel">Turn-Based Combat</CardTitle>
            </CardHeader>
            <CardContent>
              <CardDescription className="font-crimson text-center">
                Engage in strategic battles where every action matters. Plan your moves carefully to emerge victorious.
              </CardDescription>
            </CardContent>
          </Card>

          <Card className="bg-card/50 backdrop-blur-sm border-border/50">
            <CardHeader className="text-center">
              <Shield className="h-12 w-12 text-primary mx-auto mb-4" />
              <CardTitle className="font-cinzel">Character Classes</CardTitle>
            </CardHeader>
            <CardContent>
              <CardDescription className="font-crimson text-center">
                Choose from diverse classes, each with unique abilities and playstyles to master.
              </CardDescription>
            </CardContent>
          </Card>
        </div>
      </div>

      {/* Footer */}
      <footer className="border-t border-border/50 bg-muted/20 py-8">
        <div className="max-w-6xl mx-auto px-6 text-center">
          <p className="text-muted-foreground font-crimson">
            © 2025 Twisted Taleweaver. All rights reserved.
          </p>
        </div>
      </footer>
    </div>
  );
};

export default Landing;