import { useState } from 'react'
import { Container, Grid, Paper, Button, Box } from '@mui/material'
import MissingIngredientCount from './ingredientsMissing'
import VerifiedOnly from './verifiedOnly'
import SearchByText from './searchByText.tsx'
import IngredientsBox from './Ingredients/IngredientsBox'


  const apiUrl = import.meta.env.VITE_API_URL;

  export default function SearchDrinkPage() {
    const [missingCount, setMissingCount] = useState<number>(0)
    const [verified, setVerified] = useState<boolean>(false)
    const [textFilter, setTextFilter] = useState<string>("")
    //usestate for selected ingredients
    const [selectedIngredients, setSelectedIngredients] = useState<string[]>([])

    const getDrinks = async () => {
    const url = new URL(`${apiUrl}/drinks`);
    url.searchParams.append("Verified", String(verified));
    url.searchParams.append("TextFilter", textFilter);
    url.searchParams.append("MissingIngredients", String(missingCount));
    url.searchParams.append("PageSize", "20");
    url.searchParams.append("Page", "1");
    selectedIngredients.forEach(ing => url.searchParams.append("Ingredients", ing));

    try {
      const res = await fetch(url.toString());
      if (!res.ok) throw new Error("Server error: " + res.status);
      const data = await res.json();
      console.log("Drinks response:", data);
      // tutaj możesz ustawić stan z drinkami, jeśli chcesz je wyświetlić
    } catch (err) {
      alert("Błąd pobierania drinków: " + (err as Error).message);
    }
  };

 return (
    <Container maxWidth="xl" sx={{ py: 4 }}>
      
      {/* MAIN CONTAINER */}
      <Grid container spacing={3}>

        {/* --- FILTERS - top bar */}
        <Grid size={{xs: 12}}>
          <Paper sx={{ p: 2, display: 'flex', gap: 2, justifyContent: 'space-around'}}>

            {/*type missing indegridients*/}
            <MissingIngredientCount number={missingCount} setNumber={setMissingCount} />
            {/*check if want verified only*/}
            <VerifiedOnly verified={verified} setVerified={setVerified}/>
            {/*filter by text*/}
            <SearchByText 
                textFilter={textFilter} 
                setTextFilter={setTextFilter} 
                onSearch={getDrinks}
            />
            <Button onClick={getDrinks}>Apply filters</Button>
          </Paper>
        </Grid>

        {/* LEFT PANEL - INGREDIENTS*/}
        {/* get 3/12 width on PC and MAX on Phone */}
        <Grid size={{ xs: 12, md: 3}} >
          <Paper sx={{ p: 2, height: '70vh'}}>

            <IngredientsBox
              onSelectedChange={setSelectedIngredients}
              width="100%"
              height="100%"
            />

          </Paper>
        </Grid>

        {/* MAIN PANEL: RETURNED DRINKS*/}
        <Grid size={{xs: 12, md: 9 }} >
          <Paper sx={{ p: 2,height: '70vh', bgcolor: '#3e010148' }}>
          
    {selectedIngredients}

          </Paper>
        </Grid>

      </Grid>
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