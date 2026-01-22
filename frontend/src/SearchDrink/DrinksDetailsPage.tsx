import { useParams, useNavigate } from 'react-router-dom';
import { useEffect, useState } from 'react';
import { Box, Button, Paper, Typography, Stack } from '@mui/material';

const apiUrl = import.meta.env.VITE_API_URL;

export default function DrinkDetailsPage() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [drink, setDrink] = useState<any>(null);

  useEffect(() => {
    fetch(`${apiUrl}/drinks/${id}`)
      .then(res => res.json())
      .then(setDrink);
  }, [id]);

  if (!drink) return <Box>Loading... (goes by faster when you're drunk😉)</Box>;

  const fontSize = '3vh';

  return (
    <Paper sx={{ p: 3, width: '60vw', margin: 'auto', mt: '15vh',  }}>
      <Button
        onClick={() => navigate(-1)}
        sx={{
            mb: 2,
            fontSize,
            padding: '1em 2em',
            minWidth: 'unset',
            minHeight: 'unset',
            borderWidth: 4,
            borderColor: '#660404',
            borderStyle: 'solid',
        }}
      >
        Back
      </Button>
      <Typography variant="h4" gutterBottom sx={{ fontSize, textAlign: 'center' }}>
        <strong>Name:</strong> {drink.name} {drink.verified ? "✅" : "❌"}
      </Typography>
      <Stack spacing={2} sx={{ width: '100%', alignItems: 'center' }}>
        <Typography sx={{ fontSize, textAlign: 'center' }}>
          <strong>Recipe:</strong> {drink.recipe}
        </Typography>
        <Typography sx={{ fontSize, textAlign: 'center' }}>
          <strong>Ingredients:</strong> {drink.ingredients.map((ing: any) => ing.name).join(', ')}
        </Typography>
        <Typography sx={{ fontSize, textAlign: 'center' }}>
          <strong>Average rating:</strong> {drink.averageRating}
        </Typography>
      </Stack>
    </Paper>
  );
}