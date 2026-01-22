import { useState } from 'react'
import { Container, TextField, Button, Typography, Box } from '@mui/material'

export default function AddDrinkPage() {
  /*
      Insert Ingredients
  */
 //usestate for name
  const [name, setName] = useState<string>("")
  //usestate for recipe
  const [recipe, setRecipe] = useState<string>("")

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
                    '& .MuiInputBase-input': { 
                        fontSize: '2rem',
                        padding: '2vh'
                    }
                }}
            />

    </Container>
  )
}
