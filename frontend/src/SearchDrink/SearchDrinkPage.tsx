import { useState } from 'react'
import { Container, Typography } from '@mui/material'
import MissingIngredientCount from './ingredientsMissing'
import VerifiedOnly from './verifiedOnly'
import SearchByText from './searchByText.tsx'

export default function SearchDrinkPage() {
  const [missingCount, setMissingCount] = useState<number>(0)
  const [verified, setVerified] = useState<boolean>(false)
  const [textFilter, setTextFilter] = useState<string>("")

  //temporary send http simulator
  const getDrinks = () => {
    console.log({ missingCount, verified, textFilter }) // fixed typo
  }

  return (
    <Container sx={{ py: 4 }}>

      <MissingIngredientCount number={missingCount} setNumber={setMissingCount} />

      <VerifiedOnly verified = {verified} setVerified={setVerified}/>
      
      <SearchByText textFilter = {textFilter} setTextFilter={setTextFilter} onSearch={getDrinks}/>
      
      Aktualny textFilter {textFilter}

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
      sortowanie
    */


      /*
      HTTP:
      ile drinkow moze brakowac
      jaka nazwa parametry
      jakei skladniki
      czy zweryfikowane
      */