import { useState, useEffect } from "react";
import { Button } from "@/components/ui/button";
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card";
import { Badge } from "@/components/ui/badge";
import { Input } from "@/components/ui/input";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select";
import { Pagination, PaginationContent, PaginationItem, PaginationLink, PaginationNext, PaginationPrevious } from "@/components/ui/pagination";
import { Dialog, DialogTrigger } from "@/components/ui/dialog";
import { Search, Users, Skull, Crown, Loader2, AlertCircle, Calendar, Clock } from "lucide-react";
import { ExpeditionDifficulty, ExpeditionOutcome, ExpeditionStatus, type ExpeditionSearchInput, type GetArchivedExpeditionsQuery } from "@/__generated__/twisted-taleweaver-api/types";
import { useQuery } from "@tanstack/react-query";
import { useGraphQlClient } from "@/hooks/useGraphQlClient";
import { getDifficultyColor, getOutcomeColor, getOutcomeIcon } from "@/utils/ExpeditionUtils";
import { getEnumValues } from "@/utils/EnumUtils";
import { formatDuration } from "@/utils/TimeUtils";
import { ExpeditionDetailsDialog } from "./ExpeditionDetailsDialog";

export function ExpeditionArchives() {
  const pageSize = 6;
  const { requestWithAuth } = useGraphQlClient();

  const [searchTerm, setSearchTerm] = useState("");
  const [difficultyFilter, setDifficultyFilter] = useState<ExpeditionDifficulty | "all">("all");
  const [outcomeFilter, setOutcomeFilter] = useState<ExpeditionOutcome | "all">("all");
  const [currentPage, setCurrentPage] = useState(1);
  const [selectedExpeditionId, setSelectedExpedition] = useState<string | null>(null);
  const [dialogOpen, setDialogOpen] = useState(false);

  const { data, isLoading, isError } = useQuery({
    queryKey: ["expeditions-archive", difficultyFilter, outcomeFilter, searchTerm, currentPage, pageSize],
    queryFn: () => {
      const input: ExpeditionSearchInput = {
        page: currentPage,
        pageSize: pageSize,
        statuses: [ExpeditionStatus.Completed],
        difficulty: difficultyFilter === "all" ? undefined : difficultyFilter,
        outcome: outcomeFilter === "all" ? undefined : outcomeFilter,
        searchText: searchTerm.trim() === "" ? undefined : searchTerm.trim(),
      };
      return requestWithAuth((sdk) => sdk.GetArchivedExpeditions({ input }));
    },
  });

  // Reset page when filters change
  useEffect(() => {
    setCurrentPage(1);
  }, [searchTerm, difficultyFilter, outcomeFilter]);

  const countSurvivors = (expedition: GetArchivedExpeditionsQuery["expeditions"]["items"][number]) => {
    let aliveCount = 0;
  
    for (const participant of expedition.participants) {
      const characterId = participant.character?.id;
      if (!characterId) {
        continue;
      }
  
      // find the creature state matching the character
      const creatureState = expedition.state.creatureStates.find(
        cs => cs.creatureId === characterId
      );
  
      if (creatureState!.currentHealth > 0) {
        aliveCount++;
      }
    }
  
    return aliveCount;
  }

  return (
    <div className="bg-background p-6">
      <div className="max-w-6xl mx-auto">
        {/* Header */}
        <div className="mb-8">
          <h1 className="text-4xl font-cinzel font-bold text-primary">
            Archives of Fallen Heroes
          </h1>
          <p className="text-muted-foreground font-crimson text-lg mt-2">
            Tales of glory, defeat, and everything in between... Remember them well.
          </p>
        </div>

        {/* Filters */}
        <div className="grid grid-cols-1 md:grid-cols-3 gap-4 mb-8">
          <div className="relative">
            <Search className="absolute left-3 top-1/2 transform -translate-y-1/2 h-4 w-4 text-muted-foreground" />
            <Input
              placeholder="Search expeditions..."
              value={searchTerm}
              onChange={(e) => setSearchTerm(e.target.value)}
              className="pl-10 bg-input border-border"
            />
          </div>
          <Select value={difficultyFilter ?? ""} onValueChange={(value) => setDifficultyFilter(value as ExpeditionDifficulty | "all")}>
            <SelectTrigger className="bg-input border-border">
              <SelectValue placeholder="Filter by difficulty" />
            </SelectTrigger>
            <SelectContent>
              <SelectItem value="all">All Difficulties</SelectItem>
              {getEnumValues(ExpeditionDifficulty).map((cls) => {
                return (
                  <SelectItem key={cls} value={cls}>{cls}</SelectItem>
                );
              })}
            </SelectContent>
          </Select>
          <Select value={outcomeFilter} onValueChange={(value) => setOutcomeFilter(value as ExpeditionOutcome | "all")}>
            <SelectTrigger className="bg-input border-border">
              <SelectValue placeholder="Filter by outcome" />
            </SelectTrigger>
            <SelectContent>
              <SelectItem value="all">All Outcomes</SelectItem>
               {getEnumValues(ExpeditionOutcome).map((cls) => {
                return (
                  <SelectItem key={cls} value={cls}>{cls}</SelectItem>
                );
              })}
            </SelectContent>
          </Select>
        </div>

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
        {!isError && !isLoading && (
          <div>
            <div className="grid grid-cols-3 gap-4 mb-8">
              <Card className="bg-card border-border">
                <CardContent className="p-4 text-center">
                  <div className="text-2xl font-bold text-primary">{data?.expeditions.statistics.totalExpeditions}</div>
                  <div className="text-sm text-muted-foreground font-crimson">Total Expeditions</div>
                </CardContent>
              </Card>
              <Card className="bg-card border-border">
                <CardContent className="p-4 text-center">
                  <div className="text-2xl font-bold text-health">{data?.expeditions.statistics.successfulExpeditions}</div>
                  <div className="text-sm text-muted-foreground font-crimson">Victories</div>
                </CardContent>
              </Card>
              <Card className="bg-card border-border">
                <CardContent className="p-4 text-center">
                  <div className="text-2xl font-bold text-destructive">{data?.expeditions.statistics.failedExpeditions}</div>
                  <div className="text-sm text-muted-foreground font-crimson">Defeats</div>
                </CardContent>
              </Card>
            </div>

            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6 mb-8">
              {!isLoading && !isError && data?.expeditions.items.length === 0 && (
                <p className="col-span-full flex justify-center items-center text-muted-foreground text-center font-crimson italic">
                  Adjust your search or filters to uncover more stories of the past...
                </p>
              )}
              {data?.expeditions.items.map((expedition) => (
                <Card key={expedition.id} className="bg-muted/30 border-border/50 shadow-dark hover:shadow-glow transition-all duration-300">
                  <CardHeader>
                    <div className="flex justify-between items-start mb-2">
                      <CardTitle className="font-cinzel text-lg text-foreground">{expedition.name}</CardTitle>
                      <div className="flex flex-col gap-1 items-end">
                        <Badge variant="secondary" className={`${getDifficultyColor(expedition.difficulty)} capitalize pointer-events-none`}>
                          {expedition.difficulty}
                        </Badge>
                          <Badge variant="secondary" className={`${getOutcomeColor(expedition.outcome!)} capitalize pointer-events-none whitespace-nowrap`}>
                            {getOutcomeIcon(expedition.outcome!)} {expedition.outcome}
                          </Badge>
                      </div>
                    </div>
                    <CardDescription className="font-crimson text-sm text-muted-foreground">
                      {expedition.description}
                    </CardDescription>
                  </CardHeader>
                  <CardContent>
                    <div className="space-y-3">
                      {/* Participants */}
                      <div className="flex items-center justify-between text-sm">
                        <div className="flex items-center gap-2">
                          <Users className="h-4 w-4 text-muted-foreground" />
                          <span className="font-crimson text-muted-foreground">
                            {expedition.currentParticipants}/{expedition.maxParticipants} souls
                          </span>
                        </div>
                        <div className="flex items-center gap-2">
                          <Crown className="h-4 w-4 text-muted-foreground" />
                          <span className="font-crimson text-muted-foreground">
                            {countSurvivors(expedition)} survived
                          </span>
                        </div>
                      </div>

                      {/* Date and Duration */}
                      <div className="flex items-center justify-between text-sm">
                        <div className="flex items-center gap-2">
                          <Calendar className="h-4 w-4 text-muted-foreground" />
                          <span className="font-crimson text-muted-foreground">
                            {new Date(expedition.completedAt!).toLocaleDateString()}
                          </span>
                        </div>
                        <div className="flex items-center gap-2">
                          <Clock className="h-4 w-4 text-muted-foreground" />
                          <span className="font-crimson text-muted-foreground">
                            {formatDuration(expedition.startedAt!, expedition.completedAt!)}
                          </span>
                        </div>
                      </div>

                      <Dialog>
                        <DialogTrigger asChild>
                          <Button
                            variant="outline"
                            className="w-full gap-2 font-crimson hover:bg-primary hover:text-primary-foreground"
                            onClick={() => {
                              setSelectedExpedition(expedition.id);
                              setDialogOpen(true);
                            }}
                          >
                            <Skull className="h-4 w-4" />
                            Read Tale
                          </Button>
                        </DialogTrigger>
                      </Dialog>
                    </div>
                  </CardContent>
                </Card>
              ))}
            </div>
          </div>
        )}
        

        {/* Pagination */}
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

      <ExpeditionDetailsDialog
        expeditionId={selectedExpeditionId}
        open={dialogOpen}
        onOpenChange={setDialogOpen}
      />
    </div>
  );
};