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

  if (!drink) return <Box>Ładowanie...</Box>;

  // Ustal rozmiar czcionki zależny od szerokości lub wysokości okna
  const fontSize = '3vh'; // lub np. '8vh' jeśli chcesz zależność od wysokości

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
            borderWidth: 4, // grubość ramki
            borderColor: '#660404', // kolor ramki (opcjonalnie)
            borderStyle: 'solid', // styl ramki (opcjonalnie)
        }}
      >
        Wróć
      </Button>
      <Typography variant="h4" gutterBottom sx={{ fontSize, textAlign: 'center' }}>
        <strong>Nazwa:</strong> {drink.name} {drink.verified ? "✅" : "❌"}
      </Typography>
      <Stack spacing={2} sx={{ width: '100%', alignItems: 'center' }}>
        <Typography sx={{ fontSize, textAlign: 'center' }}>
          <strong>Przepis:</strong> {drink.recipe}
        </Typography>
        <Typography sx={{ fontSize, textAlign: 'center' }}>
          <strong>Składniki:</strong> {drink.ingredients.map((ing: any) => ing.name).join(', ')}
        </Typography>
        <Typography sx={{ fontSize, textAlign: 'center' }}>
          <strong>Średnia ocena:</strong> {drink.averageRating}
        </Typography>
      </Stack>
    </Paper>
  );
}