import { Box, Button, Container, Paper, Stack, Typography } from '@mui/material'
import { Link as RouterLink } from 'react-router-dom'

export default function BartenderWebPage() {
  return (
    <Container>
        <Box>
          <Typography variant="h3" sx={{ fontWeight: 900, mb: 1 }}>
            BartenderWeb
          </Typography>
          <Typography sx={{ opacity: 0.8 }}>
            Wybierz co chcesz zrobić.
          </Typography>
        </Box>

    </Container>
  )
}