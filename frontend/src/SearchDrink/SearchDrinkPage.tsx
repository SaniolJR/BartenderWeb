import { useState } from 'react'
import { Container, Typography } from '@mui/material'
import MissingIngredientCount from './ingredientsMissing'

export default function SearchDrinkPage() {
  const [missingCount, setMissingCount] = useState<number>(0)

  return (
    <Container sx={{ py: 4 }}>

      <MissingIngredientCount number={missingCount} setNumber={setMissingCount} />

      <Typography >
        Może brakować: {missingCount}
      </Typography>
    </Container>
  )
}


/*
 Przeglądanie drinków
	-na podstawie nazwy i wybranych składników
	-możliwość wyboru opcji bez unvedified 	skladnikow - pokazuje tylko driny ze 	składnikami od admina
	-sortowanie oceną
	-możliwość wybrania verified drinkow
 */

  /*
  TODO:
    -okno wpisywania
    -przycisk VERIFIED ONLY obok
    -Menu ze składnikami:
      -okno wyszukiwania składniku po nazwie
      -okienko gdzie masz już zaznaczone (możesz wyczyścic i odznaczyć)
      -ilu składnikow moze brakować
    */