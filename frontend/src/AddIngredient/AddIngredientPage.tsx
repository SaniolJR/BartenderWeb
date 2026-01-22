import { useState } from 'react'
import { Container, TextField, Button, Typography, Box } from '@mui/material'

export default function AddIngredientPage() {
    //usestate for name
  const [ingredientName, setIngredientName] = useState<string>("")

  //usestate for return message after add attempt
  const [message, setMessage] = useState<string | null>(null)

  const handleAdd = async () => {
  try {
    const res = await fetch('url', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ name: ingredientName }),
    })

    if (!res.ok) throw new Error(`HTTP ${res.status}`)
        setMessage('✅ Ingredient added!')
  } catch (err) {
    const msg = err instanceof Error ? err.message : 'Unknown error'
    setMessage(`❌ Cannot added Ingredient, sorry sth is wrong: ${msg}`)
  }
}

  return (
    <Container sx={{ py: 4 }}>

        {/*WELCOME MESSAGE*/}
    <Typography
        sx={{ 
            mt: '0.5vh',
            fontSize: '4rem',
            textAlign: 'center'
        }}
        >
        ✨ Add a new ingredient 🍋🥃
    </Typography>

    {/*TEXTFIELD FOR TYPE NAME OF DRINK*/}
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

        {/*BUTTON FOR ADD - AFTER CLICK IT TUN POST HTTP */}
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
        
        {/*MESSAGE BOX FOR SERVER RESPONSE*/}
        {message && (
        <Typography sx={{ mt: 2, textAlign: 'center', fontSize: '3rem' }}>
            {message}
        </Typography>
        )}
    </Container>
  )
}