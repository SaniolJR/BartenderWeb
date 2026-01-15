import { useState } from 'react'
import { Container, Typography } from '@mui/material'
import MissingIngredientCount from './ingredientsMissing'
import VerifiedOnly from './verifiedOnly'

export default function SearchDrinkPage() {
  const [missingCount, setMissingCount] = useState<number>(0)
  const [verified, setVerified] = useState<boolean>(false)

  return (
    <Container sx={{ py: 4 }}>

      <MissingIngredientCount number={missingCount} setNumber={setMissingCount} />

      <VerifiedOnly verified = {verified} setVerified={setVerified}/>


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
    */


      /*
      HTTP:
      ile drinkow moze brakowac
      jaka nazwa parametry
      jakei skladniki
      czy zweryfikowane
      */