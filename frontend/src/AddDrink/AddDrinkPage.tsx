import { useState } from 'react'
import { Container, TextField, Button, Typography } from '@mui/material'
import IngredientsBox from '../SearchDrink/Ingredients/IngredientsBox'


  const apiUrl = import.meta.env.VITE_API_URL;

export default function AddDrinkPage() {
  //usestate for selected ingredients
  const [selectedIngredients, setSelectedIngredients] = useState<string[]>([])
 //usestate for name
  const [name, setName] = useState<string>("")
  //usestate for recipe
  const [recipe, setRecipe] = useState<string>("")

const handleAddDrink = async () => {
  const drink = {
    name: name,
    recipe: recipe,
    ingredients: selectedIngredients,
  };

  try {
    const response = await fetch(`${apiUrl}/drinks`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(drink),
    });

    // --- FIX START: Bezpieczne czytanie odpowiedzi ---
    const textData = await response.text(); // Czytamy raz jako tekst
    let data;
    try {
        data = JSON.parse(textData); // Próbujemy zrobić z tego JSON
    } catch {
        data = textData; // Jak się nie uda (np. puste body), zostawiamy tekst
    }
    // --- FIX END ---

    if (!response.ok) {
      // Backend zwrócił błąd (np. 400, 500)
      const errorMsg = data.message || data.title || JSON.stringify(data);
      alert("Error: " + errorMsg);
      return;
    }

    // Sukces (200/201)
    alert("Drink dodany! ID: " + (data.id || "OK"));
    
    // Opcjonalnie: wyczyść formularz
    setName("");
    setRecipe("");
    setSelectedIngredients([]);

  } catch (error) {
    alert("Network Error: " + (error as Error).message);
  }
};

  return (
    <Container>
       {/*WELCOME MESSAGE*/}
      <Typography
        sx={{ 
            mt: '0.5vh',
            fontSize: '4rem',
            textAlign: 'center'
        }}
        >
        🍸🍹 Add a new Drink! 🍹🍸
    </Typography>

    {/*TEXTFIELD FOR TYPE NAME OF DRINK*/}
          <TextField
                label="Drink name"
                value={name}
                onChange={(e) => setName(e.target.value)}
                placeholder="e.g. Cola"
                fullWidth
                size="medium"
                InputLabelProps={{
                    sx: {
                    fontSize: '2rem',
                    '&.Mui-focused': { fontSize: '2rem' },
                    },
                }}
                sx={{
                    mt: '5vh',
                    '& .MuiInputBase-input': { 
                        fontSize: '2rem',
                        padding: '2vh'
                    }
                }}
            />

       {/*TEXTFIELD FOR TYPE RECEIPE OF DRINK*/}
          <TextField
                label="Drink recipe"
                value={recipe}
                onChange={(e) => setRecipe(e.target.value)}
                placeholder="e.g. 150ml of cola added to 50ml of whisky"
                fullWidth
                size="medium"
                InputLabelProps={{
                    sx: {
                    fontSize: '2rem',
                    '&.Mui-focused': { fontSize: '2rem' },
                    },
                }}
                sx={{
                    mt: '1vh',
                    mb: '1vh',
                    '& .MuiInputBase-input': { 
                        fontSize: '2rem',
                        padding: '2vh'
                    }
                }}
            />
          <IngredientsBox
            onSelectedChange={setSelectedIngredients}
            width="100%"
            height="40vh"
          />
          <Button
            variant="contained"
            color="primary"
            sx={{ mt: '2vh', fontSize: '2rem', width: '100%' }}
            onClick={handleAddDrink}
          >
            Add Drink
          </Button>
    </Container>
  )
}
