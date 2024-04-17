// Copyright Epic Games, Inc. All Rights Reserved.

#include "CastorinAdventureGameMode.h"
#include "CastorinAdventureCharacter.h"
#include "UObject/ConstructorHelpers.h"

ACastorinAdventureGameMode::ACastorinAdventureGameMode()
{
	// set default pawn class to our Blueprinted character
	static ConstructorHelpers::FClassFinder<APawn> PlayerPawnBPClass(TEXT("/Game/ThirdPerson/Blueprints/BP_ThirdPersonCharacter"));
	if (PlayerPawnBPClass.Class != NULL)
	{
		DefaultPawnClass = PlayerPawnBPClass.Class;
	}
}
