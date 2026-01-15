import { Box, Button, Container, Paper, Stack, Typography } from '@mui/material'
import { Link as RouterLink } from 'react-router-dom'

export default function FavDrinksPage() {
  return (
    <Container>
        <Box>
          <Typography variant="h3" sx={{ fontWeight: 900, mb: 1 }}>
            Heres list of yours favourite drinks!
          </Typography>
        </Box>

    </Container>
  )
}