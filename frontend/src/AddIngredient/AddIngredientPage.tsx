import { useState } from 'react'
import { Container, TextField, Button, Typography, Box } from '@mui/material'

export default function AddIngredientPage() {
    //usestate for name
  const [ingredientName, setIngredientName] = useState<string>("")

  const handleAdd = () => {
    console.log({ ingredientName })
  }

  return (
    <Container sx={{ py: 4 }}>
    <Typography
        sx={{ 
            mt: '0.5vh',
            fontSize: '4rem',
            textAlign: 'center'
        }}
        >
        ✨ Add a new ingredient 🍋🥃
    </Typography>
      <TextField
            label="Ingredient name"
            value={ingredientName}
            onChange={(e) => setIngredientName(e.target.value)}
            placeholder="e.g. Cola"
            fullWidth
            size="medium"
            InputLabelProps={{
                sx: {
                fontSize: '2.5rem',
                '&.Mui-focused': { fontSize: '2rem' },
                },
            }}
            sx={{
                mt: '10vh',
                '& .MuiInputBase-input': { 
                    fontSize: '3rem',
                    padding: '2vh'
                }
            }}
        />
        <Box sx={{ display: 'flex', justifyContent: 'center' }}>
            <Button 
                onClick={handleAdd} 
                variant="contained" 
                sx={{ 
                    mt: 2,
                    fontSize: '3rem',
                    padding: '2vh 5vh',
                    height: '8vh'
                }}
                >
                Add
            </Button>
        </Box>
    </Container>
  )
}