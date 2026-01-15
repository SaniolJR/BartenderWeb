import { Box, Button, Container, Paper, Stack, Typography } from '@mui/material'
import { Link as RouterLink } from 'react-router-dom'

export default function BartenderWebPage() {
  return (
    <Container>
        <Box>
          <Typography variant="h3" sx={{ fontWeight: 900, mb: 1 }}>
            🍸 BartenderWeb
          </Typography>
          <Typography sx={{ opacity: 0.8 }}>
                    <p>
            There’s a party. You want a drink.<br />
            You’ve got 2 limes, some cola… and maybe something stronger?
            Let’s see what we can make out of it 😎
          </p>

          <p>
            <strong>BartenderWeb</strong> helps you:
          </p>

          <ul>
            <li>🔍 Find drinks based on the ingredients you already have</li>
            <li>🍹 Discover new cocktails without guessing</li>
            <li>🚀 Turn random stuff from your fridge into a proper drink</li>
          </ul>

          <p>
            After logging in, you can:
          </p>

          <ul>
            <li>➕ Add your own drinks</li>
            <li>❤️ Save your favorite cocktails</li>
            <li>💬 Leave reviews </li>
          </ul>
          </Typography>
        </Box>

    </Container>
  )
}