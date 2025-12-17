import { useState } from "react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { TextArea } from "@/components/ui/textarea";
import { Label } from "@/components/ui/label";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select";
import { Dices, Sparkles, Plus, Minus, Loader2 } from "lucide-react";
import { getClassIcon, getGenderSymbol } from "@/utils/CharacterUtils";
import { CharactersService, ClassType, CreatureType, Gender, NarrativeAlignment, type CharacterRequest } from "@/generated/twisted-taleweaver-api";
import { getRandomEnumValue, getEnumValues } from "@/utils/EnumUtils";
import { getDisplayString } from "@/utils/NarrativeAlignmentUtils";
import { Dialog, DialogContent, DialogDescription, DialogHeader, DialogTrigger } from "@/components/ui/dialog";
import { DialogTitle } from "@radix-ui/react-dialog";
import { useToast } from "@/hooks/useToast";
import { useApiClient } from "@/hooks/useApiClient";
import { useMutation, useQueryClient } from "@tanstack/react-query";


export interface CharacterCreationFormProps {
  expeditionId: string;
};

export const CharacterCreationForm = ({expeditionId}: CharacterCreationFormProps) => {
  const queryClient = useQueryClient();
  const { requestWithAuth } = useApiClient();
  const { toast } = useToast();
  const [isDialogOpen, setIsDialogOpen] = useState(true);

  const totalStartingPoints = 75;
  const defaultAttributePoints = 8;
  const minAttributePoints = 1;
  const maxAttributePoints = 20;

  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [characterClass, setCharacterClass] = useState<ClassType>(ClassType.BARBARIAN);
  const [characterGender, setCharacterGender] = useState<Gender>(Gender.MALE);
  const [creatureType, setCreatureType] = useState<CreatureType>(CreatureType.HUMANOID);
  const [narrativeAlignment, setNarrativeAlignment] = useState<NarrativeAlignment>(NarrativeAlignment.UNKNOWN);
  const [characterStrength, setCharacterStrength] = useState(defaultAttributePoints);
  const [characterDexterity, setCharacterDexterity] = useState(defaultAttributePoints);
  const [characterConstitution, setCharacterConstitution] = useState(defaultAttributePoints);
  const [characterIntelligence, setCharacterIntelligence] = useState(defaultAttributePoints);
  const [characterWisdom, setCharacterWisdom] = useState(defaultAttributePoints);
  const [characterCharisma, setCharacterCharisma] = useState(defaultAttributePoints);
  const remainingPoints = totalStartingPoints - characterStrength - characterDexterity - characterConstitution - characterIntelligence - characterWisdom - characterCharisma;

  const createCharacterMutation = useMutation({
    mutationFn: (request: CharacterRequest) =>
      requestWithAuth(() =>
        CharactersService.postApiCharacters(request)
      ),

    onSuccess: (data, request) => {
      queryClient.invalidateQueries({ queryKey: ["expedition", request.expeditionId] });

      toast({
        title: "Character Forged!",
        description: "Another wanderer steps into the abyss...",
      });

      setIsDialogOpen(false);
      
      setName("");
      setDescription("")
      setCharacterClass(ClassType.BARBARIAN);
      setCharacterGender(Gender.MALE);
      setCreatureType(CreatureType.HUMANOID);
      setNarrativeAlignment(NarrativeAlignment.UNKNOWN);
      setCharacterStrength(defaultAttributePoints);
      setCharacterDexterity(defaultAttributePoints);
      setCharacterConstitution(defaultAttributePoints);
      setCharacterIntelligence(defaultAttributePoints);
      setCharacterWisdom(defaultAttributePoints);
      setCharacterCharisma(defaultAttributePoints);
    },

    onError: (_) => {
      toast({
        title: "Creation Failed",
        description: "Failed to create character. Try again.",
        variant: "destructive",
      });
    },
  });

  const randomize = () => {
    setCharacterClass(getRandomEnumValue(ClassType));
    setCharacterGender(getRandomEnumValue(Gender));
    setCreatureType(getRandomEnumValue(CreatureType));
    setNarrativeAlignment(getRandomEnumValue(NarrativeAlignment));

    setCharacterStrength(defaultAttributePoints);
    setCharacterDexterity(defaultAttributePoints);
    setCharacterConstitution(defaultAttributePoints);
    setCharacterIntelligence(defaultAttributePoints);
    setCharacterWisdom(defaultAttributePoints);
    setCharacterCharisma(defaultAttributePoints);

    let pointsLeft = totalStartingPoints - (defaultAttributePoints * 6);
    while (pointsLeft > 0) {
      const attrIndex = Math.floor(Math.random() * 6);
      switch (attrIndex) {
        case 0:
          if (characterStrength < maxAttributePoints) {
            setCharacterStrength(prev => prev + 1);
            pointsLeft--;
          }
          break;
        case 1:
          if (characterDexterity < maxAttributePoints) {
            setCharacterDexterity(prev => prev + 1);
            pointsLeft--;
          }
          break;
        case 2:
          if (characterConstitution < maxAttributePoints) {
            setCharacterConstitution(prev => prev + 1);
            pointsLeft--;
          }
          break;
        case 3:
          if (characterIntelligence < maxAttributePoints) {
            setCharacterIntelligence(prev => prev + 1);
            pointsLeft--;
          }
          break;
        case 4:
          if (characterWisdom < maxAttributePoints) {
            setCharacterWisdom(prev => prev + 1);
            pointsLeft--;
          }
          break;
        case 5:
          if (characterCharisma < maxAttributePoints) {
            setCharacterCharisma(prev => prev + 1);
            pointsLeft--;
          }
          break;
      }
    }
  };

  const canDecrement = (points: number) => {
    return points > minAttributePoints;
  }

  const canIncrement = (points: number) => {
    return points < maxAttributePoints && remainingPoints > 0;
  }

  const handleSubmit = () => {
    const request: CharacterRequest = {
      expeditionId: expeditionId,
      name: name || undefined,
      description: description || undefined,
      class: characterClass,
      gender: characterGender,
      creatureType: creatureType,
      alignment: narrativeAlignment,
      attributeScores: {
        strength: characterStrength,
        dexterity: characterDexterity,
        constitution: characterConstitution,
        intelligence: characterIntelligence,
        wisdom: characterWisdom,
        charisma: characterCharisma
      }
    }
    createCharacterMutation.mutate(request);
  };

  return (
    <Dialog open={isDialogOpen} onOpenChange={setIsDialogOpen}>
      <DialogTrigger asChild>
        <Button variant="default" className="gap-2">
          <Plus className="h-4 w-4" />
          Create Your Character
        </Button>
      </DialogTrigger>
      <DialogContent className="bg-card border-border">
        <DialogHeader>
          <DialogTitle className="font-cinzel text-xl">Create Your Character</DialogTitle>
          <DialogDescription className="font-crimson">Dare to spin a new thread of misery? The Taleweaver hungers for another soul.</DialogDescription>
        </DialogHeader>
        <div className="space-y-4">
          <div className="grid grid-cols-2 gap-4">
            <div className="space-y-1">
              <Label htmlFor="char-name" className="font-crimson">
                Name <span className="text-muted-foreground">(optional)</span>
              </Label>
              <Input
                id="char-name"
                placeholder="Leave blank for random..."
                value={name}
                onChange={(e) => setName(e.target.value)}
                maxLength={50}
                className="bg-input border-border"
              />
            </div>
            <div className="flex justify-end items-end">
              <Button
                variant="outline"
                size="sm"
                onClick={randomize}
              >
                <Dices className="h-3 w-3" />
                Randomize
              </Button>
            </div>
          </div>

          <div className="space-y-1">
            <Label htmlFor="char-desc" className="font-crimson">
              Description <span className="text-muted-foreground">(optional)</span>
            </Label>
            <TextArea
              id="char-desc"
              placeholder="Leave blank for random backstory..."
              value={description}
              onChange={(e) => setDescription(e.target.value)}
              className="bg-input border-border min-h-[80px]"
            />
          </div>

          <div className="grid grid-cols-2 gap-4">
            <div className="space-y-1">
              <Label htmlFor="char-class" className="font-crimson">Class</Label>
              <Select value={characterClass} onValueChange={(value: ClassType) => setCharacterClass(value)}>
                <SelectTrigger className="bg-input border-border">
                  <SelectValue placeholder="Choose a class ..." />
                </SelectTrigger>
                <SelectContent>
                  {getEnumValues(ClassType).map((cls) => {
                    const Icon = getClassIcon(cls);
                    return (
                      <SelectItem key={cls} value={cls}>
                        <div className="flex items-center gap-2">
                          <Icon className="h-3 w-3" />
                          {cls}
                        </div>
                      </SelectItem>
                    );
                  })}
                </SelectContent>
              </Select>
            </div>

            <div className="space-y-1">
              <Label htmlFor="char-gender" className="font-crimson">Gender</Label>
              <Select value={characterGender} onValueChange={(value: Gender) => setCharacterGender(value)}>
                <SelectTrigger className="bg-input border-border">
                  <SelectValue placeholder="Choose a gender ..." />
                </SelectTrigger>
                <SelectContent>
                  {getEnumValues(Gender).map((cls) => {
                    const symbol = getGenderSymbol(cls);
                    return (
                      <SelectItem key={cls} value={cls}>
                        <div className="flex items-center gap-2">
                          {cls} {symbol}
                        </div>
                      </SelectItem>
                    );
                  })}
                </SelectContent>
              </Select>
            </div>

            <div className="space-y-1">
              <Label htmlFor="char-type" className="font-crimson">Creature Type</Label>
              <Select value={creatureType} onValueChange={(value: CreatureType) => setCreatureType(value)}>
                <SelectTrigger className="bg-input border-border">
                  <SelectValue placeholder="Choose a creature type ..." />
                </SelectTrigger>
                <SelectContent>
                  {getEnumValues(CreatureType).map((cls) => {
                    return (
                      <SelectItem key={cls} value={cls}>
                        <div className="flex items-center gap-2">
                          {cls}
                        </div>
                      </SelectItem>
                    );
                  })}
                </SelectContent>
              </Select>
            </div>

            <div className="space-y-1">
              <Label htmlFor="narrative-alignment" className="font-crimson">Narrative Alignment</Label>
              <Select value={narrativeAlignment} onValueChange={(value: NarrativeAlignment) => setNarrativeAlignment(value)}>
                <SelectTrigger className="bg-input border-border">
                  <SelectValue placeholder="Choose a narrative alignment ..." />
                </SelectTrigger>
                <SelectContent>
                  {getEnumValues(NarrativeAlignment).map((cls) => {
                    return (
                      <SelectItem key={cls} value={cls}>
                        <div className="flex items-center gap-2">
                          {getDisplayString(cls)}
                        </div>
                      </SelectItem>
                    );
                  })}
                </SelectContent>
              </Select>
            </div>
          </div>

          {/* Attribute Points */}
          <div className="space-y-2">
            <div className="flex items-center justify-between">
              <Label className="font-crimson">Attributes</Label>
              <span className={`text-xs font-semibold ${remainingPoints === 0 ? 'text-primary' : remainingPoints < 0 ? 'text-destructive' : 'text-muted-foreground'}`}>
                {remainingPoints} points remaining
              </span>
            </div>
            <div className="grid grid-cols-2 gap-2">
              {/* Strength */}
              <div key="strength-attribute" className="flex items-center justify-between bg-background/50 rounded px-2 py-1">
                <span className="text-xs font-crimson w-20">Strength</span>
                <div className="flex items-center gap-1">
                  <Button
                    variant="ghost"
                    size="icon"
                    className="h-6 w-6"
                    onClick={() => setCharacterStrength(characterStrength - 1)}
                    disabled={!canDecrement(characterStrength)}
                  >
                    <Minus className="h-3 w-3" />
                  </Button>
                  <span className="w-8 text-center text-sm font-semibold">{characterStrength}</span>
                  <Button
                    variant="ghost"
                    size="icon"
                    className="h-6 w-6"
                    onClick={() => setCharacterStrength(characterStrength + 1)}
                    disabled={!canIncrement(characterStrength)}
                  >
                    <Plus className="h-3 w-3" />
                  </Button>
                </div>
              </div>

              {/* Dexterity */}
              <div key="dexterity-attribute" className="flex items-center justify-between bg-background/50 rounded px-2 py-1">
                <span className="text-xs font-crimson w-20">Dexterity</span>
                <div className="flex items-center gap-1">
                  <Button
                    variant="ghost"
                    size="icon"
                    className="h-6 w-6"
                    onClick={() => setCharacterDexterity(characterDexterity - 1)}
                    disabled={!canDecrement(characterDexterity)}
                  >
                    <Minus className="h-3 w-3" />
                  </Button>
                  <span className="w-8 text-center text-sm font-semibold">{characterDexterity}</span>
                  <Button
                    variant="ghost"
                    size="icon"
                    className="h-6 w-6"
                    onClick={() => setCharacterDexterity(characterDexterity + 1)}
                    disabled={!canIncrement(characterDexterity)}
                  >
                    <Plus className="h-3 w-3" />
                  </Button>
                </div>
              </div>

              {/* Constitution */}
              <div key="constitution-attribute" className="flex items-center justify-between bg-background/50 rounded px-2 py-1">
                <span className="text-xs font-crimson w-20">Constitution</span>
                <div className="flex items-center gap-1">
                  <Button
                    variant="ghost"
                    size="icon"
                    className="h-6 w-6"
                    onClick={() => setCharacterConstitution(characterConstitution - 1)}
                    disabled={!canDecrement(characterConstitution)}
                  >
                    <Minus className="h-3 w-3" />
                  </Button>
                  <span className="w-8 text-center text-sm font-semibold">{characterConstitution}</span>
                  <Button
                    variant="ghost"
                    size="icon"
                    className="h-6 w-6"
                    onClick={() => setCharacterConstitution(characterConstitution + 1)}
                    disabled={!canIncrement(characterConstitution)}
                  >
                    <Plus className="h-3 w-3" />
                  </Button>
                </div>
              </div>

              {/* Intelligence */}
              <div key="intelligence-attribute" className="flex items-center justify-between bg-background/50 rounded px-2 py-1">
                <span className="text-xs font-crimson w-20">Intelligence</span>
                <div className="flex items-center gap-1">
                  <Button
                    variant="ghost"
                    size="icon"
                    className="h-6 w-6"
                    onClick={() => setCharacterIntelligence(characterIntelligence - 1)}
                    disabled={!canDecrement(characterIntelligence)}
                  >
                    <Minus className="h-3 w-3" />
                  </Button>
                  <span className="w-8 text-center text-sm font-semibold">{characterIntelligence}</span>
                  <Button
                    variant="ghost"
                    size="icon"
                    className="h-6 w-6"
                    onClick={() => setCharacterIntelligence(characterIntelligence + 1)}
                    disabled={!canIncrement(characterIntelligence)}
                  >
                    <Plus className="h-3 w-3" />
                  </Button>
                </div>
              </div>

              {/* Wisdom */}
              <div key="wisdom-attribute" className="flex items-center justify-between bg-background/50 rounded px-2 py-1">
                <span className="text-xs font-crimson w-20">Wisdom</span>
                <div className="flex items-center gap-1">
                  <Button
                    variant="ghost"
                    size="icon"
                    className="h-6 w-6"
                    onClick={() => setCharacterWisdom(characterWisdom - 1)}
                    disabled={!canDecrement(characterWisdom)}
                  >
                    <Minus className="h-3 w-3" />
                  </Button>
                  <span className="w-8 text-center text-sm font-semibold">{characterWisdom}</span>
                  <Button
                    variant="ghost"
                    size="icon"
                    className="h-6 w-6"
                    onClick={() => setCharacterWisdom(characterWisdom + 1)}
                    disabled={!canIncrement(characterWisdom)}
                  >
                    <Plus className="h-3 w-3" />
                  </Button>
                </div>
              </div>

              {/* Charisma */}
              <div key="charisma-attribute" className="flex items-center justify-between bg-background/50 rounded px-2 py-1">
                <span className="text-xs font-crimson w-20">Charisma</span>
                <div className="flex items-center gap-1">
                  <Button
                    variant="ghost"
                    size="icon"
                    className="h-6 w-6"
                    onClick={() => setCharacterCharisma(characterCharisma - 1)}
                    disabled={!canDecrement(characterCharisma)}
                  >
                    <Minus className="h-3 w-3" />
                  </Button>
                  <span className="w-8 text-center text-sm font-semibold">{characterCharisma}</span>
                  <Button
                    variant="ghost"
                    size="icon"
                    className="h-6 w-6"
                    onClick={() => setCharacterCharisma(characterCharisma + 1)}
                    disabled={!canIncrement(characterCharisma)}
                  >
                    <Plus className="h-3 w-3" />
                  </Button>
                </div>
              </div>
            </div>
          </div>

          <div className="flex gap-2 pt-4">
            <Button onClick={handleSubmit} className="flex-1" size="sm" disabled={createCharacterMutation.isPending}>
              {createCharacterMutation.isPending ? <Loader2 className="h-4 w-4 animate-spin mr-2" /> : <Sparkles className="h-3 w-3" />}
              {createCharacterMutation.isPending ? "Summoning Soul..." : "Forge the Character"}
            </Button>
          </div>
        </div>
      </DialogContent>
    </Dialog>
  );
};
