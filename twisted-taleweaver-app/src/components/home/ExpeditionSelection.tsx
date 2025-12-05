import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useToast } from "@/hooks/useToast";
import { Button } from "@/components/ui/button";
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card";
import { Badge } from "@/components/ui/badge";
import { Dialog, DialogContent, DialogDescription, DialogHeader, DialogTitle, DialogTrigger } from "@/components/ui/dialog";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select";
import { Textarea } from "@/components/ui/textarea";
import { Users, Skull, Plus, Swords, KeyRound, Archive, Loader2, AlertCircle } from "lucide-react";
import { useApiClient } from "@/hooks/useApiClient";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { ExpeditionsService } from "@/generated/twisted-taleweaver-api";
import type { ExpeditionRequest } from "@/generated/twisted-taleweaver-api/models/ExpeditionRequest";
import type { Expedition } from "@/generated/twisted-taleweaver-api/models/Expedition";
import { ExpeditionStatus } from "@/generated/twisted-taleweaver-api/models/ExpeditionStatus";
import { ExpeditionDifficulty } from "@/generated/twisted-taleweaver-api/models/ExpeditionDifficulty";

// TODO: Replace with actual API data
const mockExpeditions: Expedition[] = [
  {
    expeditionId: crypto.randomUUID(),
    name: "The Cursed Catacombs",
    description: "Ancient crypts filled with restless spirits and forgotten treasures. A perfect place for heroes to meet their doom.",
    //currentPlayers: 3,
    maxParticipants: 6,
    status: ExpeditionStatus.CREATED,
    difficulty: ExpeditionDifficulty.MEDIUM,
    createdAt: new Date().toISOString(),
    updatedAt: new Date().toISOString(),
  },
  {
    expeditionId: crypto.randomUUID(), 
    name: "Doom's Embrace",
    description: "A foreboding fortress where shadows whisper of past failures. Will you add your name to the list of the fallen?",
    //currentPlayers: 4,
    maxParticipants: 4,
    status: ExpeditionStatus.STARTED,
    difficulty: ExpeditionDifficulty.HARD,
    createdAt: new Date().toISOString(),
    updatedAt: new Date().toISOString(),
  },
  {
    expeditionId: crypto.randomUUID(),
    name: "The Weeping Woods",
    description: "Trees that bleed sorrow and paths that lead nowhere. Even the bravest souls lose their way here.",
    //currentPlayers: 2,
    maxParticipants: 5,
    status: ExpeditionStatus.CREATED,
    difficulty: ExpeditionDifficulty.EASY,
    createdAt: new Date().toISOString(),
    updatedAt: new Date().toISOString(),
  },
  {
    expeditionId: crypto.randomUUID(),
    name: "Nightmare's End",
    description: "The final trial where legends are made... or more commonly, where heroes become cautionary tales.",
    //currentPlayers: 1,
    maxParticipants: 3,
    status: ExpeditionStatus.COMPLETED,
    difficulty: ExpeditionDifficulty.NIGHTMARE,
    createdAt: new Date().toISOString(),
    updatedAt: new Date().toISOString(),
  }
];

export const ExpeditionSelection = () => {
  const navigate = useNavigate();
  const { requestWithAuth } = useApiClient();
  const { toast } = useToast();
  const queryClient = useQueryClient();

  const [isCreateDialogOpen, setIsCreateDialogOpen] = useState(false);
  const [isJoinDialogOpen, setIsJoinDialogOpen] = useState(false);
  
  const [isJoining, setIsJoining] = useState(false);
  
  const [newExpeditionName, setNewExpeditionName] = useState("");
  const [newExpeditionDescription, setNewExpeditionDescription] = useState("");
  const [newExpeditionDifficulty, setNewExpeditionDifficulty] = useState<ExpeditionDifficulty>(ExpeditionDifficulty.MEDIUM);
  const [newMaxParticipants, setNewMaxParticipants] = useState("4");
  
  const [joinCode, setJoinCode] = useState("");
  const [joinError, setJoinError] = useState("");
  
  const createExpeditionMutation = useMutation({
    mutationFn: (request: ExpeditionRequest) =>
      requestWithAuth(() =>
        ExpeditionsService.postApiExpeditions(request)
      ),

    onSuccess: (data) => {
      queryClient.setQueryData(["expedition", data.expeditionId], data);

      toast({
        title: "Expedition Created!",
        description: "Your tale of doom begins...",
      });

      setIsCreateDialogOpen(false);
      setNewExpeditionName("");
      setNewExpeditionDescription("");
      setNewExpeditionDifficulty(ExpeditionDifficulty.MEDIUM);
      setNewMaxParticipants("4");

      navigateToExpedition(data.expeditionId);
    },

    onError: (_) => {
      toast({
        title: "Creation Failed",
        description: "Failed to create expedition. Try again.",
        variant: "destructive",
      });
    },
  });

  const getStatusColor = (status: ExpeditionStatus) => {
    switch (status) {
      case ExpeditionStatus.CREATED: return "bg-warning text-background";
      case ExpeditionStatus.STARTED: return "bg-player text-background";
      case ExpeditionStatus.COMPLETED: return "bg-secondary text-secondary-foreground";
    }
  };

  const getDifficultyColor = (difficulty: ExpeditionDifficulty) => {
    switch (difficulty) {
      case ExpeditionDifficulty.EASY: return "bg-health text-background";
      case ExpeditionDifficulty.MEDIUM: return "bg-warning text-background";
      case ExpeditionDifficulty.HARD: return "bg-accent text-accent-foreground";
      case ExpeditionDifficulty.NIGHTMARE: return "bg-destructive text-destructive-foreground";
    }
  };

  const navigateToExpedition = (expeditionId: string) => {
    navigate("/expedition/" + expeditionId);
  };

  const handleCreateExpedition = () => {
    const request: ExpeditionRequest = {
      name: newExpeditionName || undefined,
      description: newExpeditionDescription || undefined,
      difficulty: newExpeditionDifficulty,
      maxParticipants: Number(newMaxParticipants),
    };

    createExpeditionMutation.mutate(request);
  };

  // TODO: Implement actual join by code API call
  const handleJoinByCode = async () => {
    if (!joinCode.trim()) return;
    
    setIsJoining(true);
    setJoinError("");
    
    try {
      // Simulate API call delay
      await new Promise(resolve => setTimeout(resolve, 1500));
      
      // Validate GUID format
      const guidPattern = /^[0-9a-f]{8}-[0-9a-f]{4}-[1-5][0-9a-f]{3}-[89ab][0-9a-f]{3}-[0-9a-f]{12}$/i;
      if (!guidPattern.test(joinCode)) {
        throw new Error("Invalid expedition code format. Expected GUID format.");
      }
      
      // Mock check if expedition exists (simulate some GUIDs not existing)
      const nonExistentCodes = [
        "12345678-1234-1234-1234-123456789abc",
        "00000000-0000-0000-0000-000000000000",
        "ffffffff-ffff-ffff-ffff-ffffffffffff"
      ];
      if (nonExistentCodes.includes(joinCode)) {
        throw new Error("Expedition not found. The code may be expired or invalid.");
      }
      
      console.log("Joining expedition with code:", joinCode);
      navigateToExpedition(joinCode);
      setIsJoinDialogOpen(false);
      setJoinCode("");
      setJoinError("");
      
      toast({
        title: "Expedition Joined!",
        description: `Successfully joined expedition ${joinCode}`,
      });
      
    } catch (error) {
      const errorMessage = error instanceof Error ? error.message : "Failed to join expedition";
      setJoinError(errorMessage);
      
      toast({
        title: "Failed to Join",
        description: errorMessage,
        variant: "destructive",
      });
    } finally {
      setIsJoining(false);
    }
  };


  // TODO: Why would I be loaded old rooms here?
  // Filter rooms by status and user participation
  const userActiveExpeditions = mockExpeditions.filter(e => 
    e.status !== ExpeditionStatus.COMPLETED //&& room.currentPlayers > 0 // TODO: Replace with actual user participation check
  );
  const completedExpeditions = mockExpeditions.filter(e => e.status === ExpeditionStatus.COMPLETED);

  return (
    <div className="bg-background p-6">
      <div className="max-w-6xl mx-auto">
        {/* Page Header */}
        <div className="mb-8">
          <h1 className="text-4xl font-cinzel font-bold text-primary mb-2">
            Choose Your Doom
          </h1>
          <p className="text-muted-foreground font-crimson text-lg">
            The Taleweaver has prepared several paths to your inevitable demise...
          </p>
        </div>

        {/* Actions */}
        <div className="flex gap-4 mb-8">
          <Dialog open={isCreateDialogOpen} onOpenChange={setIsCreateDialogOpen}>
            <DialogTrigger asChild>
              <Button variant="default" className="gap-2">
                <Plus className="h-4 w-4" />
                Forge New Expedition
              </Button>
            </DialogTrigger>
            <DialogContent className="bg-card border-border">
              <DialogHeader>
                <DialogTitle className="font-cinzel text-xl">Create New Expedition</DialogTitle>
                <DialogDescription className="font-crimson">
                  Dare to create a new tale of woe? The Taleweaver eagerly awaits fresh victims...
                </DialogDescription>
              </DialogHeader>
              <div className="space-y-4 mt-4">
                <div className="space-y-2">
                  <Label htmlFor="expedition-name" className="font-crimson">Expedition Name (Optional)</Label>
                  <Input
                    id="expedition-name"
                    placeholder="Leave empty for auto-generated name"
                    value={newExpeditionName}
                    onChange={(e) => setNewExpeditionName(e.target.value)}
                    className="bg-input border-border"
                  />
                </div>
                <div className="space-y-2">
                  <Label htmlFor="expedition-description" className="font-crimson">Description (Optional)</Label>
                  <Textarea
                    id="expedition-description"
                    placeholder="Leave empty for auto-generated description"
                    value={newExpeditionDescription}
                    onChange={(e) => setNewExpeditionDescription(e.target.value)}
                    className="bg-input border-border min-h-[80px]"
                  />
                  <p className="text-xs text-muted-foreground font-crimson italic">
                    The Taleweaver will craft a fitting name and tale if you prefer to leave these fields empty...
                  </p>
                </div>
                <div className="grid grid-cols-2 gap-4">
                  <div className="space-y-2">
                    <Label htmlFor="max-players" className="font-crimson">Maximum Souls</Label>
                    <Input
                      id="max-players"
                      type="number"
                      min="2"
                      max="8"
                      value={newMaxParticipants}
                      onChange={(e) => setNewMaxParticipants(e.target.value)}
                      className="bg-input border-border"
                    />
                  </div>
                  <div className="space-y-2">
                    <Label className="font-crimson">Difficulty</Label>
                    <Select value={newExpeditionDifficulty} onValueChange={(value: ExpeditionDifficulty) => setNewExpeditionDifficulty(value)}>
                      <SelectTrigger className="bg-input border-border">
                        <SelectValue />
                      </SelectTrigger>
                      <SelectContent>
                        <SelectItem value={ExpeditionDifficulty.EASY}>Easy - Child's Play</SelectItem>
                        <SelectItem value={ExpeditionDifficulty.MEDIUM}>Medium - Standard Doom</SelectItem>
                        <SelectItem value={ExpeditionDifficulty.HARD}>Hard - Certain Death</SelectItem>
                        <SelectItem value={ExpeditionDifficulty.NIGHTMARE}>Nightmare - Beyond Despair</SelectItem>
                      </SelectContent>
                    </Select>
                  </div>
                </div>
                <div className="flex gap-2 pt-4">
                  <Button 
                    onClick={handleCreateExpedition} 
                    className="flex-1"
                    disabled={createExpeditionMutation.isPending}
                  >
                    {createExpeditionMutation.isPending && <Loader2 className="h-4 w-4 animate-spin mr-2" />}
                    {createExpeditionMutation.isPending ? "Weaving Tale..." : "Begin the Tale"}
                  </Button>
                  <Button 
                    variant="outline" 
                    onClick={() => setIsCreateDialogOpen(false)}
                    className="flex-1"
                    disabled={createExpeditionMutation.isPending}
                  >
                    Reconsider
                  </Button>
                </div>
              </div>
            </DialogContent>
          </Dialog>

          <Dialog open={isJoinDialogOpen} onOpenChange={setIsJoinDialogOpen}>
            <DialogTrigger asChild>
              <Button variant="outline" className="gap-2">
                <KeyRound className="h-4 w-4" />
                Join by Code
              </Button>
            </DialogTrigger>
            <DialogContent className="bg-card border-border">
              <DialogHeader>
                <DialogTitle className="font-cinzel text-xl">Join Expedition</DialogTitle>
                <DialogDescription className="font-crimson">
                  Have a secret invitation to doom? Enter the expedition code below...
                </DialogDescription>
              </DialogHeader>
              <div className="space-y-4 mt-4">
                <div className="space-y-2">
                  <Label htmlFor="join-code" className="font-crimson">Expedition Code</Label>
                  <Input
                    id="join-code"
                    placeholder="e.g. 123e4567-e89b-12d3-a456-426614174000"
                    value={joinCode}
                    onChange={(e) => {
                      setJoinCode(e.target.value.toLowerCase());
                      if (joinError) setJoinError("");
                    }}
                    className={`bg-input border-border font-mono ${joinError ? "border-red-500" : ""}`}
                    disabled={isJoining}
                  />
                  {joinError && (
                    <div className="flex items-center gap-1 text-red-500 text-sm">
                      <AlertCircle className="h-4 w-4" />
                      {joinError}
                    </div>
                  )}
                </div>
                <div className="flex gap-2 pt-4">
                  <Button 
                    onClick={handleJoinByCode} 
                    className="flex-1" 
                    disabled={!joinCode.trim() || isJoining}
                  >
                    {isJoining && <Loader2 className="h-4 w-4 animate-spin mr-2" />}
                    {isJoining ? "Entering..." : "Enter the Void"}
                  </Button>
                  <Button 
                    variant="outline" 
                    onClick={() => {
                      setIsJoinDialogOpen(false);
                      setJoinError("");
                      setJoinCode("");
                    }}
                    className="flex-1"
                    disabled={isJoining}
                  >
                    Stay Safe
                  </Button>
                </div>
              </div>
            </DialogContent>
          </Dialog>
        </div>

        {/* My Active Expeditions Section */}
        <div className="mb-8">
          <h2 className="text-2xl font-cinzel font-semibold text-primary mb-4">
            My Active Expeditions
          </h2>
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
            {userActiveExpeditions.map((expedition) => (
              <Card key={expedition.expeditionId} className="bg-card border-border shadow-dark hover:shadow-glow transition-all duration-300">
                <CardHeader>
                  <div className="flex justify-between items-start mb-2">
                    <CardTitle className="font-cinzel text-xl text-primary">{expedition.name}</CardTitle>
                    <div className="flex flex-col gap-1">
                      <Badge className={getDifficultyColor(expedition.difficulty)}>
                        {expedition.difficulty}
                      </Badge>
                    </div>
                  </div>
                  <CardDescription className="font-crimson text-sm">
                    {expedition.description}
                  </CardDescription>
                </CardHeader>
                <CardContent>
                  <div className="space-y-4">
                    {/* Player Count */}
                    <div className="flex items-center justify-between">
                      <div className="flex items-center gap-2 text-sm">
                        <Users className="h-4 w-4" />
                        <span className="font-crimson">
                          {0/*room.currentPlayers*/}/{expedition.maxParticipants} souls
                        </span>
                      </div>
                      <Badge className={getStatusColor(expedition.status)}>
                        {expedition.status === ExpeditionStatus.CREATED && "Gathering Victims"}
                        {expedition.status === ExpeditionStatus.STARTED && "Doom in Progress"}
                      </Badge>
                    </div>

                    {/* Join Button */}
                    <Button
                      variant="default"
                      className="w-full gap-2"
                      onClick={() => navigateToExpedition(expedition.expeditionId)}
                    >
                      <Swords className="h-4 w-4" />
                      Continue Expedition
                    </Button>
                  </div>
                </CardContent>
              </Card>
            ))}
          </div>
        </div>

        {/* Completed Expeditions Section */}
        {completedExpeditions.length > 0 && (
          <div className="mb-8">
            <div className="flex items-center justify-between mb-4">
              <h2 className="text-2xl font-cinzel font-semibold text-muted-foreground">
                Tales of Old
              </h2>
              <Button 
                variant="ghost" 
                className="gap-2 text-muted-foreground"
                onClick={() => navigate("/archives")}
              >
                <Archive className="h-4 w-4" />
                View All Archives
              </Button>
            </div>
            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
              {completedExpeditions.slice(0, 3).map((expedition) => (
                <Card key={expedition.expeditionId} className="bg-muted/30 border-border/50 opacity-75">
                  <CardHeader>
                    <div className="flex justify-between items-start mb-2">
                      <CardTitle className="font-cinzel text-lg text-muted-foreground">{expedition.name}</CardTitle>
                      <Badge className={getDifficultyColor(expedition.difficulty)}>
                        {expedition.difficulty}
                      </Badge>
                    </div>
                    <CardDescription className="font-crimson text-xs text-muted-foreground">
                      {expedition.description}
                    </CardDescription>
                  </CardHeader>
                  <CardContent>
                    <div className="space-y-4">
                      <div className="flex items-center justify-between">
                        <div className="flex items-center gap-2 text-sm text-muted-foreground">
                          <Users className="h-4 w-4" />
                          <span className="font-crimson">
                            {0/*room.currentPlayer*/}/{expedition.maxParticipants} souls
                          </span>
                        </div>
                        <Badge className={getStatusColor(expedition.status)}>
                          Tale Complete
                        </Badge>
                      </div>
                      <Button variant="outline" className="w-full gap-2" disabled>
                        <Skull className="h-4 w-4" />
                        Completed
                      </Button>
                    </div>
                  </CardContent>
                </Card>
              ))}
            </div>
          </div>
        )}

        {/* Footer Quote */}
        <div className="text-center mt-12">
          <p className="text-muted-foreground font-crimson italic text-lg">
            "Choose wisely, for each path leads but to a different shade of ruin..."
          </p>
          <p className="text-primary font-cinzel text-sm mt-2">- The Twisted Taleweaver</p>
        </div>
      </div>
    </div>
  );
};