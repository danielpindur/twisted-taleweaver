import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useToast } from "@/hooks/use-toast";
import { Button } from "@/components/ui/button";
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card";
import { Badge } from "@/components/ui/badge";
import { Dialog, DialogContent, DialogDescription, DialogHeader, DialogTitle, DialogTrigger } from "@/components/ui/dialog";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select";
import { Textarea } from "@/components/ui/textarea";
import { Users, Crown, Skull, Plus, LogOut, Swords, KeyRound, Archive, Loader2, AlertCircle } from "lucide-react";
import type { UserResource } from "@clerk/types";

interface Room {
  id: string;
  name: string;
  description: string;
  currentPlayers: number;
  maxPlayers: number;
  status: "waiting" | "in-progress" | "completed";
  difficulty: "easy" | "medium" | "hard" | "nightmare";
}

interface RoomSelectionProps {
  user: UserResource;
  onJoinRoom: (roomId: string) => void;
  onLogout: () => void;
}

// TODO: Replace with actual API data
const mockRooms: Room[] = [
  {
    id: "1",
    name: "The Cursed Catacombs",
    description: "Ancient crypts filled with restless spirits and forgotten treasures. A perfect place for heroes to meet their doom.",
    currentPlayers: 3,
    maxPlayers: 6,
    status: "waiting",
    difficulty: "medium"
  },
  {
    id: "2", 
    name: "Doom's Embrace",
    description: "A foreboding fortress where shadows whisper of past failures. Will you add your name to the list of the fallen?",
    currentPlayers: 4,
    maxPlayers: 4,
    status: "in-progress",
    difficulty: "hard"
  },
  {
    id: "3",
    name: "The Weeping Woods",
    description: "Trees that bleed sorrow and paths that lead nowhere. Even the bravest souls lose their way here.",
    currentPlayers: 2,
    maxPlayers: 5,
    status: "waiting",
    difficulty: "easy"
  },
  {
    id: "4",
    name: "Nightmare's End",
    description: "The final trial where legends are made... or more commonly, where heroes become cautionary tales.",
    currentPlayers: 1,
    maxPlayers: 3,
    status: "completed",
    difficulty: "nightmare"
  }
];

export const RoomSelection = ({ user, onJoinRoom, onLogout }: RoomSelectionProps) => {
  const navigate = useNavigate();
  const { toast } = useToast();
  const [isCreateDialogOpen, setIsCreateDialogOpen] = useState(false);
  const [isJoinDialogOpen, setIsJoinDialogOpen] = useState(false);
  const [isJoining, setIsJoining] = useState(false);
  const [isCreating, setIsCreating] = useState(false);
  const [newRoomName, setNewRoomName] = useState("");
  const [newRoomDescription, setNewRoomDescription] = useState("");
  const [newRoomDifficulty, setNewRoomDifficulty] = useState<Room["difficulty"]>("medium");
  const [maxPlayers, setMaxPlayers] = useState("4");
  const [joinCode, setJoinCode] = useState("");
  const [joinError, setJoinError] = useState("");
  
  const getStatusColor = (status: Room["status"]) => {
    switch (status) {
      case "waiting": return "bg-warning text-background";
      case "in-progress": return "bg-player text-background";
      case "completed": return "bg-secondary text-secondary-foreground";
    }
  };

  const getDifficultyColor = (difficulty: Room["difficulty"]) => {
    switch (difficulty) {
      case "easy": return "bg-health text-background";
      case "medium": return "bg-warning text-background";
      case "hard": return "bg-accent text-accent-foreground";
      case "nightmare": return "bg-destructive text-destructive-foreground";
    }
  };

  // TODO: Implement actual room creation API call
  const handleCreateRoom = async () => {
    setIsCreating(true);
    
    try {
      // Simulate API call delay
      await new Promise(resolve => setTimeout(resolve, 1500));
      
      // No validation needed for name/description since they're optional
      const newRoomId = `room-${Date.now()}`; // TODO: Replace with actual API response
      console.log("Creating room:", { 
        name: newRoomName.trim() || null, // Send null if empty for backend auto-generation
        description: newRoomDescription.trim() || null, 
        maxPlayers, 
        difficulty: newRoomDifficulty 
      });
      
      setIsCreateDialogOpen(false);
      setNewRoomName("");
      setNewRoomDescription("");
      setNewRoomDifficulty("medium");
      setMaxPlayers("4");
      
      toast({
        title: "Expedition Created!",
        description: "Your tale of doom begins...",
      });
      
      // Join the newly created room
      handleJoinRoom(newRoomId);
      
    } catch (error) {
      toast({
        title: "Creation Failed",
        description: "Failed to create expedition. Try again.",
        variant: "destructive",
      });
    } finally {
      setIsCreating(false);
    }
  };

  // TODO: Check if user has character, if not trigger character creation first
  const handleJoinRoom = (roomId: string) => {
    // Navigate directly to the expedition - character creation will happen there
    onJoinRoom(roomId);
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
      handleJoinRoom(joinCode);
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


  // Filter rooms by status and user participation
  const userActiveRooms = mockRooms.filter(room => 
    room.status !== "completed" && room.currentPlayers > 0 // TODO: Replace with actual user participation check
  );
  const completedRooms = mockRooms.filter(room => room.status === "completed");

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
                  <Label htmlFor="room-name" className="font-crimson">Expedition Name (Optional)</Label>
                  <Input
                    id="room-name"
                    placeholder="Leave empty for auto-generated name"
                    value={newRoomName}
                    onChange={(e) => setNewRoomName(e.target.value)}
                    className="bg-input border-border"
                  />
                </div>
                <div className="space-y-2">
                  <Label htmlFor="room-description" className="font-crimson">Description (Optional)</Label>
                  <Textarea
                    id="room-description"
                    placeholder="Leave empty for auto-generated description"
                    value={newRoomDescription}
                    onChange={(e) => setNewRoomDescription(e.target.value)}
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
                      value={maxPlayers}
                      onChange={(e) => setMaxPlayers(e.target.value)}
                      className="bg-input border-border"
                    />
                  </div>
                  <div className="space-y-2">
                    <Label className="font-crimson">Difficulty</Label>
                    <Select value={newRoomDifficulty} onValueChange={(value: Room["difficulty"]) => setNewRoomDifficulty(value)}>
                      <SelectTrigger className="bg-input border-border">
                        <SelectValue />
                      </SelectTrigger>
                      <SelectContent>
                        <SelectItem value="easy">Easy - Child's Play</SelectItem>
                        <SelectItem value="medium">Medium - Standard Doom</SelectItem>
                        <SelectItem value="hard">Hard - Certain Death</SelectItem>
                        <SelectItem value="nightmare">Nightmare - Beyond Despair</SelectItem>
                      </SelectContent>
                    </Select>
                  </div>
                </div>
                <div className="flex gap-2 pt-4">
                  <Button 
                    onClick={handleCreateRoom} 
                    className="flex-1"
                    disabled={isCreating}
                  >
                    {isCreating && <Loader2 className="h-4 w-4 animate-spin mr-2" />}
                    {isCreating ? "Weaving Tale..." : "Begin the Tale"}
                  </Button>
                  <Button 
                    variant="outline" 
                    onClick={() => setIsCreateDialogOpen(false)}
                    className="flex-1"
                    disabled={isCreating}
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
            {userActiveRooms.map((room) => (
              <Card key={room.id} className="bg-card border-border shadow-dark hover:shadow-glow transition-all duration-300">
                <CardHeader>
                  <div className="flex justify-between items-start mb-2">
                    <CardTitle className="font-cinzel text-xl text-primary">{room.name}</CardTitle>
                    <div className="flex flex-col gap-1">
                      <Badge className={getDifficultyColor(room.difficulty)}>
                        {room.difficulty}
                      </Badge>
                    </div>
                  </div>
                  <CardDescription className="font-crimson text-sm">
                    {room.description}
                  </CardDescription>
                </CardHeader>
                <CardContent>
                  <div className="space-y-4">
                    {/* Player Count */}
                    <div className="flex items-center justify-between">
                      <div className="flex items-center gap-2 text-sm">
                        <Users className="h-4 w-4" />
                        <span className="font-crimson">
                          {room.currentPlayers}/{room.maxPlayers} souls
                        </span>
                      </div>
                      <Badge className={getStatusColor(room.status)}>
                        {room.status === "waiting" && "Gathering Victims"}
                        {room.status === "in-progress" && "Doom in Progress"}
                      </Badge>
                    </div>

                    {/* Join Button */}
                    <Button
                      variant="default"
                      className="w-full gap-2"
                      onClick={() => onJoinRoom(room.id)}
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
        {completedRooms.length > 0 && (
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
              {completedRooms.slice(0, 3).map((room) => (
                <Card key={room.id} className="bg-muted/30 border-border/50 opacity-75">
                  <CardHeader>
                    <div className="flex justify-between items-start mb-2">
                      <CardTitle className="font-cinzel text-lg text-muted-foreground">{room.name}</CardTitle>
                      <Badge className={getDifficultyColor(room.difficulty)}>
                        {room.difficulty}
                      </Badge>
                    </div>
                    <CardDescription className="font-crimson text-xs text-muted-foreground">
                      {room.description}
                    </CardDescription>
                  </CardHeader>
                  <CardContent>
                    <div className="space-y-4">
                      <div className="flex items-center justify-between">
                        <div className="flex items-center gap-2 text-sm text-muted-foreground">
                          <Users className="h-4 w-4" />
                          <span className="font-crimson">
                            {room.currentPlayers}/{room.maxPlayers} souls
                          </span>
                        </div>
                        <Badge className={getStatusColor(room.status)}>
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