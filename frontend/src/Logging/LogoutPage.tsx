import { Box, Button, Container, Paper, Stack, Typography } from '@mui/material'
import { Link as RouterLink } from 'react-router-dom'

export default function LogoutPage() {
  return (
    <Container>
        <Box>
          <Typography variant="h3" sx={{ fontWeight: 900, mb: 1 }}>
            You have been logged out
          </Typography>
          
        </Box>

    </Container>
  )
}