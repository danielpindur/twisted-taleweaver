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
import { TextArea } from "@/components/ui/textarea";
import { Users, Plus, Swords, KeyRound, Loader2, AlertCircle } from "lucide-react";
import { useGraphQlClient } from "@/hooks/useGraphQlClient";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { ExpeditionDifficulty, ExpeditionStatus } from "@/__generated__/twisted-taleweaver-api/types";
import { Pagination, PaginationContent, PaginationItem, PaginationLink, PaginationNext, PaginationPrevious } from "@/components/ui/pagination";
import { getDifficultyColor, getStatusColor } from "@/utils/ExpeditionUtils";
import { extractGraphQLErrors, isValidationGraphQLError, normalizeValidationErrors } from "@/utils/GraphQlErrorUtils";
import type { AddExpeditionInput, ExpeditionSearchInput, JoinExpeditionInput } from "@/__generated__/twisted-taleweaver-api/types";
import type { ValidationGraphQLErrorExtensions } from "@/graphql/errors/graphQlError";

export const ExpeditionSelection = () => {
  const navigate = useNavigate();
  const { requestWithAuth } = useGraphQlClient();
  const { toast } = useToast();
  const queryClient = useQueryClient();

  const [isCreateDialogOpen, setIsCreateDialogOpen] = useState(false);
  const [isJoinDialogOpen, setIsJoinDialogOpen] = useState(false);
  const [currentPage, setCurrentPage] = useState(1);
  const pageSize = 6;
    
  const [newExpeditionName, setNewExpeditionName] = useState("");
  const [newExpeditionDescription, setNewExpeditionDescription] = useState("");
  const [newExpeditionDifficulty, setNewExpeditionDifficulty] = useState<ExpeditionDifficulty>(ExpeditionDifficulty.Medium);
  const [newMinParticipants, setNewMinParticipants] = useState(4);
  const [newMaxParticipants, setNewMaxParticipants] = useState(4);
  
  const [joinCode, setJoinCode] = useState("");
  const [joinError, setJoinError] = useState("");
  
  const createExpeditionMutation = useMutation({
    mutationFn: (input: AddExpeditionInput) =>
      requestWithAuth((sdk) => sdk.AddExpedition({ input})),

    onSuccess: (data) => {
      queryClient.invalidateQueries({ queryKey: ["expeditions"] });

      toast({
        title: "Expedition Created!",
        description: "Your tale of doom begins...",
      });

      setIsCreateDialogOpen(false);
      setNewExpeditionName("");
      setNewExpeditionDescription("");
      setNewExpeditionDifficulty(ExpeditionDifficulty.Medium);
      setNewMinParticipants(4);
      setNewMaxParticipants(4);

      navigateToExpedition(data.addExpedition.id);
    },

    onError: (_) => {
      toast({
        title: "Creation Failed",
        description: "Failed to create expedition. Try again.",
        variant: "destructive",
      });
    },
  });

  const joinExpeditionMutation = useMutation({
    mutationFn: (input: JoinExpeditionInput) => 
      requestWithAuth((sdk) => sdk.JoinExpedition({ input })),

    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["expeditions"] });

      toast({
        title: "Expedition Joined!",
        description: "Successfully joined expedition.",
      });

      navigateToExpedition(joinCode);
      setIsJoinDialogOpen(false);
      setJoinCode("");
      setJoinError("");
    },
    onError: (err) => {
      const gqlErrors = extractGraphQLErrors(err);

      for (const gqlError of gqlErrors) {
        if (isValidationGraphQLError(gqlError)) {
          handleJoinValidationErrors(gqlError.extensions!);
          return;
        }
      }

      setJoinError("An unexpected error has occured.");

      toast({
        title: "Failed to Join",
        description: "Failed to join the expedition. Try again.",
        variant: "destructive",
      });
    }
  })

  const handleJoinValidationErrors = (validationErrorExtensions: ValidationGraphQLErrorExtensions) => {
    const normalizedErrorText = normalizeValidationErrors(validationErrorExtensions);
    setJoinError(normalizedErrorText);
  }

  const navigateToExpedition = (expeditionId: string) => {
    navigate("/expedition/" + expeditionId);
  };

  // TODO: Handle long name and description display properly

  const handleCreateExpedition = () => {
    const input: AddExpeditionInput = {
      name: newExpeditionName || undefined,
      description: newExpeditionDescription || undefined,
      difficulty: newExpeditionDifficulty,
      minParticipants: newMinParticipants,
      maxParticipants: newMaxParticipants,
    };

    createExpeditionMutation.mutate(input);
  };

  const handleJoinExpedition = () => {
    if (!joinCode.trim()) {
      return;
    }

    const guidPattern = /^[0-9a-f]{8}-[0-9a-f]{4}-[1-5][0-9a-f]{3}-[89ab][0-9a-f]{3}-[0-9a-f]{12}$/i;
    if (!guidPattern.test(joinCode)) {
      setJoinError("Invalid expedition code format. Expected GUID format.");
      return;
    }

    const input: JoinExpeditionInput = {
      expeditionId: joinCode,
    };

    joinExpeditionMutation.mutate(input)
  };

  const { data, isLoading, isError } = useQuery({
    queryKey: ["expeditions", currentPage, pageSize],
    queryFn: () => {
      const input: ExpeditionSearchInput = {
        page: currentPage,
        pageSize: pageSize,
        statuses: [ExpeditionStatus.Created, ExpeditionStatus.Started],
      };
      return requestWithAuth((sdk) => sdk.GetExpeditions({ input }));
    },
  });

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
                    maxLength={100}
                    onChange={(e) => setNewExpeditionName(e.target.value)}
                    className="bg-input border-border"
                  />
                </div>
                <div className="space-y-2">
                  <Label htmlFor="expedition-description" className="font-crimson">Description (Optional)</Label>
                  <TextArea
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
                    <Label htmlFor="min-players" className="font-crimson">Minimum Souls</Label>
                    <Input
                      id="min-players"
                      type="number"
                      min="2"
                      max="10"
                      value={newMinParticipants}
                      onChange={(e) => setNewMinParticipants(Number(e.target.value))}
                      className="bg-input border-border"
                    />
                  </div>
                  <div className="space-y-2">
                    <Label htmlFor="max-players" className="font-crimson">Maximum Souls</Label>
                    <Input
                      id="max-players"
                      type="number"
                      min="2"
                      max="10"
                      value={newMaxParticipants}
                      onChange={(e) => setNewMaxParticipants(Number(e.target.value))}
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
                        <SelectItem value={ExpeditionDifficulty.Easy}>Easy - Child's Play</SelectItem>
                        <SelectItem value={ExpeditionDifficulty.Medium}>Medium - Standard Doom</SelectItem>
                        <SelectItem value={ExpeditionDifficulty.Hard}>Hard - Certain Death</SelectItem>
                        <SelectItem value={ExpeditionDifficulty.Nightmare}>Nightmare - Beyond Despair</SelectItem>
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
                    disabled={joinExpeditionMutation.isPending}
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
                    onClick={handleJoinExpedition} 
                    className="flex-1" 
                    disabled={!joinCode.trim() || joinExpeditionMutation.isPending}
                  >
                    {joinExpeditionMutation.isPending && <Loader2 className="h-4 w-4 animate-spin mr-2" />}
                    {joinExpeditionMutation.isPending ? "Entering..." : "Enter the Void"}
                  </Button>
                  <Button 
                    variant="outline" 
                    onClick={() => {
                      setIsJoinDialogOpen(false);
                      setJoinError("");
                      setJoinCode("");
                    }}
                    className="flex-1"
                    disabled={joinExpeditionMutation.isPending}
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
            {isLoading && (
              <div className="col-span-full flex justify-center items-center h-32">
                <Loader2 className="h-8 w-8 animate-spin text-primary" />
              </div>
            )}
            {isError && (
              <div className="col-span-full flex justify-center items-center text-center text-red-500 font-crimson">
                <AlertCircle className="inline-block h-5 w-5 mr-1" />
                Failed to load expeditions. Try refreshing the page.
              </div>
            )}
            {!isLoading && !isError && data?.expeditions.items.length === 0 && (
              <p className="col-span-full flex justify-center items-center text-muted-foreground text-center font-crimson italic">
                No active expeditions found. Dare to create a new one?
              </p>
            )}

            {!isError && data?.expeditions.items.map((expedition) => (
              <Card key={expedition.id} className="bg-card border-border shadow-dark hover:shadow-glow transition-all duration-300 flex flex-col">
                <CardHeader className="mb-2">
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
                
                {/* This will stick to the bottom */}
                <CardContent className="mt-auto">
                  <div className="space-y-4">
                    {/* Player Count */}
                    <div className="flex items-center justify-between">
                      <div className="flex items-center gap-2 text-sm">
                        <Users className="h-4 w-4" />
                        <span className="font-crimson">
                          {expedition.currentParticipants}/{expedition.maxParticipants} souls
                        </span>
                      </div>

                      <Badge className={getStatusColor(expedition.state.status)}>
                        {expedition.state.status === ExpeditionStatus.Created && "Gathering Victims"}
                        {expedition.state.status === ExpeditionStatus.Started && "Doom in Progress"}
                      </Badge>
                    </div>

                    {/* Join Button */}
                    <Button
                      variant="default"
                      className="w-full gap-2"
                      onClick={() => navigateToExpedition(expedition.id)}
                    >
                      <Swords className="h-4 w-4" />
                      Continue Expedition
                    </Button>
                  </div>
                </CardContent>
              </Card>
            ))}

            {/* Pagination for Active Expeditions */}
            {!isLoading && !isError && data && data.expeditions.items.length !== 0 && data.expeditions.totalPages !== 1 && (
              <div className="col-span-full flex justify-center items-center justify-center mt-6">
                <Pagination>
                  <PaginationContent>
                    <PaginationItem>
                      <PaginationPrevious 
                        onClick={() => setCurrentPage(Math.max(1, currentPage - 1))}
                        className={currentPage === 1 ? "pointer-events-none opacity-50" : "cursor-pointer"}
                      />
                    </PaginationItem>
                    {Array.from({ length: data.expeditions.totalPages }, (_, i) => i + 1).map((page) => (
                      <PaginationItem key={page}>
                        <PaginationLink
                          onClick={() => setCurrentPage(page)}
                          isActive={currentPage === page}
                          className="cursor-pointer"
                        >
                          {page}
                        </PaginationLink>
                      </PaginationItem>
                    ))}
                    <PaginationItem>
                      <PaginationNext 
                        onClick={() => setCurrentPage(Math.min(data?.expeditions.totalPages, currentPage + 1))}
                        className={currentPage === data?.expeditions.totalPages ? "pointer-events-none opacity-50" : "cursor-pointer"}
                      />
                    </PaginationItem>
                  </PaginationContent>
                </Pagination>
              </div>
            )}
          </div>
        </div>
      </div>
    </div>
  );
};